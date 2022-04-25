using System.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Gym.API.Domain.Models;
using Gym.API.Helpers;
using System.Collections.Generic;

namespace Gym.API.Extensions
{
    public static class UserExtensions
    {
        public static void GenerateToken(this User user, string secret, int expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.Login),
            };

            var roles = user.UserRoles.Select(x => x.Role).ToList();

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x.Name));

            claims.AddRange(roleClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
        }
    }
}