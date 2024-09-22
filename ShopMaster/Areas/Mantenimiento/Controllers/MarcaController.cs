using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicio;

namespace ShopMaster.Areas.Mantenimiento.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Area("Mantenimiento")]
    public class MarcaController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMarcaServicio _marcaServicio;
        public MarcaController(IContenedorTrabajo contenedorTrabajo, IMarcaServicio marcaServicio)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _marcaServicio = marcaServicio;
        }

        #region Metodos GET

        [HttpGet]
        public IActionResult GestionMarca()
        {
            return View(_contenedorTrabajo.Marca.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_ModalCreateMarca");
        }

        [HttpGet]
        public async Task<IActionResult> EditMarca(int id)
        {
            var marca = await _marcaServicio.GetCategoriaByIdAsync(id);
            return PartialView("_ModalEditMarca", marca);
        }

        #endregion

        #region Metodos POST y PUT

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Marca marca)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return _marcaServicio.CrearMarca(marca);
                }
                catch (DbUpdateException ex)
                {
                    // Registra el error interno
                    return Json(new { success = false, errors = ex.InnerException?.Message ?? "Error al guardar los cambios." });
                }
            }
            return Json(new { success = false, errors = "Error en los datos proporcionados." });
        }


        [HttpPut]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public IActionResult EditMarca([FromBody] Marca marca)
        {
            if (ModelState.IsValid)
            {
                return _marcaServicio.ActualizarMarca(marca);
            }
            else
            {
                return Json(new { success = true, message = "Error en los datos proporcionados." });
            }
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _marcaServicio.EliminarMarca(id);
        }

        #endregion
    }
}