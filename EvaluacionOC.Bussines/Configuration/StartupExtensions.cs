using EvaluacionOC.Bussines.Helpers;
using EvaluacionOC.Bussines.Services;
using EvaluacionOC.Data.Configuration;
using EvaluacionOC.Data.Data.Context;
using EvaluacionOC.Model.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluacionOC.Bussines.Configuration
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserServiceBL, UserServiceBL>();
            services.AddTransient<ISeguridad, Seguridad>();
            services.AddServicesDl(configuration);
            return services;
        }
    }
}
