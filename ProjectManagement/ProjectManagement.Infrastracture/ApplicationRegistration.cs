using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Infrastracture.Context;
using ProjectManagement.Infrastracture.Interface;
using ProjectManagement.Infrastracture.Repositories;
using ProjectManagement.Infrastracture.Services;
using ProjectManagement.Infrastracture.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture
{
    public static class ApplicationRegistration
    {
       
        public static IServiceCollection AddApplicationServicess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            services.AddScoped<JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();



            return services;
        }
    }
}
