
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class AplicacionDBContext : DbContext
    {
        public AplicacionDBContext(DbContextOptions<AplicacionDBContext> options): base(options)
        {
        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Vuelo>? Vuelos { get; set; }
        public DbSet<Aerolinea>? Aerolineas { get; set; }
        public DbSet<Ciudad>? Ciudades { get; set; }
        public DbSet<Estado>? Estados { get; set; }
    }
}