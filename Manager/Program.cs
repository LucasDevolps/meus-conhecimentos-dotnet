using Manager.Infrastructure;
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

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Logs 
builder.Services.AddScoped<ILogRequestResponseRepository, LogRequestResponseRepository>();
builder.Services.AddScoped<ILogRequestResponseService, LogRequestResponseService>();

//Pegando variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
