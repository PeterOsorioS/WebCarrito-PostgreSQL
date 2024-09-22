using AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace ShopMaster.Areas.Resumen.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Area("Resumen")]
    public class DashboardController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IDashboardServicio _DashboardServicio;
        public DashboardController(IContenedorTrabajo contenedorTrabajo, IDashboardServicio DashboardServicio)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _DashboardServicio = DashboardServicio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var DashboardDatos = _DashboardServicio.ObtenerDashboard();
            return View(DashboardDatos);
        }


        #region Llamados a la API

        [HttpPost]
        public IActionResult Buscar(string fechainicio, string fechafin)
        {
            var historialVentasFiltrado = _DashboardServicio.BuscarVentasPorFechas(fechainicio, fechafin);

            return PartialView("_TablaHistorialVentas", historialVentasFiltrado);
        }


        [HttpPost]
        public IActionResult ExportarVenta(string fechainicio, string fechafin)
        {
            return _DashboardServicio.ExportarVentasAExcel(fechainicio, fechafin);
        }
    }
}

#endregion