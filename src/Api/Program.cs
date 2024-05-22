using Api.Middlewares;
using Domain.Options;
using Infra.DB;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PasswordHashOptions>(builder.Configuration.GetSection("PasswordHashOptions"));
builder.Services.AddTransient<IHashingService, HashingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMainTaskService, MainTaskService>();
builder.Services.AddScoped<IMainTaskRepository, MainTaskRepository>();

builder.Services.AddScoped<ISubTaskService, SubTaskService>();
builder.Services.AddScoped<ISubTaskRepository, SubTaskRepository>();

builder.Services.AddDbContext<MyDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AceleraDev"),
        sqlOptions => sqlOptions.MigrationsAssembly("Infra")));

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

app.UseMiddleware(typeof(ExceptionHandler));

app.UseAuthorization();

app.MapControllers();

app.Run();
