using Datos;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Logica
{
    public class VueloService
    {
        private readonly AplicacionDBContext _context;
        public VueloService(AplicacionDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Vuelo>? ConsultarTodos() => _context.Vuelos?
            .Include("Aerolinea")
            .Include("EstadoVuelo")
            .Include("CiudadOrigen")
            .Include("CiudadDestino")
            .ToList();

        public Vuelo? ConsultarVuelo(int id) => _context.Vuelos?
            .Where(v => v.NumeroVuelo == id)
            .Include("Aerolinea")
            .Include("EstadoVuelo")
            .Include("CiudadOrigen")
            .Include("CiudadDestino")
            .FirstOrDefault();

        public GuardarVueloResponse GuardarVuelo(Vuelo vuelo) {

            try
            {
                if (vuelo.CiudadOrigenId == vuelo.CiudadDestinoId)
                {
                    return new GuardarVueloResponse("La ciudades de origen y destino no pueden ser las mismas. ");
                }

                _context.Vuelos?.Add(vuelo);
                _context.SaveChanges();

                return new GuardarVueloResponse(vuelo);
            }
            catch (System.Exception)
            {
                return new GuardarVueloResponse("Error en la base de datos. ");
            }
        }
    }
    
    public class GuardarVueloResponse 
    {
        public GuardarVueloResponse(string mensaje)
        {
            this.Error = true;
            this.Mensaje = mensaje;
        }

        public GuardarVueloResponse(Vuelo vuelo)
        {
            this.Error = false;
            this.Vuelo = vuelo;
        }

        public bool Error { get; set; }
        public string? Mensaje { get; set; }
        public Vuelo? Vuelo { get; set; }
    }
}