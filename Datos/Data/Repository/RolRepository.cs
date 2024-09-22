using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository
{
    public class RolRepository : Repository<Rol>, IRolRepository
    {
        private readonly ApplicationDbContext _db;
        public RolRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetLista()
        {
            return _db.Roles
                .Select(m => new SelectListItem()
                {
                    Text = m.Descripcion,
                    Value = m.IdRol.ToString()
                });
        }
    }
}
