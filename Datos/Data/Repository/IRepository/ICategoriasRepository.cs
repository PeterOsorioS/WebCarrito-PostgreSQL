using Entidad;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccesoDatos.Data.Repository.IRepository
{
    public interface ICategoriasRepository : IRepository<Categoria>
    {
        void Update(Categoria categoria);
        IEnumerable<SelectListItem> GetLista();
    }
}
