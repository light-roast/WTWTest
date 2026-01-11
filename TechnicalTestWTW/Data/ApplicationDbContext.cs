using Microsoft.EntityFrameworkCore;
using TechnicalTestWTW.Models;

namespace TechnicalTestWTW.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.FullIdentificationNumber)
                .HasComputedColumnSql("([IdentificationNumber] + '-' + [IdentificationType])", stored: true);

            entity.Property(e => e.FullName)
                .HasComputedColumnSql("([FirstName] + ' ' + [LastName])", stored: true);

            entity.HasData(
                new Persona
                {
                    Id = 1,
                    FirstName = "Daniel",
                    LastName = "Echeverri",
                    IdentificationNumber = "1088315344",
                    IdentificationType = "CC",
                    Email = "echeverri121@gmail.com",
                    CreatedDate = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc)
                }
            );
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.HasData(
                new Usuario
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123",
                    CreatedDate = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc)
                }
            );
        });
    }
}
