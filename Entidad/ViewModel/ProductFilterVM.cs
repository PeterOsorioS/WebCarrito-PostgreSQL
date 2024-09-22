namespace Entidad.ViewModel
{
    public class ProductFilterVM
    {
        public IEnumerable<Producto> productos { get; set; }
        public IEnumerable<Categoria> categorias { get; set; }
        public IEnumerable<Marca> marcas { get; set; }
    }
}
