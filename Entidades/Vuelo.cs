using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Vuelo
    {
        [Key]
        public int NumeroVuelo { get; set; }
        
        [ForeignKey("Ciudad")]
        public int? CiudadOrigenId { get; set; }
        public Ciudad? CiudadOrigen { get; set; }

        [ForeignKey("Ciudad")]
        public int? CiudadDestinoId { get; set; }
        public Ciudad? CiudadDestino { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraSalida { get; set; }
        public DateTime HoraLlegada { get; set; }

        [ForeignKey("Aerolinea")]
        public int? AerolineaId { get; set; }
        public Aerolinea? Aerolinea { get; set; }

        [ForeignKey("Estado")]
        public int? EstadoVueloId { get; set; }
        public Estado? EstadoVuelo { get; set; }
    }
}