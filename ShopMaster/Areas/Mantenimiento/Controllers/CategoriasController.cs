using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace ShopMaster.Areas.Mantenimiento.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Area("Mantenimiento")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ICategoriaServicio _categoriaServicio;
        public CategoriasController(IContenedorTrabajo contenedorTrabajo, ICategoriaServicio categoriaServicio)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _categoriaServicio = categoriaServicio;
        }


        #region Metodos GET

        [HttpGet]
        public IActionResult GestionCategoria()
        {
            return View(_contenedorTrabajo.Categoria.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_ModalCreateCategoria");
        }

        [HttpGet]
        public async Task<IActionResult> EditCategoria(int id)
        {
            var categoria = await _categoriaServicio.GetCategoriaByIdAsync(id);
            return PartialView("_ModalEditCategoria", categoria);
        }

        #endregion


        #region Metodos POST y PUT

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var archivo = Request.Form.Files.FirstOrDefault();
                return _categoriaServicio.CrearCategoria(categoria, archivo);
            }
            else
            {
                return Json(new { success = false, message = "Error en los datos proporcionados." });
            }

        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategoria([FromForm] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var archivo = Request.Form.Files.FirstOrDefault();
                return await _categoriaServicio.ActualizarCategoria(categoria, archivo);
            }
            else
            {
                return new BadRequestObjectResult(new { success = true, message = "Error al guardar los cambios" });
            }
        }

        #endregion


        #region Delete

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _categoriaServicio.EliminarCategoria(id);
        }
        #endregion
    }
}
