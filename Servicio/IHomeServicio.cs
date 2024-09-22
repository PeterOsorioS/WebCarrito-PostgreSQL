using Entidad;
using Entidad.ViewModel;

namespace Servicio
{
    public interface IHomeServicio
    {
        public HomeVm VistaInicio();
        public ProductFilterVM VistaProductos(List<int> CategoriasFiltradas = null);
        public ProductFilterVM ProductosFiltrados(List<int> CategoriasFiltradas, List<int> MarcasFiltradas);
        public Task<Producto> ObtenerProductoPorID(int id);
        public Task<Venta> ObtenerVentaPorID(int id);
        public List<Detalle_Venta> ObtenerDetallesVentaPorVentaID(int idVenta);
        public Task<Producto> EnviarProductoAlCarrito(int id);
        public CarritoVm InformacionVistaCarrito();
        public Task<Venta> RealizarCompraCarrito(Venta venta);
        public Task<Venta> DatosVenta(int id);
        public Task<Venta> RegistrarVenta(Venta venta);
        public Task RegistrarDetallesVenta(Venta venta, Venta nuevaVenta);
    }
}
