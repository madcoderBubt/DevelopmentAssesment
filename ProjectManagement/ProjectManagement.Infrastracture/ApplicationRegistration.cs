using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.CQRS;
using ProjectManagement.Infrastracture.Context;
using ProjectManagement.Infrastracture.Interface;
using ProjectManagement.Infrastracture.Repositories;
using ProjectManagement.Infrastracture.Services;
using ProjectManagement.Infrastracture.Services.Interfaces;
using System.Reflection;

namespace ProjectManagement.Infrastracture
{
    public static class ApplicationRegistration
    {
       
        public static IServiceCollection AddApplicationServicess(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddVa

            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            services.AddScoped<JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddTransient<Dispatcher>();

            services.AddCQRSFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
