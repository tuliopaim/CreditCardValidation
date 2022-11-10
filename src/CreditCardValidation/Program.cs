using CreditCardValidation.Core;
using CreditCardValidation.Core.Contracts;
using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Infrastructure;
using CreditCardValidation.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CreditCardDbContext>(opt => opt.UseInMemoryDatabase("CreditCardDB"));

var currentAssembly =  Assembly.GetExecutingAssembly();

builder.Services
   .AddMediatR(config => config.AsScoped(), currentAssembly)
   .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>))
   .AddValidatorsFromAssembly(currentAssembly);

ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services
    .AddSingleton<IDateTimeProvider, DateTimeProvider>()
    .AddScoped<INotifier, Notifier>()
    .AddScoped<ICreditCardRepository, CreditCardRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<CreditCardDbContext>().Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
