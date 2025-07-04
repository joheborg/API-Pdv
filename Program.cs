using API_Pdv.Infraestructure.Data.Context;
using API_Pdv.Interfaces.Repositories;
using API_Pdv.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5193); 
});

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "WebPdv",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "WebPdv",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "SuaChaveSecretaAqui123456789"))
        };
    });

builder.Services.AddAuthorization();

// Dependency Injection
builder.Services.AddScoped<IProduto, API_Pdv.Infraestructure.Repositories.Produto>();
builder.Services.AddScoped<IEmpresa, API_Pdv.Infraestructure.Repositories.Empresa>();
builder.Services.AddScoped<IItemPedido, API_Pdv.Infraestructure.Repositories.ItemPedido>();
builder.Services.AddScoped<ICategoria, API_Pdv.Infraestructure.Repositories.CategoriaRepository>();
builder.Services.AddScoped<IStatusPedido, API_Pdv.Infraestructure.Repositories.StatusPedidoRepository>();
builder.Services.AddScoped<IUsuario, API_Pdv.Infraestructure.Repositories.UsuarioRepository>();
builder.Services.AddScoped<IAuthService, API_Pdv.Infraestructure.Services.AuthService>();
//builder.Services.AddScoped<ICaixa, API_Pdv.Infraestructure.Repositories.Caixa>();
//builder.Services.AddScoped<IMotoboy, API_Pdv.Infraestructure.Repositories.Motoboy>();
builder.Services.AddScoped<IPedido, API_Pdv.Infraestructure.Repositories.Pedido>();
builder.Services.AddScoped<IPagamentoCaixa, API_Pdv.Infraestructure.Repositories.PagamentoCaixa>();

// Adiciona Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Pdv", Version = "v1" });
    
    // Configurar JWT no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pdv v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();