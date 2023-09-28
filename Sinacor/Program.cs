using Microsoft.EntityFrameworkCore;
using Sinacor.Context;
using Sinacor.Domain;
using Sinacor.Interfaces;
using Sinacor.Repositories;
using Sinacor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMQConfig>(builder.Configuration.GetSection("RabbitMQ"));
// Cria string de conexão
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

//Repositories
builder.Services.AddTransient<ITarefaRepository, TarefaRepository>();

//Services
builder.Services.AddTransient<ILogger, Logger<RabbitMQService>>();
builder.Services.AddTransient<IRabbitMQService, RabbitMQService>();
builder.Services.AddTransient<ITarefaService, TarefaService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseCors("AllowedHosts");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
