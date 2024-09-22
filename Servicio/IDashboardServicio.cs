using Entidad;
using Entidad.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Servicio
{
    public interface IDashboardServicio
    {
        public DashboardVm ObtenerDashboard();
        public IEnumerable<Venta> ObtenerTodasLasVentas();
        public IEnumerable<Venta> BuscarVentasPorFechas(string fechainicio, string fechafin);
        public FileContentResult ExportarVentasAExcel(string fechainicio, string fechafin);
    }
}
