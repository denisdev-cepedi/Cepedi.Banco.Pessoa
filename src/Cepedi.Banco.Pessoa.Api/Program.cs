using Serilog;
using Cepedi.Banco.Pessoa.IoC;
using Microsoft.IdentityModel.Logging;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureAppDependencies(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
    .ReadFrom.Configuration(context.Configuration)
    .WriteTo.Console()
    .WriteTo.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithExceptionDetails()
    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!)
    .WriteTo.Elasticsearch(ConfigureElasticSink(context.Configuration, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"BancoPessoa{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}

app.UseHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Map("/", () => Results.Redirect("/swagger"));

app.Run();

