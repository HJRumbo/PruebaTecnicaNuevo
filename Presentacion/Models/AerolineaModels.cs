using Entidades;

namespace Presentacion.Models
{
    public class AerolineaViewModel
    {
        public AerolineaViewModel(Aerolinea aerolinea)
        {
            this.Id = aerolinea.Id;
            this.Nombre = aerolinea.Nombre;
        }

        public int Id { get; set; }
        
        public string? Nombre { get; set; }
    }
}