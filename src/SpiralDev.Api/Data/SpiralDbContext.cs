using Microsoft.EntityFrameworkCore;
using SpiralDev.Api.Models;

namespace SpiralDev.Api.Data;

/// <summary>
/// El puente entre nuestro código C# y la base de datos PostgreSQL.
/// EF Core lee estas clases y las convierte en tablas reales.
/// </summary>
public class SpiralDbContext : DbContext
{
    public SpiralDbContext(DbContextOptions<SpiralDbContext> options)
        : base(options)
    {
    }

    // Cada DbSet = una tabla en la base de datos
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
}
