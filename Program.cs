using API_Pdv.Infraestructure.Data.Context;
using API_Pdv.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

// Dependency Injection
builder.Services.AddScoped<IProduto, API_Pdv.Infraestructure.Repositories.Produto>();
builder.Services.AddScoped<IEmpresa, API_Pdv.Infraestructure.Repositories.Empresa>();
builder.Services.AddScoped<IItemPedido, API_Pdv.Infraestructure.Repositories.ItemPedido>();
//builder.Services.AddScoped<ICaixa, API_Pdv.Infraestructure.Repositories.Caixa>();
//builder.Services.AddScoped<IMotoboy, API_Pdv.Infraestructure.Repositories.Motoboy>();
builder.Services.AddScoped<IPedido, API_Pdv.Infraestructure.Repositories.Pedido>();
builder.Services.AddScoped<IPagamentoCaixa, API_Pdv.Infraestructure.Repositories.PagamentoCaixa>();

// Adiciona Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Pdv", Version = "v1" });
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

app.MapControllers();
app.Run();