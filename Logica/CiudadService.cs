using Datos;
using Entidades;

namespace Logica
{
    public class CiudadService
    {
        private readonly AplicacionDBContext _context;
        public CiudadService(AplicacionDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Ciudad>? ConsultarTodas() => _context.Ciudades?.ToList();
    }
}