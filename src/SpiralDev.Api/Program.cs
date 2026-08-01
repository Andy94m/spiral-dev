using Microsoft.EntityFrameworkCore;
using SpiralDev.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Swagger UI en /swagger (Swashbuckle) — documentación interactiva de la API
builder.Services.AddSwaggerGen();

// Registra el DbContext con PostgreSQL.
// La connection string vive en User Secrets (ConnectionStrings:Default).
builder.Services.AddDbContext<SpiralDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// CORS: permite que el frontend (React en localhost:5173) llame a esta API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

// En desarrollo, sembramos la base de datos con el contenido del libro
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<SpiralDbContext>();
    db.Database.Migrate();      // Aplica migraciones pendientes
    DbSeeder.Seed(db);          // Carga el contenido inicial
}

app.Run();
