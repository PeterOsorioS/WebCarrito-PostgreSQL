using AccesoDatos.Data.Repository.IRepository;
using ClosedXML.Excel;
using Entidad;
using Entidad.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Servicio.Implementacion
{
    public class DashboardServicio : IDashboardServicio
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public DashboardServicio(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        public DashboardVm ObtenerDashboard()
        {
            DashboardVm dashboard = new DashboardVm()
            {
                TotalProductos = _contenedorTrabajo.Productos.GetAllCount(),
                TotalClientes = _contenedorTrabajo.Usuario.GetAllCount(u => u.Rol.Descripcion == "Cliente"),
                TotalVentas = _contenedorTrabajo.Venta.GetAllCount(),
                HistorialVentas = ObtenerTodasLasVentas()
            };
            return dashboard;
        }
        public IEnumerable<Venta> ObtenerTodasLasVentas()
        {
            return _contenedorTrabajo.Venta.GetAll(includeProperties: "DetalleVenta.Producto");
        }

        public IEnumerable<Venta> BuscarVentasPorFechas(string fechainicio, string fechafin)
        {
            DateTime fechaInicioParsed = DateTime.Parse(fechainicio);
            DateTime fechaFinParsed = DateTime.Parse(fechafin);

            var venta = ObtenerTodasLasVentas();
            venta = venta.Where(v => v.FechaVenta >= fechaInicioParsed && v.FechaVenta <= fechaFinParsed);
            return venta;
        }
        public FileContentResult ExportarVentasAExcel(string fechainicio, string fechafin)
        {
            // Obtención de ventas
            var VentasFiltradasPorFecha = BuscarVentasPorFechas(fechainicio, fechafin);

            // Creación del DataTable
            var dataTable = new DataTable("Ventas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
            new DataColumn("Fecha Venta"),
            new DataColumn("Cliente"),
            new DataColumn("Producto"),
            new DataColumn("Precio"),
            new DataColumn("Cantidad"),
            new DataColumn("Total")
            });

            // Llenado del DataTable con los datos de ventas
            foreach (var venta in VentasFiltradasPorFecha)
            {
                foreach (var detalle in venta.DetalleVenta)
                {
                    dataTable.Rows.Add(
                        venta.FechaVenta.ToString("yyyy-MM-dd"),
                        venta.Contacto,
                        detalle.Producto.Nombre,
                        detalle.Producto.Precio.ToString("C"),
                        detalle.Cantidad,
                        detalle.Cantidad * detalle.Producto.Precio
                    );
                }
            }

            // Creación del archivo Excel
            using (var workbook = new XLWorkbook())
            {
                using (var stream = new MemoryStream())
                {
                    workbook.Worksheets.Add(dataTable);
                    workbook.SaveAs(stream);

                    // Retornar el archivo como FileContentResult
                    var fileContent = stream.ToArray();
                    return new FileContentResult(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "Ventas.xlsx"
                    };
                }
            }
        }

    }
}
