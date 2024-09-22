using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace ShopMaster.Areas.Complementos.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Area("Complementos")]
    public class SliderController : Controller
    {
        private readonly ISliderServicio _sliderServicio;
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public SliderController(IContenedorTrabajo contenedorTrabajo, ISliderServicio sliderServicio)
        {
            _sliderServicio = sliderServicio;
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult GestionSlider()
        {
            return View(_contenedorTrabajo.Slider.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_ModalCreateSlider");
        }

        [HttpGet]
        public async Task<IActionResult> EditSlider(int id)
        {
            var Slider = await _contenedorTrabajo.Slider.Get(id);
            return PartialView("_ModalEditSlider", Slider);
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                var archivo = Request.Form.Files.FirstOrDefault();
                return _sliderServicio.CrearSlider(slider, archivo);
            }
            else
            {
                return new JsonResult(new { success = false, message = "Error al crear el Slider" });
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSlider([FromForm] Slider slider) 
        { 
            if (ModelState.IsValid)
            {
                var archivo = Request.Form.Files.FirstOrDefault();
                return await _sliderServicio.ActualizarSlider(slider, archivo);
            }

            return Json(new { success = false, message = "Error al actualizar: datos no válidos" });

        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _sliderServicio.EliminarSlider(id);
        }
    }
}
