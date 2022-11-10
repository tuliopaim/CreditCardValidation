using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Infrastructure.Core;
using CreditCardValidation.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<IDateTimeProvider, DateTimeProvider>();

builder.Services
    .AddScoped<INotifier, Notifier>()
    .AddScoped<ICreditCardRepository, CreditCardRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>();

var currentAssembly =  Assembly.GetExecutingAssembly();
builder.Services
   .AddMediatR(config => config.AsScoped(), currentAssembly)
   .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>))
   .AddValidatorsFromAssembly(currentAssembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
