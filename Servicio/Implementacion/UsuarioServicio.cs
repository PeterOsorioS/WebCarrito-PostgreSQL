using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Mvc;


namespace Servicio.Implementacion
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public UsuarioServicio(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        public void AgregarUsuario(Usuario usuario)
        {
            try
            {
                usuario.Activo = true;
                usuario.Clave = _contenedorTrabajo.Usuario.ConvertirSha256(usuario.Clave);
                _contenedorTrabajo.Usuario.Add(usuario);
                _contenedorTrabajo.Save();
            }
            catch
            {
                throw;
            }

        }

        public async Task<IActionResult> ActualizarUsuario(Usuario usuario)
        {
            try
            {
                _contenedorTrabajo.Usuario.Update(usuario);
                await _contenedorTrabajo.SaveAsync();
                return new OkObjectResult(new { success = true, message = "Usuario actualizado correctamente" });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al guardar los cambios." });
            }
        }
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                var usuarioDb = await _contenedorTrabajo.Usuario.Get(id);
                if (usuarioDb == null)
                {
                    return new BadRequestObjectResult(new { success = false, message = "No se encontró el usuario" });
                }
                await _contenedorTrabajo.Usuario.Remove(usuarioDb);
                await _contenedorTrabajo.SaveAsync();

                return new OkObjectResult(new { success = true, message = "Usuario Eliminado" });

            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al eliminar" });
            }
        }

        public async Task<Usuario> VerificarUsuario(string Correo, string Clave)
        {
            var usuario = await _contenedorTrabajo.Usuario.BuscarUsuario(Correo, _contenedorTrabajo.Usuario.ConvertirSha256(Clave));

            if (usuario != null)
            {
                return usuario;
            }

            return null;

        }
        public async Task<bool> ExistenciaCorreo(Usuario usuario)
        {
            try
            {
                var usuarioExistente = await _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.Correo == usuario.Correo);
                if (usuarioExistente != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
