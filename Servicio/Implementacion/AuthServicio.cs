using AccesoDatos.Data.Repository.IRepository;
using DocumentFormat.OpenXml.Spreadsheet;
using Entidad;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Servicio.Implementacion
{
    public class AuthServicio : IAuthServicio
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthServicio(IContenedorTrabajo contenedorTrabajo,
            IUsuarioServicio usuarioServicio, IHttpContextAccessor httpContextAccessor)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _usuarioServicio = usuarioServicio;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(List<Claim> claims, string role)> AutenticarUsuario(Usuario usuario)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombres ?? string.Empty),
                        new Claim("correo", usuario.Correo ?? string.Empty),
                        new Claim(ClaimTypes.Role, usuario.Rol?.Descripcion ?? "Usuario"),
                        new Claim(ClaimTypes.Actor, $"{usuario.Nombres} {usuario.Apellidos}"),
                        new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString())
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            var role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;

            return (claims, role);
        }

        public async Task<IActionResult> IniciarSesion(Usuario usuario)
        {
            try
            {
                var user = await _usuarioServicio.VerificarUsuario(usuario.Correo, usuario.Clave);
                if (user == null)
                {
                    return new OkObjectResult(new { success = false, message ="Correo o contraseña incorrectos." });
                }

                else if (!user.Activo)
                {
                    return new OkObjectResult(new { success = false, message = "Tu cuenta está desactivada"});
                }

                var (claims, role) = await AutenticarUsuario(user);

                var redirectUrl = role == "Cliente" ? "/Tienda/Home/Index" : "/Resumen/Dashboard/Index";

                return new OkObjectResult (new { success = true, redirectUrl });
            }
            catch (Exception)
            {
                return new OkObjectResult(new { success = false, message = "Error al iniciar sesion" });
            }
        }
        public async Task<string> Registrar(Usuario usuario)
        {
            try
            {
                usuario.IdRol = 2; // Asignar rol
                _usuarioServicio.AgregarUsuario(usuario); // Agregar usuario
                usuario.Rol = await ObtenerRolPorId(usuario.IdRol); // Obtener rol

                var (claims, role) = await AutenticarUsuario(usuario); // Autenticar

                return (role); // Retornar éxito y rol
            }
            catch (Exception)
            {
                return null; // Retornar fallo
            }
        }
        public async Task<Rol> ObtenerRolPorId(int idRol)
        {
            return await _contenedorTrabajo.Rol.GetFirstOrDefault(r => r.IdRol == idRol);
        }

    }
}
