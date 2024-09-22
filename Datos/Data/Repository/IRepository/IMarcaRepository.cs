using Entidad;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository.IRepository
{
    public interface IMarcaRepository : IRepository<Marca>
    {
        void Update(Marca marca);
        IEnumerable<SelectListItem> GetLista();
    }
}
