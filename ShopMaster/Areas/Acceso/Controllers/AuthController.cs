using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace ShopMaster.Areas.Acceso.Controllers
{
    [Area("Acceso")]
    public class AuthController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IAuthServicio _authServicio;
        public AuthController(IUsuarioServicio usuarioServicio, IContenedorTrabajo contenedorTrabajo, IAuthServicio authServicio)
        {
            _usuarioServicio = usuarioServicio;
            _contenedorTrabajo = contenedorTrabajo;
            _authServicio = authServicio;
        }


        #region Metodos Get

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Resumen" });
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Usuarios", new { area = "Resumen" });
            }
            return View();
        }

        #endregion


        #region Metodos Post

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {          
            return await _authServicio.IniciarSesion(usuario);
        }



        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (await _usuarioServicio.ExistenciaCorreo(usuario))
            {
                ModelState.AddModelError("Correo", "El correo electrónico ya está registrado.");
                return View(usuario);
            }

            if (ModelState.IsValid)
            {
                await _authServicio.Registrar(usuario);
                return RedirectToAction("Index", "Home", new { area = "Tienda" });
            }

            return View(usuario);
        }

        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "Tienda" });
        }
    }
}
