using BankingTask.BusinessLogic.Services;
using BankingTask.BusinessLogic.Validators;
using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BankingTask.BusinessLogic.Config
{
    public static class BusinessLogicServiceDependencyExtensions
    {
        public static IServiceCollection AddBusinessLogicServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ClientePostRequestDto>, ClientePostRequestValidator>();
            services.AddScoped<IValidator<ClientePutRequestDto>, ClientePutRequestValidator>();
            services.AddScoped<IValidator<CuentaPatchRequestDto>, CuentaPatchRequestValidator>();
            services.AddScoped<IValidator<CuentaPostRequestDto>, CuentaPostRequestValidator>();
            services.AddScoped<IValidator<MovimientoRequestDto>, MovimientoRequestValidator>();
            services.AddScoped<IValidator<ReporteRequestDto>, ReporteRequestValidator>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<ICuentaService, CuentaService>();
            services.AddTransient<IMovimientoService, MovimientoService>();
            return services;
        }
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
          services.AddDbContext<BankingDBContext>(options =>
               options.UseSqlServer(connectionString));
    }
}
