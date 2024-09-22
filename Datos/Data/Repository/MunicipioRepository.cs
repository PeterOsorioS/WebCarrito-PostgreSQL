using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository
{
    internal class MunicipioRepository : IMunicipioRepository<Municipio>
    {
        private readonly ApplicationDbContext _db;
        public MunicipioRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetLista()
        {
            return _db.Municipio
                .Select(m => new SelectListItem()
                {
                    Text = m.Descripcion,
                    Value = m.IdMunicipio.ToString()
                });
        }
    }
}
