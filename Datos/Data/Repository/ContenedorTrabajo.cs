using AccesoDatos.Data.Repository.IRepository;
using Entidad;

namespace AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;
        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Usuario = new UsuarioRepository(_db);
            Categoria = new CategoriaRepository(_db);
            Marca = new MarcaRepository(_db);
            Productos = new ProductosRepository(_db);
            Rol = new RolRepository(_db);
            Venta = new VentaRepository(_db);
            Detalle_Venta = new Detalle_VentaRepository(_db);
            Slider = new SliderRepository(_db);
            Municipio = new MunicipioRepository(_db);
            Departamento = new DepartamentoRepository(_db);
        }

        public IUsuariosRepository Usuario { get; private set; }
        public ICategoriasRepository Categoria { get; private set; }
        public IMarcaRepository Marca { get; private set; }
        public IProductosRepository Productos { get; private set; }
        public IRolRepository Rol { get; private set; }
        public IVentaRepository Venta { get; private set; }
        public IDetalle_VentaRepository Detalle_Venta { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IMunicipioRepository<Municipio> Municipio { get; private set; }
        public IDepartamentoRepository<Departamento> Departamento { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
