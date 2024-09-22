using Entidad;
using Entidad.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Servicio
{
    public interface IProductoServicio
    {
        public ProductoVm ObtenerProductoVm();
        public Task<ProductoVm> ObtenerProductoVmPorId(int id);
        public IActionResult CrearProducto(Producto producto, IFormFile archivo);
        public Task<IActionResult> ActualizarProducto(Producto producto, IFormFile archivo);
        public Task<IActionResult> EliminarProducto(int id);
        public string SubirImagen(Producto producto, IFormFile archivo);
        public Task<string> EditarImagen(Producto producto, IFormFile archivo);
        public void EliminarImagen(Producto producto);
    }
}
