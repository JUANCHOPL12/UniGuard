using Microsoft.EntityFrameworkCore;
using UniGuard.Shared;

namespace UniGuard.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Equipo> Equipos { get; set; }
    public DbSet<MarcaEquipo> MarcasEquipos { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<UsuarioSistema> UsuariosSistema { get; set; }
    public DbSet<Asignacion> Asignaciones { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Estudiante>().ToTable("Estudiantes");
        modelBuilder.Entity<Equipo>().ToTable("Equipos");
        modelBuilder.Entity<MarcaEquipo>().ToTable("MarcasEquipos");
        modelBuilder.Entity<Estado>().ToTable("Estados");
        modelBuilder.Entity<Rol>().ToTable("Roles");
        modelBuilder.Entity<UsuarioSistema>().ToTable("UsuariosSistema");
        modelBuilder.Entity<Movimiento>().ToTable("Movimientos");

        // 1. Relación: Equipo -> Estudiante
        // CAMBIADO: De Cascade a Restrict para romper el ciclo que sale en el error rojo
        modelBuilder.Entity<Equipo>()
            .HasOne(eq => eq.Estudiante)
            .WithMany(e => e.Equipos)
            .HasForeignKey(eq => eq.EstudianteId)
            .OnDelete(DeleteBehavior.Restrict); // <--- ESTO ES VITAL

        // 2. Relación: Movimiento -> Estudiante
        modelBuilder.Entity<Movimiento>()
            .HasOne(m => m.Estudiante)
            .WithMany()
            .HasForeignKey(m => m.EstudianteId)
            .OnDelete(DeleteBehavior.NoAction);

        // 3. Relación: Movimiento -> Equipo
        modelBuilder.Entity<Movimiento>()
            .HasOne(m => m.Equipo)
            .WithMany()
            .HasForeignKey(m => m.EquipoId)
            .OnDelete(DeleteBehavior.NoAction);

        // 4. Otras relaciones (Todas en Restrict por seguridad)
        modelBuilder.Entity<Equipo>()
            .HasOne(eq => eq.Marca)
            .WithMany()
            .HasForeignKey(eq => eq.MarcaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Equipo>()
            .HasOne(eq => eq.EstadoActual)
            .WithMany()
            .HasForeignKey(eq => eq.EstadoActualId)
            .OnDelete(DeleteBehavior.Restrict);

        // DATA SEEDING
        modelBuilder.Entity<Rol>().HasData(
            new Rol { Id = 1, Nombre = "Administrador" },
            new Rol { Id = 2, Nombre = "Guardia" }
        );

        modelBuilder.Entity<Estado>().HasData(
            new Estado { Id = 1, Nombre = "Dentro", TipoEntidad = "Equipo" },
            new Estado { Id = 2, Nombre = "Fuera", TipoEntidad = "Equipo" },
            new Estado { Id = 3, Nombre = "Activo", TipoEntidad = "Estudiante" },
            new Estado { Id = 4, Nombre = "Inactivo", TipoEntidad = "Estudiante" }
        );

        modelBuilder.Entity<MarcaEquipo>().HasData(
            new MarcaEquipo { Id = 1, Nombre = "HP" },
            new MarcaEquipo { Id = 2, Nombre = "Lenovo" },
            new MarcaEquipo { Id = 3, Nombre = "Dell" },
            new MarcaEquipo { Id = 4, Nombre = "Apple" },
            new MarcaEquipo { Id = 5, Nombre = "Asus" },
            new MarcaEquipo { Id = 6, Nombre = "Acer" }
        );
    }
}