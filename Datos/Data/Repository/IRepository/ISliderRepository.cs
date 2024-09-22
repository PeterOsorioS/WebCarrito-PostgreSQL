using Entidad;

namespace AccesoDatos.Data.Repository.IRepository
{
    public interface ISliderRepository : IRepository<Slider>
    {
        void update(Slider slider);
    }
}
