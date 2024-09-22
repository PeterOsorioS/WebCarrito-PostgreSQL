using AccesoDatos.Data.Repository.IRepository;
using Entidad.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace ShopMaster.Areas.Mantenimiento.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Area("Mantenimiento")]
    public class ProductosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProductoServicio _productoServicio;
        public ProductosController(IContenedorTrabajo contenedorTrabajo,
            IWebHostEnvironment hostEnvironment, IProductoServicio productoServicio)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostEnvironment;
            _productoServicio = productoServicio;
        }
        #region Metodos GET


        [HttpGet]
        public IActionResult GestionProducto()
        {
            var producto = _contenedorTrabajo.Productos.GetAll(includeProperties: "Marca,Categoria");
            return View(producto);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var productosVm = _productoServicio.ObtenerProductoVm();
            return PartialView("_ModalCreateProducto", productosVm);
        }


        [HttpGet]
        public async Task<ActionResult> EditProducto(int id)
        {
            var productosVm = await _productoServicio.ObtenerProductoVmPorId(id);
            return PartialView("_ModalEditProducto", productosVm);
        }


        #endregion

        #region Metodos POST y PUT


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoVm ProductoVm)
        {
            if (ModelState.IsValid)
            {
                var archivo = Request.Form.Files.FirstOrDefault();
                return _productoServicio.CrearProducto(ProductoVm.producto, archivo);
            }
            else
            {
                return Json(new { success = false, message = "Error al guardar los cambios" });
            }
        }


        [HttpPut]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProducto([FromForm] ProductoVm ProductoVm)
        {
            if (ModelState.IsValid)
            {
                var archivo = Request.Form.Files.FirstOrDefault();
                return await _productoServicio.ActualizarProducto(ProductoVm.producto, archivo);
            }

            return Json(new { success = false, message = "Error al actualizar: datos no válidos" });
        }

        #endregion

        #region Delete
        [Authorize(Roles = "Administrador")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return await _productoServicio.EliminarProducto(id);
        }
        #endregion
    }
}
