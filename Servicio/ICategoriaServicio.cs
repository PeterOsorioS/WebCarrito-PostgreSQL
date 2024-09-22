using Entidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Servicio
{
    public interface ICategoriaServicio
    {
        public IActionResult CrearCategoria(Categoria categoria, IFormFile archivo);
        public Task<Categoria> GetCategoriaByIdAsync(int id);
        public Task<IActionResult> EliminarCategoria(int id);
        public Task<IActionResult> ActualizarCategoria(Categoria categoria, IFormFile archivo);
        public string SubirImagen(Categoria categoria, IFormFile archivo);
        public Task<string> EditarImagen(Categoria categoria, IFormFile archivo);
        public void EliminarImagen(Categoria categoria);
    }
}
