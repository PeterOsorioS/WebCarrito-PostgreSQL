using AccesoDatos.Data.Repository.IRepository;
using Entidad;

namespace AccesoDatos.Data.Repository
{
    public class VentaRepository : Repository<Venta>, IVentaRepository
    {
        private readonly ApplicationDbContext _db;
        public VentaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
