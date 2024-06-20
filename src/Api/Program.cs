using Api.Middlewares;
using Domain.Options;
using Infra.DB;
using Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service;
using System.Text;

using FluentAssertions.Common;
using Microsoft.OpenApi.Models;
using Infra;


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

builder.Services.AddHttpClient("notificationClient", client =>
{
    var notificationUri = builder.Configuration.GetValue<string>("NotificationApi:BaseUrl");
    client.BaseAddress = new Uri(notificationUri);
    client.Timeout = TimeSpan.FromSeconds(30);
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
builder.Services.AddScoped<NotificationHttpClient>();

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


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer { Token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    }); ;
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
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

app.UseCors("AllowAllHeaders");

app.UseMiddleware(typeof(ExceptionHandler));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
