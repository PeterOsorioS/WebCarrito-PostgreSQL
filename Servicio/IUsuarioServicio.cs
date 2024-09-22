using Entidad;
using Microsoft.AspNetCore.Mvc;

namespace Servicio
{
    public interface IUsuarioServicio
    {
        public void AgregarUsuario(Usuario usuario);
        public Task<IActionResult> ActualizarUsuario(Usuario usuario);
        public Task<IActionResult> EliminarUsuario(int id);
        public Task<Usuario> VerificarUsuario(string correo, string clave);
        public Task<bool> ExistenciaCorreo(Usuario usuario);
    }
}
