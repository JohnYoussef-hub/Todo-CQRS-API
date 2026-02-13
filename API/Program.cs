using Application;
using Application.Behaviors;
using Application.Common.Interfaces;
using API.Exceptions;
using FluentValidation;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IAppDbContext, AppDbContext>();

var app = builder.Build();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
