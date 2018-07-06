using EvaluacionOC.Model.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EvaluacionOC.Model.Interfaces;

namespace EvaluacionOC.Data.Helpers
{
    internal class Token : IToken
    {

        public Token(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string CrearToken(User usuario)
        {
            var claims = new[]
            {
                new Claim("UserData", JsonConvert.SerializeObject(usuario))
            };

            // Generamos el Token
            var token = new JwtSecurityToken
            (
                issuer: Configuration["ApiAuth:Issuer"],
                audience: Configuration["ApiAuth:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApiAuth:SecretKey"])),
                        SecurityAlgorithms.HmacSha256)
            );            
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            return tokenString;
        }


    }
}
