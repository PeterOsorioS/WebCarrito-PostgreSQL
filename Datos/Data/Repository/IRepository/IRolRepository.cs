using Entidad;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AccesoDatos.Data.Repository.IRepository
{
    public interface IRolRepository : IRepository<Rol>
    {
        IEnumerable<SelectListItem> GetLista();
    }
}
