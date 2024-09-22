using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriasRepository
    {
        ApplicationDbContext _db;
        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = _db.Categorias.FirstOrDefault(s => s.IdCategoria == categoria.IdCategoria);
            objDesdeDb.Descripcion = categoria.Descripcion;
            objDesdeDb.Activo = categoria.Activo;
            objDesdeDb.RutaImagen = categoria.RutaImagen;
        }

        public IEnumerable<SelectListItem> GetLista()
        {
            return _db.Categorias
                .Where(C => C.Activo)
                .Select(C => new SelectListItem()
                {
                    Text = C.Descripcion,
                    Value = C.IdCategoria.ToString()
                });
        }
    }
}
