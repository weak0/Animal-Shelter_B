using System.Text;
using Animal_Shelter;
using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using Animal_Shelter.Models.Validators;
using Animal_Shelter.Serivces;
using Animal_Shelter.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AnimalShelterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPasswordHasher<Shelter>, PasswordHasher<Shelter>>();
var authSettings = new AuthenticationSettings();
if (Environment.GetEnvironmentVariable("CI") == "true")
{
    // authSettings.ExpiresDate = int.Parse(Environment.GetEnvironmentVariable("AUTH_JWT_DATE")?? throw new InvalidOperationException());
    authSettings.JwtIssuer = Environment.GetEnvironmentVariable("AUTH_JWT_ISSUER") ?? throw new InvalidOperationException();
    authSettings.JwtKey = Environment.GetEnvironmentVariable("AUTH_JWT_KEY") ?? throw new InvalidOperationException();
    Console.WriteLine(authSettings.JwtKey);
}
else
{
    builder.Configuration.GetSection("Auth").Bind(authSettings);
}

builder.Services.AddSingleton<IAuthenticationSettings>(authSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(options => 
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authSettings.JwtIssuer,
        ValidAudience = authSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey))
    };
});
builder.Services.AddSingleton<IValidator<AuthShelterRegisterDto>, CreateShelterValidator>();
builder.Services.AddScoped<IAuthSerivce, AuthService>();
builder.Services.AddScoped<IAnimalsService, AnimalsService>();
builder.Services.AddScoped<IShelterService, ShelterService>();
builder.Services.AddScoped<ICostService, CostService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
};
