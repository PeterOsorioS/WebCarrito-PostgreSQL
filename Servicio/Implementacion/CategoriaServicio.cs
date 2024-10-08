using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace Servicio.Implementacion
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoriaServicio(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            try
            {
                var categoria = await _contenedorTrabajo.Categoria.Get(id);
                return categoria;
            }
            catch (Exception e)
            {

                throw new ApplicationException("Error al obtener la categoría.", e);
            }
        }

        public IActionResult CrearCategoria(Categoria categoria, IFormFile archivo)
        {
            try
            {
                categoria.FechaRegistro = DateTime.UtcNow;
                categoria.RutaImagen = SubirImagen(categoria, archivo);
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return new OkObjectResult(new { success = true, message = "Categoría creada correctamente." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Error al guardar los cambios." });
            }

        }
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            try
            {
                var categoria = await _contenedorTrabajo.Categoria.Get(id);
                if (categoria == null)
                {
                    return new BadRequestObjectResult(new { success = false, message = "No se encontró el usuario." });
                }
                EliminarImagen(categoria);
                await _contenedorTrabajo.Categoria.Remove(categoria);
                await _contenedorTrabajo.SaveAsync();
                return new OkObjectResult(new { success = true, message = "Categoria Eliminada." });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al eliminar la categoria." });
            }

        }

        public async Task<IActionResult> ActualizarCategoria(Categoria categoria, IFormFile archivo)
        {
            try
            {
                categoria.RutaImagen = await EditarImagen(categoria, archivo);
                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();
                return new OkObjectResult(new { success = true, message = "Categoría actualizada correctamente." });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al actualizar la categoría." });
            }

        }
        public string SubirImagen(Categoria categoria, IFormFile archivo)
        {
            if (categoria.IdCategoria == 0 && archivo != null)
            {
                string extension = Path.GetExtension(archivo.FileName).ToLower();


                string rutaPrincipal = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes", "categorias");
                string nombreArchivo = Guid.NewGuid().ToString();

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

                return categoria.RutaImagen = @"\imagenes\categorias\" + nombreArchivo + extension;
            }
            throw new InvalidOperationException("La Imagen es obligatoria");
        }

        public async Task<string> EditarImagen(Categoria categoria, IFormFile archivo)
        {
            var CategoriaDb = await _contenedorTrabajo.Categoria.Get(categoria.IdCategoria);
            if (archivo != null)
            {
                string extension = Path.GetExtension(archivo.FileName).ToLower();
                string rutaPrincipal = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes", "categorias");
                string nombreArchivo = Guid.NewGuid().ToString();

                // Verificar si existe una imagen anterior y eliminarla
                if (!string.IsNullOrEmpty(categoria.RutaImagen))
                {
                    string rutaImagenAnterior = Path.Combine(_webHostEnvironment.WebRootPath, categoria.RutaImagen.TrimStart('\\'));
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
                categoria.RutaImagen = @"\imagenes\categorias\" + nombreArchivo + extension;
                return categoria.RutaImagen;
            }
            else
            {
                categoria.RutaImagen = CategoriaDb.RutaImagen;
                return categoria.RutaImagen;
            }
        }

        public void EliminarImagen(Categoria categoria)
        {
            string rutaPrincipal = _webHostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaPrincipal, categoria.RutaImagen.TrimStart('\\'));
            if (File.Exists(rutaImagen))
            {
                File.Delete(rutaImagen);
            }
        }

    }
}
