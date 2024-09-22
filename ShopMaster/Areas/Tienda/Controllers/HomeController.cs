using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Servicio;
using System.Security.Claims;

namespace ShopMaster.Areas.Tienda.Controllers
{
    [Area("Tienda")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IHomeServicio _homeServicio;
        public HomeController(IContenedorTrabajo contenedorTrabajo, IHomeServicio homeServicio)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _homeServicio = homeServicio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.IsHome = true;
            return View(_homeServicio.VistaInicio());
        }



        [HttpGet]
        public IActionResult Productos(List<int> CategoriasFiltradas = null)
        {
            ViewBag.CategoriasFiltradas = CategoriasFiltradas;

            var productos = _homeServicio.VistaProductos(CategoriasFiltradas);

            return View(productos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuscarProductosPorFiltro(List<int> CategoriasFiltradas, List<int> MarcasFiltradas)
        {
            var productosFiltrados = _homeServicio.ProductosFiltrados(CategoriasFiltradas, MarcasFiltradas);
            return PartialView("_ProductoPartial", productosFiltrados);
        }

        [HttpGet]
        public async Task<IActionResult> DetalleProducto(int id)
        {
            var producto = await _homeServicio.ObtenerProductoPorID(id);
            return PartialView("_ModalDetalleProductos", producto);
        }

        [HttpGet]
        public async Task<IActionResult> AgregarProductosCarrito(int id)
        {
            var productoCarrito = await _homeServicio.EnviarProductoAlCarrito(id);
            return Json(productoCarrito);
        }


        [HttpGet]
        public IActionResult Carrito()
        {
            if (User.Identity.IsAuthenticated)
            {
                var carrito = _homeServicio.InformacionVistaCarrito();
                return View(carrito);
            }
            return RedirectToAction("Login", "Auth", new { area = "Acceso" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarVenta([FromBody] Venta venta)
        {
            if (ModelState.IsValid)
            {
                var nuevaVenta = await _homeServicio.RealizarCompraCarrito(venta);
                return new OkObjectResult(new { success = true, idVenta = nuevaVenta.IdVenta });
            }

            return new BadRequestObjectResult(new { success = false, message = "Datos inválidos" });
        }


        [HttpGet]
        public async Task<IActionResult> ResumenCompra(int id)
        {
            var venta = await _homeServicio.DatosVenta(id);

            return View(venta);
        }

        [HttpGet]
        public IActionResult MisCompras()
        {
            int idusuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine(idusuario);
            var usuario = User.FindFirstValue(ClaimTypes.Name);

            var ventas = _contenedorTrabajo.Venta.GetAll().Where(v => v.IdUsuario == idusuario);
            return View(ventas);
        }
    }
}
