using Datos;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models;

namespace Presentacion.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private CiudadService _ciudadService; 
        public CiudadController(AplicacionDBContext context)
        {
            _ciudadService = new CiudadService(context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CiudadViewModel>>? Get() {
            var ciudades = _ciudadService.ConsultarTodas()?
                .Select(c => new CiudadViewModel(c));

            if (ciudades?.Count() == 0)
                return NotFound("No se encontró ninguna ciudad registrada. ");

            return Ok(ciudades);
        }
    }
}