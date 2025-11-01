using Manager.Application;
using Manager.Application.Interfaces;
using Manager.Application.Services;
using Manager.Domain.Entities;
using Manager.Domain.Interfaces;
using Manager.Infrastructure;
using Manager.Infrastructure.Repositories;
using Manager.Infrastructure.Repositories.Logging;
using Manager.Infrastructure.Services;
using Manager.Web.Services;
using Manager.WebApi.Middleware;
using Manager.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplicationServices();

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IRepository<Moto>, MotoRepository>();
builder.Services.AddScoped<IMotoService, MotoService>();


builder.Services.AddScoped<ILogRequestResponseRepository, LogRequestResponseRepository>();
builder.Services.AddScoped<ILogRequestResponseService, LogRequestResponseService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();