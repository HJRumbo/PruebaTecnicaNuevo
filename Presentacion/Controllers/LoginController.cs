using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datos;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentacion.Config;
using Presentacion.Models;
using Presentacion.Service;

namespace Presentacion.Controllers
{
    [Authorize(Roles = "ADMIN, CLIENTE")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private UsuarioService usuarioService;
        JwtService _jwtService;
        public LoginController(AplicacionDBContext context, IOptions<AppSetting> appSettings) {
            usuarioService = new UsuarioService(context);
            _jwtService = new JwtService(appSettings);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<LoginViewModel> Post([FromBody] LoginInputModel model)
        {
            var usuario = usuarioService.ValidarCredenciales(model.NombreUsuario!, model.Contrasena!);

            if (usuario == null)
                return NotFound("Usuario o contraseña inválidos.");

            var respuesta = _jwtService.GenerarToken(usuario);

            return Ok(respuesta);
        }

        [HttpGet("RenovarToken")]
        public ActionResult<LoginViewModel> RenovarToken() 
        {
            string token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;

            var nombreUsuario = _jwtService.VerificarToken(token);

            if( nombreUsuario == null )
                return Unauthorized("Token no válido 1" );

            var usuario = usuarioService.ConsulterUsuario(nombreUsuario);

            if(usuario == null)
                return Unauthorized("Token no válido 2");

            var respuesta = _jwtService.GenerarToken(usuario);

            return Ok(respuesta);
        }
    }
}