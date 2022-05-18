using Datos;
using Entidades;

namespace Logica
{
    public class AerolineaService
    {
        private readonly AplicacionDBContext _context;
        public AerolineaService(AplicacionDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Aerolinea>? ConsultarTodas() => _context.Aerolineas?.ToList();
    }
}