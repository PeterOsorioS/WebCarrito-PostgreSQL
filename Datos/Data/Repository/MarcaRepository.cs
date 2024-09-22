using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository
{
    public class MarcaRepository : Repository<Marca>, IMarcaRepository
    {
        private readonly ApplicationDbContext _db;
        public MarcaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Marca marca)
        {
            var objDesdeDb = _db.Marcas.FirstOrDefault(m => m.IdMarca == marca.IdMarca);
            objDesdeDb.Descripcion = marca.Descripcion;
            objDesdeDb.Activo = marca.Activo;
        }
        public IEnumerable<SelectListItem> GetLista()
        {
            return _db.Marcas
                .Where(m => m.Activo)
                .Select(m => new SelectListItem()
                {
                    Text = m.Descripcion,
                    Value = m.IdMarca.ToString()
                });
        }

    }
}
