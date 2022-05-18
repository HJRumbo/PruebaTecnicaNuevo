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
    public class AerolineaController : ControllerBase
    {
        private AerolineaService _aerolineaService;
        public AerolineaController(AplicacionDBContext context)
        {
            _aerolineaService = new AerolineaService(context);

        }

        [HttpGet]
        public ActionResult<IEnumerable<AerolineaViewModel>>? Get()
        {
            var aerolineas = _aerolineaService.ConsultarTodas()?
                .Select(a => new AerolineaViewModel(a));

            if (aerolineas?.Count() == 0) 
                return NotFound("No se encontró ninguna aerolinea registrada. ");

            return Ok(aerolineas);
        }
    }
}