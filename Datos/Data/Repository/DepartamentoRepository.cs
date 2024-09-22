using AccesoDatos.Data.Repository.IRepository;
using Entidad;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository
{
    public class DepartamentoRepository : IDepartamentoRepository<Departamento>
    {
        private readonly ApplicationDbContext _db;
        public DepartamentoRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetLista()
        {
            return _db.Departamentos.Select(d => new SelectListItem()
            {
                Text = d.Descripcion,
                Value = d.IdDepartamento.ToString()
            });
        }
    }
}
