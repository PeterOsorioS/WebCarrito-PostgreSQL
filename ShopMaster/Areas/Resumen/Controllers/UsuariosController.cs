using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Entidad.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicio;
using System.Security.Claims;

namespace ShopMaster.Areas.Resumen.Controllers
{
    [Area("Resumen")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly ILogger<UsuariosController> _logger;
        public UsuariosController(IContenedorTrabajo contenedorTrabajo, IUsuarioServicio usuarioServicio, ILogger<UsuariosController> logger)
        {

            _contenedorTrabajo = contenedorTrabajo;
            _usuarioServicio = usuarioServicio;
            _logger = logger;
        }

        #region Metodos GET

        [HttpGet]
        public async Task<IActionResult> GestionUsuario()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var usuarioActualNombre = claimsIdentity.FindFirst(ClaimTypes.Name).Value;

            // Aquí se asume que GetAll() es un método asincrónico
            var usuarios = await _contenedorTrabajo.Usuario.GetAllAsync(u => u.Nombres != usuarioActualNombre && u.IdRol != 1, includeProperties: "Rol");
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UsuarioVm()
            {
                rol = _contenedorTrabajo.Rol.GetLista()
            };
            return PartialView("_ModalCreateUsuario", model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Soporte,Cliente")]
        public async Task<IActionResult> EditUsuario(int id)
        {
            var usuario = await _contenedorTrabajo.Usuario.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var usuarioVm = new UsuarioVm()
            {
                usuario = usuario,
                rol = _contenedorTrabajo.Rol.GetLista()
            };
            return PartialView("_ModalEditUsuario", usuarioVm);
        }

        [HttpGet]
        public async Task<IActionResult> Perfil(int id)
        {
            var usuarioInformacion = await _contenedorTrabajo.Usuario.Get(id);
            return PartialView("_ModalPerfilUsuario", usuarioInformacion);
        }


        #endregion


        #region Metodos POST y PUT

        [HttpPost]
        [Authorize(Roles = "Administrador,Soporte")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioVm usuariovm)
        {
            if (await _usuarioServicio.ExistenciaCorreo(usuariovm.usuario))
            {
                ModelState.AddModelError("Correo", "El correo electronico ya esta registrado.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _usuarioServicio.AgregarUsuario(usuariovm.usuario);
                    return Json(new { success = true, message = "Usuario creado correctamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = "Error al guardar los cambios." });
                }
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage))
                    .ToList();


                return Json(new { success = false, errors });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsuario([FromForm] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                return await _usuarioServicio.ActualizarUsuario(usuario);
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage))
                    .ToList();


                return Json(new { success = false, errors });
            }
        }


        #endregion


        #region Delete

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _usuarioServicio.EliminarUsuario(id);
        }



        #endregion
    }
}

