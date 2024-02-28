using System.Text;
using Animal_Shelter;
using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using Animal_Shelter.Models.Validators;
using Animal_Shelter.Serivces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AnimalShelterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IValidator<AuthShelterDto>, CreateShelterValidator>();
builder.Services.AddScoped<IAuthSerivce, AuthService>();
builder.Services.AddScoped<IPasswordHasher<Shelter>, PasswordHasher<Shelter>>();
var authSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Auth").Bind(authSettings);
builder.Services.AddSingleton(authSettings);
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
builder.Services.AddScoped<IAuthSerivce, AuthService>();


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