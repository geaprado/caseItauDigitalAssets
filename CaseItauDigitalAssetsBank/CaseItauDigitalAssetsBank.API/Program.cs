using CaseItauDigitalAssetsBank.API.Auth;
using CaseItauDigitalAssetsBank.Application.Interfaces;
using CaseItauDigitalAssetsBank.Application.Services;
using CaseItauDigitalAssetsBank.Infra.Data.Data;
using CaseItauDigitalAssetsBank.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=db_Itau_clientes.db"));


builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ClienteService>();


var jwtSecretConfig = builder.Configuration["Jwt:Secret"];
if (string.IsNullOrEmpty(jwtSecretConfig))
    throw new InvalidOperationException("Configuration missing: Jwt:Secret (check appsettings, env vars, or user-secrets).");
byte[] jwtKeyBytes;
try { jwtKeyBytes = Convert.FromBase64String(jwtSecretConfig); }
catch { jwtKeyBytes = Encoding.UTF8.GetBytes(jwtSecretConfig); }
if (jwtKeyBytes.Length * 8 <= 256)
    throw new InvalidOperationException($"Jwt:Secret too short: {jwtKeyBytes.Length * 8} bits. Provide a base64 key >256 bits.");
builder.Services.AddSingleton<IAuthService>(new JwtAuthService(jwtKeyBytes));
var key = new SymmetricSecurityKey(jwtKeyBytes);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanClientesRefactor API", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    };
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    { new OpenApiSecurityScheme { Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }}, new string[] {} }
});
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.EnsureCreated();

    if (!ctx.Clientes.Any())
    {
        ctx.Clientes.Add(new CaseItauDigitalAssetsBank.Domain.Entities.Cliente { Nome = "Test", Email = "test@test.com", Saldo = 0m });
        ctx.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


using var rng = RandomNumberGenerator.Create();
var bytes = new byte[64];
rng.GetBytes(bytes);
var base64 = Convert.ToBase64String(bytes);
Console.WriteLine(base64);
