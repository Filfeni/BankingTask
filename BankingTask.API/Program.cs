using BankingTask.API;
using BankingTask.API.Data.DTOs;
using BankingTask.API.Data.Entities;
using BankingTask.API.Services;
using BankingTask.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddFile($@"{Directory.GetCurrentDirectory()}\logs\log.txt");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILoggerService,LoggerService>();
builder.Services.AddAutoMapper(typeof(AutomapperProfile));


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BankingDBContext>(options =>
           options.UseSqlServer(connectionString));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<ClientePostRequestDto>, ClientePostRequestValidator>();
builder.Services.AddScoped<IValidator<ClientePutRequestDto>, ClientePutRequestValidator>();
builder.Services.AddScoped<IValidator<CuentaPatchRequestDto>, CuentaPatchRequestValidator>();
builder.Services.AddScoped<IValidator<CuentaPostRequestDto>, CuentaPostRequestValidator>();
builder.Services.AddScoped<IValidator<MovimientoRequestDto>, MovimientoRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
