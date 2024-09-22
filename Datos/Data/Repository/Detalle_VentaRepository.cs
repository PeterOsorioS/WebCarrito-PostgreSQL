using AccesoDatos.Data.Repository.IRepository;
using Entidad;

namespace AccesoDatos.Data.Repository
{
    public class Detalle_VentaRepository : Repository<Detalle_Venta>, IDetalle_VentaRepository
    {
        private readonly ApplicationDbContext _db;
        public Detalle_VentaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
