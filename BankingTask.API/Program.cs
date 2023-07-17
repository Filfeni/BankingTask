using BankingTask.BusinessLogic;
using BankingTask.BusinessLogic.Config;
using BankingTask.BusinessLogic.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddDbContext(connectionString);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddBusinessLogicServiceDependency();

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
