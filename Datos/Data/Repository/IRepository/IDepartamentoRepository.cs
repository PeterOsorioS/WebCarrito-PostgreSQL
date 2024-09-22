using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository.IRepository
{
    public interface IDepartamentoRepository<Departamento>
    {
        public IEnumerable<SelectListItem> GetLista();
    }
}
