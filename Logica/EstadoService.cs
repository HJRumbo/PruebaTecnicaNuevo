using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Logica
{
    public class EstadoService
    {
        private readonly AplicacionDBContext _context;
        public EstadoService(AplicacionDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Estado>? ConsultarTodas() => _context.Estados?.ToList();
    }
}