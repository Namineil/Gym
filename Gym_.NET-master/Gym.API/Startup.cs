using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Helpers;
using Gym.API.Persistence.Contexts;
using Gym.API.Persistence.Repositories;
using Gym.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Gym.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"));
            });

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IAdministratorHallRepository, AdministratorHallRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICardSalesHistoryRepository, CardSalesHistoryRepository>();
            services.AddScoped<IClassRecordsRepository, ClassRecordsRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IScheduleTrainingRepository, ScheduleTrainingRepository>();
            services.AddScoped<IServicesCardRepository, ServicesCardRepository>();
            services.AddScoped<ISpecializationRepository, SpecializationRepository>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IAdministratorHallService, AdministratorHallService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ICardSalesHistoryService, CardSalesHistoryService>();
            services.AddScoped<IClassRecordsService, ClassRecordsService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IScheduleTrainingService, ScheduleTrainingService>();
            services.AddScoped<IServicesCardService, ServicesCardService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Startup));
            services.AddCors();

            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "API Gym",
                    Version = "v1"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => {
                x.AllowAnyOrigin()
                    .AllowAnyMethod()
                        .AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(x => {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gym v1");
                x.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
