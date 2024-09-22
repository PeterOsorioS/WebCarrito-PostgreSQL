using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entidad.ViewModel
{
    public class ProductoVm
    {
        public Producto producto { get; set; }
        public IEnumerable<SelectListItem> ListaMarcas { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
