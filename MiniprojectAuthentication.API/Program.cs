using System.Reflection.Metadata;
using MiniprojectAuthentication.API.Extensions;
using MiniprojectAuthentication.API.Middleware;
using MiniprojectAuthentication.Repo;
using Microsoft.EntityFrameworkCore;
using MailService = MiniprojectAuthentication.Service.MailService;
using JwtService = MiniprojectAuthentication.Service.JwtService;


    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddControllers();
    // Add services to the container.
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );

    builder.Services.ConfigureRateLimiter();
    builder.Services.AddJwtServices(builder.Configuration);
    builder.Services.AddSwaggerServices();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddScoped<MailService.IService, MailService.Service>();
    builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
    builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

    //builder.Services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

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