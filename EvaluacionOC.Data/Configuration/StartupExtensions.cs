using EvaluacionOC.Data.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EvaluacionOC.Model.Interfaces;
using EvaluacionOC.Data.Services;
using EvaluacionOC.Data.Helpers;

namespace EvaluacionOC.Data.Configuration
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddServicesDl(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IToken, Token>();
            services.AddDbContext<EvalDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));


            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["ApiAuth:Issuer"],
                    ValidAudience = configuration["ApiAuth:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApiAuth:SecretKey"]))
                };
            });


            return services;
        }
    }
}
