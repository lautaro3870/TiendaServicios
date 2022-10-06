using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria()
        {

        }
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options)
        {

        }

        //con el virtual, le permitimos a la clase sobreescribirse a futuro
        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
