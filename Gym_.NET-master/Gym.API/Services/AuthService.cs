using System.Net.Mime;
using System.Security.AccessControl;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services;
using Gym.API.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Gym.API.Domain.Repositories;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Gym.API.Extensions;

namespace Gym.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings appSettings;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        public AuthService(IOptions<AppSettings> appSettings, IUserRepository userRepository, IUnitOfWork unitOfWork) {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> Authenticate(string login,
                                 string password, bool rememberMe)
        {
            var user = (await userRepository.ListAsync())
                            .SingleOrDefault(usr => usr.Login == login);

            if (user == null)
            {
                return null;
            }

            var hashPassword = this.hashPwd(password);
            
            if (user.Password != hashPassword) {
                return null;
            }

            user.GenerateToken(appSettings.Secret, rememberMe ? appSettings.ExpiresRememberMeMinutes : appSettings.ExpiresMinutes);

            await unitOfWork.CompleteAsync();
            return user;
        }

        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        public string hashPwd(string password) {
            var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(appSettings.Secret));
            var hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
            return AuthService.byteArrayToString(hash);
        }

        public async Task<User> ValidateToken(string token) {
            try {
                var user = (await userRepository.ListAsync())
                                .SingleOrDefault(usr => usr.Token == token);
            
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));
                var validationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    ValidateActor = false,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken = null;
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                    
                return validatedToken != null ? user : null;
            } catch {
                return null;
            }
        }
    }
}