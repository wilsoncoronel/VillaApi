using Microsoft.EntityFrameworkCore;
using VillaAPI.Modelos;

namespace VillaAPI.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa() { 
                    Id = 1,
                    Nombre = "Villa vista a la playa",
                    Detalle = "Detalle villa",
                    ImagenUrl = "",
                    Amenidad = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    Tarifa= 200,
                    FechaCreacion= DateTime.Now,
                    FechaActualizacion= DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "Villa galante",
                    Detalle = "Detalle villa",
                    ImagenUrl = "",
                    Amenidad = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    Tarifa = 200,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Villa()
                {
                    Id = 3,
                    Nombre = "Villa Nuvea",
                    Detalle = "Detalle villa",
                    ImagenUrl = "",
                    Amenidad = "",
                    Ocupantes = 4,
                    MetrosCuadrados = 50,
                    Tarifa = 200,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            );
        }
    }
}
