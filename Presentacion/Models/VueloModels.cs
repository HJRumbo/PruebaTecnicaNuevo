using System.ComponentModel.DataAnnotations;
using Entidades;

namespace Presentacion.Models
{
    public class VueloInputModel
    {
        [Required(ErrorMessage = "La ciudad de origen es requerida")]
        public int? CiudadOrigenId { get; set; }

        [Required(ErrorMessage = "La ciudad de destino es requerida")]
        public int? CiudadDestinoId { get; set; }

        [Required(ErrorMessage = "La fecha de salida es requerida")]
        [RegularExpression(@"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$", 
            ErrorMessage = "El formato debe ser de Fecha aaaa-mm-dd")]
        [ValidarFecha( ErrorMessage="La fecha debe ser mayor a la hoy")]

        public string? Fecha { get; set; }

        [Required(ErrorMessage = "La hora de salida es requerida")]
        [RegularExpression(@"([01]?[0-9]|2[0-3]):[0-5][0-9]$", 
            ErrorMessage = "El formato debe ser de sistema horario de 24 horas hh:mm")]
        public string? HoraSalida { get; set; }

        [Required(ErrorMessage = "La hora de llegada es requerida")]
        [RegularExpression(@"([01]?[0-9]|2[0-3]):[0-5][0-9]$", 
            ErrorMessage = "El formato debe ser de sistema horario de 24 horas hh:mm")]
        public string? HoraLlegada { get; set; }

        [Required(ErrorMessage = "La aerolinea es requerida")]
        public int? AerolineaId { get; set; }

        [Required(ErrorMessage = "El estado del vuelo es requerido")]
        public int? EstadoVueloId { get; set; }
    }

    public class VueloViewModel : VueloInputModel
    {
        public VueloViewModel(Vuelo vuelo)
        {
            this.NumeroVuelo = vuelo.NumeroVuelo;
            this.CiudadOrigen = vuelo.CiudadOrigen;
            this.CiudadDestino = vuelo.CiudadDestino;
            this.Fecha = vuelo.Fecha.ToString("yyyy-MM-dd");
            this.HoraSalida = vuelo.HoraSalida.ToShortTimeString();
            this.HoraLlegada = vuelo.HoraLlegada.ToShortTimeString();
            this.Aerolinea = vuelo.Aerolinea;
            this.EstadoVuelo = vuelo.EstadoVuelo;
        }

        public int NumeroVuelo { get; set; }
        public Ciudad? CiudadOrigen { get; set; }
        public Ciudad? CiudadDestino { get; set; }
        public Aerolinea? Aerolinea { get; set; }
        public Estado? EstadoVuelo { get; set; }
    }

    public class ValidarFecha : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateTime.Parse(value?.ToString()!) >= DateTime.Now.Date)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }

}