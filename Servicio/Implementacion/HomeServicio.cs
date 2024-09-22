using AccesoDatos.Data.Repository.IRepository;

using Entidad;
using Entidad.ViewModel;

namespace Servicio.Implementacion
{
    public class HomeServicio : IHomeServicio
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public HomeServicio(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        public HomeVm VistaInicio()
        {
            var productos = _contenedorTrabajo.Productos.GetAll();
            var sliders = _contenedorTrabajo.Slider.GetAll();
            var categorias = _contenedorTrabajo.Categoria.GetAll();
            var vistaInicio = new HomeVm()
            {
                Productos = productos.Where(p => p.Activo && p.Stock > 0),

                Sliders = sliders.Where(s => s.Activo),
                Categorias = categorias.Where(c => c.Activo)
            };
            return vistaInicio;
        }

        public ProductFilterVM VistaProductos(List<int> CategoriasFiltradas = null)
        {
            try
            {
                var productosDb = _contenedorTrabajo.Productos.GetAll(includeProperties: "Categoria,Marca",
                               orderBy: q => q.OrderByDescending(p => p.Stock));

                if (CategoriasFiltradas != null && CategoriasFiltradas.Any())
                {
                    productosDb = productosDb.Where(p => CategoriasFiltradas.Contains(p.Categoria.IdCategoria));
                }

                var productos = productosDb;
                var marcas = _contenedorTrabajo.Marca.GetAll();
                var categorias = _contenedorTrabajo.Categoria.GetAll();

                var VistaProductos = new ProductFilterVM()
                {
                    productos = productos,
                    marcas = marcas,
                    categorias = categorias
                };
                return VistaProductos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Se produjo un error al obtener la vista", ex);
            }

        }
        public ProductFilterVM ProductosFiltrados(List<int> CategoriasFiltradas, List<int> MarcasFiltradas)
        {
            try
            {
                var productosDb = _contenedorTrabajo.Productos.GetAll(includeProperties: "Categoria,Marca",
              orderBy: q => q.OrderByDescending(p => p.Stock));

                if (CategoriasFiltradas != null && CategoriasFiltradas.Any())
                {
                    productosDb = productosDb.Where(p => CategoriasFiltradas.Contains(p.Categoria.IdCategoria));
                }

                if (MarcasFiltradas != null && MarcasFiltradas.Any())
                {
                    productosDb = productosDb.Where(p => MarcasFiltradas.Contains(p.Marca.IdMarca));
                }

                var productos = productosDb;
                var categorias = _contenedorTrabajo.Categoria.GetAll();
                var marcas = _contenedorTrabajo.Marca.GetAll();

                var productosFiltrados = new ProductFilterVM
                {
                    productos = productos,
                    categorias = categorias,
                    marcas = marcas
                };

                return productosFiltrados;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Se produjo un error al obtener los productos", ex);
            }

        }

        public async Task<Producto> ObtenerProductoPorID(int id)
        {
            try
            {
                var producto = await _contenedorTrabajo.Productos.Get(id);
                return producto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Venta> ObtenerVentaPorID(int id)
        {
            var venta = await _contenedorTrabajo.Venta.Get(id);
            if (venta == null)
            {
                throw new InvalidOperationException("Error: la venta no existe");
            }
            return venta;
        }
        public List<Detalle_Venta> ObtenerDetallesVentaPorVentaID(int idVenta)
        {
            var detallesVenta = _contenedorTrabajo.Detalle_Venta
                .GetAll(dv => dv.IdVenta == idVenta, includeProperties: "Producto").ToList();

            return detallesVenta;
        }
        public async Task<Producto> EnviarProductoAlCarrito(int id)
        {
            try
            {
                var producto = await ObtenerProductoPorID(id);

                if (producto == null)
                {
                    return null;
                }
                return producto;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Se produjo un error al obtener el producto", ex);
            }

        }

        public CarritoVm InformacionVistaCarrito()
        {
            var departamento = _contenedorTrabajo.Departamento.GetLista();
            var municipios = _contenedorTrabajo.Municipio.GetLista();
            var carrito = new CarritoVm()
            {
                Departamenots = departamento.OrderBy(d => d.Text),
                Municipios = municipios.OrderBy(m => m.Text),
            };
            return carrito;
        }

        public async Task<Venta> RealizarCompraCarrito(Venta venta)
        {
            try
            {
                var nuevaVenta = await RegistrarVenta(venta);
                await RegistrarDetallesVenta(venta, nuevaVenta);

                return nuevaVenta;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al registrar la venta:", ex);
            }
        }

        public async Task<Venta> DatosVenta(int id)
        {
            try
            {
                var venta = await ObtenerVentaPorID(id);
                var detallesVenta = ObtenerDetallesVentaPorVentaID(id);

                // Verifica que la propiedad DetalleVenta esté inicializada
                if (venta.DetalleVenta == null)
                {
                    venta.DetalleVenta = new List<Detalle_Venta>();
                }

                venta.DetalleVenta = detallesVenta;

                return venta;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al registrar la venta:", ex);
            }
        }

        public async Task<Venta> RegistrarVenta(Venta venta)
        {
            var nuevaVenta = new Venta()
            {
                IdUsuario = venta.IdUsuario,
                Contacto = venta.Contacto,
                Telefono = venta.Telefono,
                Direccion = venta.Direccion,
                FechaVenta = DateTime.UtcNow,
                TotalProducto = venta.TotalProducto,
                MontoTotal = venta.MontoTotal
            };
            _contenedorTrabajo.Venta.Add(nuevaVenta);
            await _contenedorTrabajo.SaveAsync();

            return nuevaVenta;
        }

        public async Task RegistrarDetallesVenta(Venta venta, Venta nuevaVenta)
        {
            foreach (var detalle in venta.DetalleVenta)
            {
                var nuevoDetalle = new Detalle_Venta
                {
                    IdVenta = nuevaVenta.IdVenta,
                    IdProducto = detalle.IdProducto,
                    Cantidad = detalle.Cantidad,
                    Total = detalle.Total
                };

                _contenedorTrabajo.Detalle_Venta.Add(nuevoDetalle);
                var actualizarStock = await _contenedorTrabajo.Productos.Get(detalle.IdProducto);
                actualizarStock.Stock -= detalle.Cantidad;
                _contenedorTrabajo.Productos.Update(actualizarStock);
            }

            await _contenedorTrabajo.SaveAsync();
        }
    }
}
