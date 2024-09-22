using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entidad.ViewModel
{
    public class UsuarioVm
    {
        public Usuario usuario { get; set; }
        public IEnumerable<SelectListItem> rol { get; set; }
    }
}
