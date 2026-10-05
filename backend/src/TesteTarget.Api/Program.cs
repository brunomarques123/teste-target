using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TesteTarget.Api.Middleware;
using TesteTarget.Application;
using TesteTarget.Application.Juros;
using TesteTarget.Infrastructure;
using TesteTarget.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opcoes =>
    {
        // Enums como texto no JSON ("Entrada"/"Saida").
        opcoes.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JurosOptions>(builder.Configuration.GetSection(JurosOptions.Secao));
builder.Services.AddApplication();

var connectionString = builder.Configuration.GetConnectionString("Default");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("ConnectionStrings:Default não configurada no appsettings.json.");
}

builder.Services.AddInfrastructure(connectionString);

builder.Services.AddCors(opcoes =>
{
    opcoes.AddDefaultPolicy(politica =>
    {
        politica.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

var app = builder.Build();

// Primeiro da fila, para capturar exceções das etapas seguintes.
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Em desenvolvimento, cria o banco, aplica as migrations e o seed.
    using (var escopo = app.Services.CreateScope())
    {
        var db = escopo.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}

app.UseCors();
app.MapControllers();

app.Run();
