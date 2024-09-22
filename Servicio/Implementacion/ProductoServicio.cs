using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Entidad.ViewModel;
using Microsoft.AspNetCore.Hosting; // Asegúrate de incluir esta directiva
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Servicio.Implementacion
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoServicio(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment environment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _webHostEnvironment = environment;
        }

        public ProductoVm ObtenerProductoVm()
        {
            var listaMarcas = _contenedorTrabajo.Marca.GetLista();
            var listaCategorias = _contenedorTrabajo.Categoria.GetLista();
            var productosVm = new ProductoVm()
            {
                producto = new Producto(),
                ListaMarcas = listaMarcas,
                ListaCategorias = listaCategorias
            };
            return productosVm;
        }

        public async Task<ProductoVm> ObtenerProductoVmPorId(int id)
        {
            var listaMarcas = _contenedorTrabajo.Marca.GetLista();
            var listaCategorias = _contenedorTrabajo.Categoria.GetLista();
            var productosVm = new ProductoVm()
            {
                producto = await _contenedorTrabajo.Productos.Get(id),
                ListaMarcas = listaMarcas,
                ListaCategorias = listaCategorias
            };
            return productosVm;
        }

        public IActionResult CrearProducto(Producto producto, IFormFile archivo)
        {
            try
            {
                producto.FechaRegistro = DateTime.UtcNow;
                producto.NombreImagen = producto.Nombre;
                producto.RutaImagen = SubirImagen(producto, archivo);
                _contenedorTrabajo.Productos.Add(producto);
                _contenedorTrabajo.Save();
                return new OkObjectResult(new { success = true, message = "Producto creado correctamente." });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al crear el producto." });
            }

        }

        public async Task<IActionResult> ActualizarProducto(Producto producto, IFormFile archivo)
        {
            try
            {
                producto.RutaImagen = await EditarImagen(producto, archivo);
                producto.NombreImagen = producto.Nombre;
                _contenedorTrabajo.Productos.Update(producto);
                _contenedorTrabajo.Save();
                return new OkObjectResult(new { success = true, message = "Producto actualizado correctamente." });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al actualizar el producto." });
            }
        }


        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                var producto = await _contenedorTrabajo.Productos.Get(id);
                if (producto == null)
                {
                    return new BadRequestObjectResult(new { success = false, message = "Producto no encontrado" });
                }
                EliminarImagen(producto);
                await _contenedorTrabajo.Productos.Remove(producto);
                await _contenedorTrabajo.SaveAsync();
                return new OkObjectResult(new { success = true, message = "Producto Eliminado" });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = true, message = "Producto no Eliminado" });
            }
        }

        public string SubirImagen(Producto producto, IFormFile archivo)
        {
            if (producto.IdProducto == 0 && archivo != null)
            {
                string rutaPrincipal = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes", "productos");
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

                return producto.RutaImagen = @"\imagenes\productos\" + nombreArchivo + extension;
            }
            throw new InvalidOperationException("La Imagen es obligatoria");
        }

        public async Task<string> EditarImagen(Producto producto, IFormFile archivo)
        {
            var productoDb = await _contenedorTrabajo.Productos.Get(producto.IdProducto);
            if (archivo != null)
            {
                string rutaPrincipal = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes", "productos");
                string nombreArchivo = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(archivo.FileName);

                // Verificar si existe una imagen anterior y eliminarla
                if (!string.IsNullOrEmpty(producto.RutaImagen))
                {
                    string rutaImagenAnterior = Path.Combine(_webHostEnvironment.WebRootPath, producto.RutaImagen.TrimStart('\\'));
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
                producto.RutaImagen = @"\imagenes\productos\" + nombreArchivo + extension;
                return producto.RutaImagen;
            }
            else
            {
                producto.RutaImagen = productoDb.RutaImagen;
                return producto.RutaImagen;
            }
        }

        public void EliminarImagen(Producto producto)
        {
            string rutaPrincipal = _webHostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaPrincipal, producto.RutaImagen.TrimStart('\\'));
            if (File.Exists(rutaImagen))
            {
                File.Delete(rutaImagen);
            }
        }
    }
}
