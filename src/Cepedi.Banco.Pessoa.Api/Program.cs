using System.Data;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Cepedi.Banco.Pessoa.IoC;
using Cepedi.Banco.Pessoa.Api;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddScoped<IDbConnection>(sp => 
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMemoryCache();

builder.Services.AddScoped<PessoaRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAppDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // await app.InitialiseDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Map("/", () => Results.Redirect("/swagger"));

app.Run();
