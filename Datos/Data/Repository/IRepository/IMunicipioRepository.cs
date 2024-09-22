using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository.IRepository
{
    public interface IMunicipioRepository<Municipio>
    {
        public IEnumerable<SelectListItem> GetLista();
    }
}
