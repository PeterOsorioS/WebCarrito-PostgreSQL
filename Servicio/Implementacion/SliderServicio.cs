using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Servicio.Implementacion
{
    public class SliderServicio : ISliderServicio
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderServicio(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult CrearSlider(Slider slider, IFormFile archivo)
        {
            try
            {
                slider.RutaImagen = SubirImagen(slider, archivo);
                _contenedorTrabajo.Slider.Add(slider);
                _contenedorTrabajo.Save();
                return new OkObjectResult(new { success = true, messagge = "Slider creado correctamente." });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al guardar los cambios." });
            }
        }
        public async Task<IActionResult> ActualizarSlider(Slider slider, IFormFile archivo)
        {
            try
            {
                slider.RutaImagen = await EditarImagen(slider, archivo);
                _contenedorTrabajo.Slider.update(slider);
                _contenedorTrabajo.Save();
                return new OkObjectResult(new {success = true, message = "Slider actualizado correctamente." });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al actualizar el slider." });
            }
        }

        public async Task<IActionResult> EliminarSlider(int id)
        {
            try
            {
                var slider = await _contenedorTrabajo.Slider.Get(id);
                if (slider == null)
                {
                    return new BadRequestObjectResult(new { success = false, message = "Slider no encontrado" });
                }
                EliminarImagen(slider);
                await _contenedorTrabajo.Slider.Remove(slider);
                await _contenedorTrabajo.SaveAsync();
                return new OkObjectResult(new { success = true, message = "Slider Eliminado" });
            }
            catch (Exception)
            {
                return new OkObjectResult(new { success = false, message = "Slider no Eliminado" });
            }

        }
        public string SubirImagen(Slider slider, IFormFile archivo)
        {
            if (slider.ID == 0 && archivo != null)
            {
                string rutaPrincipal = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes", "sliders");
                string nombreArchivo = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(archivo.FileName);

                // Asegúrate de que el directorio exista
                if (!Directory.Exists(rutaPrincipal))
                {
                    Directory.CreateDirectory(rutaPrincipal);
                }

                // Guarda el archivo en el servidor
                string rutaArchivo = Path.Combine(rutaPrincipal, nombreArchivo + extension);
                using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    archivo.CopyTo(fileStream);
                }

                return slider.RutaImagen = @"\imagenes\sliders\" + nombreArchivo + extension;
            }
            throw new InvalidOperationException("La Imagen es obligatoria");
        }

        public async Task<string> EditarImagen(Slider slider, IFormFile archivo)
        {
            var SliderDb = await _contenedorTrabajo.Slider.Get(slider.ID);
            if (archivo != null)
            {
                string rutaPrincipal = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes", "sliders");
                string nombreArchivo = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(archivo.FileName);

                // Verificar si existe una imagen anterior y eliminarla
                if (!string.IsNullOrEmpty(slider.RutaImagen))
                {
                    string rutaImagenAnterior = Path.Combine(_webHostEnvironment.WebRootPath, slider.RutaImagen.TrimStart('\\'));
                    if (File.Exists(rutaImagenAnterior))
                    {
                        File.Delete(rutaImagenAnterior);
                    }
                }

                // Asegúrate de que el directorio exista
                if (!Directory.Exists(rutaPrincipal))
                {
                    Directory.CreateDirectory(rutaPrincipal);
                }

                // Subir la nueva imagen
                string rutaArchivo = Path.Combine(rutaPrincipal, nombreArchivo + extension);
                using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    archivo.CopyTo(fileStream);
                }

                // Actualizar la ruta de la imagen en el producto
                slider.RutaImagen = @"\imagenes\sliders\" + nombreArchivo + extension;
                return slider.RutaImagen;
            }
            else
            {
                slider.RutaImagen = SliderDb.RutaImagen;
                return slider.RutaImagen;
            }
        }


        public void EliminarImagen(Slider slider)
        {
            string rutaPrincipal = _webHostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaPrincipal, slider.RutaImagen.TrimStart('\\'));
            if (File.Exists(rutaImagen))
            {
                File.Delete(rutaImagen);
            }
        }
    }
}
