using AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopMaster.Areas.Complementos.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Area("Complementos")]
    public class RolController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public RolController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult GestionRol()
        {
            return View(_contenedorTrabajo.Rol.GetAll());
        }
    }
}
