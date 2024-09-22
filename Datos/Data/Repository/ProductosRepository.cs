using AccesoDatos.Data.Repository.IRepository;
using Entidad;

namespace AccesoDatos.Data.Repository
{
    public class ProductosRepository : Repository<Producto>, IProductosRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Producto producto)
        {
            var objDesdeDb = _db.Productos.FirstOrDefault(p => p.IdProducto == producto.IdProducto);
            objDesdeDb.Nombre = producto.Nombre;
            objDesdeDb.Descripcion = producto.Descripcion;
            objDesdeDb.IdCategoria = producto.IdCategoria;
            objDesdeDb.IdMarca = producto.IdMarca;
            objDesdeDb.Stock = producto.Stock;
            objDesdeDb.Activo = producto.Activo;
            objDesdeDb.Precio = producto.Precio;
            objDesdeDb.RutaImagen = producto.RutaImagen;
            objDesdeDb.NombreImagen = producto.NombreImagen;
        }
    }
}
