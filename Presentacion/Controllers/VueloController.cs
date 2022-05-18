using Datos;
using Entidades;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VueloController : ControllerBase
    {
        private VueloService _vueloService; 
        public VueloController(AplicacionDBContext context)
        {
            _vueloService = new VueloService(context);
        }

        [Authorize(Roles = "ADMIN, CLIENTE")]
        [HttpGet]
        public ActionResult<IEnumerable<VueloViewModel>>? Get()
        {
            var vuelos = _vueloService.ConsultarTodos()?
                .Select(v => new VueloViewModel(v));

            if (vuelos?.Count() == 0)
                return NotFound("No se encontró ningún vuelo registrado. ");

            return Ok(vuelos);
        }

        [Authorize(Roles = "ADMIN, CLIENTE")]
        [HttpGet("{id}")]
        public ActionResult<VueloViewModel> Get(int id) {
            var vuelo = _vueloService.ConsultarVuelo(id);

            if (vuelo == null)
                return NotFound("No se encontró ningún vuelo registrado. ");

            return Ok(new VueloViewModel(vuelo));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult<VueloViewModel> Post(VueloInputModel vueloInput) {

            var vuelo = MapearVuelo(vueloInput);

            var respuesta = _vueloService.GuardarVuelo(vuelo);

            if (respuesta.Error)
            {
                ModelState.AddModelError("Guardar vuelo", respuesta.Mensaje!);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status500InternalServerError,
                };

                return StatusCode(500, problemDetails);
            }

            return Ok(new VueloViewModel(respuesta.Vuelo!));
        }

        [NonAction]
        public Vuelo MapearVuelo(VueloInputModel vueloInput) {
            
            var vuelo = new Vuelo(){
                CiudadOrigenId = vueloInput.CiudadOrigenId,
                CiudadDestinoId = vueloInput.CiudadDestinoId,
                Fecha = DateTime.Parse(vueloInput.Fecha!),
                HoraSalida = DateTime.Parse(vueloInput.HoraSalida!),
                HoraLlegada = DateTime.Parse(vueloInput.HoraLlegada!),
                AerolineaId = vueloInput.AerolineaId,
                EstadoVueloId = vueloInput.EstadoVueloId
            };

            return vuelo;
        }
    }
}