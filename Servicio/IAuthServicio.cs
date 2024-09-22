using Entidad;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Servicio
{
    public interface IAuthServicio
    {

        public Task<(List<Claim> claims, string role)> AutenticarUsuario(Usuario usuario);
        public Task<IActionResult> IniciarSesion(Usuario usuario);
        public Task<string> Registrar(Usuario usuario);
    }
}
