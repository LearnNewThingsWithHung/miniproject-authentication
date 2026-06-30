using System.Reflection.Metadata;
using FluentValidation;
using MiniProjectAuthentication.API.Extensions;
using MiniProjectAuthentication.API.Middleware;
using MiniProjectAuthentication.Repo;
using Microsoft.EntityFrameworkCore;
using AssemblyReference = MiniProjectAuthentication.Service.AssemblyReference;
using MailService = MiniProjectAuthentication.Service.MailService;
using JwtService = MiniProjectAuthentication.Service.JwtService;
using CacheService = MiniProjectAuthentication.Service.CacheService;
using AuthService = MiniProjectAuthentication.Service.Auth;

    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddControllers();
    // Add services to the container.
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
        options.InstanceName = "TanHung";
    });
    
    builder.Services.ConfigureRateLimiter();
    builder.Services.AddJwtServices(builder.Configuration);
    builder.Services.AddSwaggerServices();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddScoped<MailService.IService, MailService.Service>();
    builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
    builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
    builder.Services.AddScoped<CacheService.IService, CacheService.Service>();
    builder.Services.AddScoped<AuthService.IService, AuthService.Service>();

    
    builder.Services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });


    var app = builder.Build();
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerAPI();
    }

    app.UseCors("AllowFrontend");

    app.UseRateLimiter();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();