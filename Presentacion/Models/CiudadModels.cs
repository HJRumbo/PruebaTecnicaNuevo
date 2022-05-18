using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;

namespace Presentacion.Models
{
    public class CiudadViewModel
    {
        public CiudadViewModel(Ciudad ciudad)
        {
            this.Id = ciudad.Id;
            this.Nombre = ciudad.Nombre;
        }
        public int Id { get; set; }
        
        public string? Nombre { get; set; }
    }
}