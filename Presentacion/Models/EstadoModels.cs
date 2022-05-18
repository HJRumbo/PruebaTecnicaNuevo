using Entidades;

namespace Presentacion.Models
{
    public class EstadoViewModel
    {
        public EstadoViewModel(Estado estado)
        {
            this.Id = estado.Id;
            this.Nombre = estado.Nombre;
        }
        public int Id { get; set; }
        
        public string? Nombre { get; set; }
    }
}