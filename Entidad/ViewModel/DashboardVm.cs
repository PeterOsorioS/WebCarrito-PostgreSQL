namespace Entidad.ViewModel
{
    public class DashboardVm
    {
        public int TotalClientes { get; set; }
        public int TotalVentas { get; set; }
        public int TotalProductos { get; set; }
        public IEnumerable<Venta> HistorialVentas { get; set; }
    }
}
