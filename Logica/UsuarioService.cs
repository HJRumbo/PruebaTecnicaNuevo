using Datos;
using Entidades;

namespace Logica
{
    public class UsuarioService
    {
        private readonly AplicacionDBContext _context;
        public UsuarioService(AplicacionDBContext context)=> _context = context;
        public Usuario? ValidarCredenciales(string nombreUsuario, string contrasena) => _context.Usuarios?
            .FirstOrDefault(t => t.NombreUsuario == nombreUsuario.ToUpper() && t.Contrasena == contrasena);

        public Usuario? ConsulterUsuario(string nombreUsuario) => _context.Usuarios?
            .FirstOrDefault(t => t.NombreUsuario == nombreUsuario);
    }
}