using Entidad;
using Microsoft.AspNetCore.Mvc;

namespace Servicio
{
    public interface IMarcaServicio
    {
        public IActionResult CrearMarca(Marca marca);
        public Task<Marca> GetCategoriaByIdAsync(int id);
        public IActionResult ActualizarMarca(Marca marca);
        public Task<IActionResult> EliminarMarca(int id);
    }
}
