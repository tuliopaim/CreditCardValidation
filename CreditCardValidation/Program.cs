using CreditCardValidation.Core;
using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Infrastructure.Notifier;
using CreditCardValidation.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<INotifier, Notifier>()
    .AddScoped<ICreditCardRepository, CreditCardRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services
   .AddMediatR(config => config.AsScoped(), Assembly.GetExecutingAssembly())
   .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>))
   .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
