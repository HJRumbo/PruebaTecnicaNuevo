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
    public class EstadoController : ControllerBase
    {
        private EstadoService _estadoService; 
        public EstadoController(AplicacionDBContext context)
        {
            _estadoService = new EstadoService(context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<EstadoViewModel>>? Get() {
            var estados = _estadoService.ConsultarTodas()?
                .Select(e => new EstadoViewModel(e));

            if (estados?.Count() == 0)
                return NotFound("No se encontró ningún estado registrado. ");

            return Ok(estados);
        }
        
    }
}