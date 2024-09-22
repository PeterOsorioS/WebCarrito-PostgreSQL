using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entidad.ViewModel
{
    public class CarritoVm
    {
        public IEnumerable<SelectListItem> Municipios { get; set; }
        public IEnumerable<SelectListItem> Departamenots { get; set; }
    }
}
