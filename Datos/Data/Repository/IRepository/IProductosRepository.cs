using Entidad;

namespace AccesoDatos.Data.Repository.IRepository
{
    public interface IProductosRepository : IRepository<Producto>
    {
        void Update(Producto producto);
    }
}
