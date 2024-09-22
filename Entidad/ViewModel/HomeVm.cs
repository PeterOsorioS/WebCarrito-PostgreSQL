namespace Entidad.ViewModel
{
    public class HomeVm
    {
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
    }
}
