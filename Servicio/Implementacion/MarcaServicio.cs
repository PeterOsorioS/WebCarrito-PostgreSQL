using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Servicio.Implementacion
{
    public class MarcaServicio : IMarcaServicio
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public MarcaServicio(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        public IActionResult CrearMarca(Marca marca)
        {
            try
            {
                marca.FechaRegistro = DateTime.UtcNow;
                _contenedorTrabajo.Marca.Add(marca);
                _contenedorTrabajo.Save();
                return new OkObjectResult(new { success = true, message = "Marca creada correctamente." });
            }
            catch (DbUpdateException ex)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al guardar los cambios." + ex.Message });
            }
        }
        public async Task<Marca> GetCategoriaByIdAsync(int id)
        {
            try
            {
                var marca = await _contenedorTrabajo.Marca.Get(id);
                return marca;
            }
            catch (Exception e)
            {

                throw new Exception($"Error al obtener la marca", e);
            }
        }
        public IActionResult ActualizarMarca(Marca marca)
        {
            try
            {
                _contenedorTrabajo.Marca.Update(marca);
                _contenedorTrabajo.Save();
                return new OkObjectResult(new { success = true, message = "Marca actualizada correctamente." });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = false, message = "Error al guardar los cambios" });
            }
        }

        public async Task<IActionResult> EliminarMarca(int id)
        {
            try
            {
                var marca = await _contenedorTrabajo.Marca.Get(id);
                if (marca == null)
                {
                    return new BadRequestObjectResult(new { success = false, message = "No se encontro marca" });
                }
                await _contenedorTrabajo.Marca.Remove(marca);
                await _contenedorTrabajo.SaveAsync();
                return new OkObjectResult(new { success = true, message = "Marca Eliminada" });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { success = true, message = "Error al eliminar la marca." });
            }

        }
    }
}
