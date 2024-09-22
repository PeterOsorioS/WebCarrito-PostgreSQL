using Entidad;

namespace AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IUsuariosRepository Usuario { get; }
        ICategoriasRepository Categoria { get; }
        IMarcaRepository Marca { get; }
        IProductosRepository Productos { get; }
        IRolRepository Rol { get; }
        IVentaRepository Venta { get; }
        IDetalle_VentaRepository Detalle_Venta { get; }
        ISliderRepository Slider { get; }
        IMunicipioRepository<Municipio> Municipio { get; }
        IDepartamentoRepository<Departamento> Departamento { get; }
        void Save();
        Task SaveAsync();
    }
}
