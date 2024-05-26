using Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Domain.Options;
using Infra.DB;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Service;
using System.Text;
using FluentAssertions.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

// Configuração do Token e Hash
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("Token"));
builder.Services.Configure<PasswordHashOptions>(builder.Configuration.GetSection("PasswordHashOptions"));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddTransient<IHashingService, HashingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMainTaskService, MainTaskService>();
builder.Services.AddScoped<IMainTaskRepository, MainTaskRepository>();
builder.Services.AddScoped<ISubTaskService, SubTaskService>();
builder.Services.AddScoped<ISubTaskRepository, SubTaskRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddDbContext<MyDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AceleraDev"),
        sqlOptions => sqlOptions.MigrationsAssembly("Infra")));

var tokenOptions = builder.Configuration.GetSection("Token").Get<TokenOptions>();
if (tokenOptions == null || string.IsNullOrEmpty(tokenOptions.Key))
{
    throw new ArgumentNullException(nameof(tokenOptions.Key), "Token key must be configured.");
}
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
      .AddJwtBearer(options =>
      {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
              IssuerSigningKey = securityKey,
              ValidateIssuerSigningKey = true,
              ValidateAudience = true,
              ValidAudience = tokenOptions.Audience,
              ValidateIssuer = true,
              ValidIssuer = tokenOptions.Issuer,
              ValidateLifetime = true
          };
      });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDBContext>();
    dbContext.Database.Migrate();

    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    {
        var seeder = new DBSeed(dbContext);
        seeder.Seed();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseMiddleware(typeof(ExceptionHandler));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
