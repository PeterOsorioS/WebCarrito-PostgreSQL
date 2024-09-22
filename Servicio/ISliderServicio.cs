using Entidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Servicio
{
    public interface ISliderServicio
    {
        public IActionResult CrearSlider(Slider slider, IFormFile archivo);
        public Task<IActionResult> ActualizarSlider(Slider slider, IFormFile archivo);
        public Task<IActionResult> EliminarSlider(int id);
        public string SubirImagen(Slider slider, IFormFile archivo);
        public Task<string> EditarImagen(Slider slider, IFormFile archivo);
        public void EliminarImagen(Slider slider);
    }
}
