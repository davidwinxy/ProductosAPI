using Microsoft.EntityFrameworkCore;
using ProductosAPI.Context;
using ProductosAPI.Services.Implementaciones;
using ProductosAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ProductosAPI.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<ProductosContext>(
    options => options.UseMySql(conString, ServerVersion.AutoDetect(conString))
);

builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<ICompraService, CompraService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(b => {
    b.SwaggerDoc("v1", new OpenApiInfo { Title = "Productos API", Version = "V1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Ingresar tu token de JWT Authentication",
        
        Reference = new OpenApiReference {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    b.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    b.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("LoggedInPolicy", policy => {
        policy.RequireAuthenticatedUser();
    });
});

// Clave JWT segura y de longitud adecuada
var key = "UnaClaveSeguraYComplejaDeAlMenos32Caracteres!2024"; // Asegúrate de que esta clave tenga al menos 32 caracteres

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.RequireHttpsMetadata = false; // Cambia a true en producción
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateAudience = false,
        ValidateIssuer = false,
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Debes iniciar sesión" });
            return context.Response.WriteAsync(result);
        }
    };
});

// Registra el servicio de autenticación JWT
builder.Services.AddScoped<IJwtAuthenticationService>(provider => new JwtAuthenticationService(key));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
