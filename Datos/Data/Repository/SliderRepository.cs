using AccesoDatos.Data.Repository.IRepository;
using Entidad;

namespace AccesoDatos.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void update(Slider slider)
        {
            var objDesdeDb = _db.Sliders.FirstOrDefault(s => s.ID == slider.ID);
            objDesdeDb.Descripcion = slider.Descripcion;
            objDesdeDb.Activo = slider.Activo;
            objDesdeDb.RutaImagen = slider.RutaImagen;
        }
    }
}
