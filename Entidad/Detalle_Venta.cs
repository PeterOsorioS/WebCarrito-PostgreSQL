using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidad
{
    public class Detalle_Venta
    {
        [Key]
        public int IdDetalleVenta { get; set; }

        public int IdVenta { get; set; }

        [ForeignKey("IdVenta")]
        public Venta Venta { get; set; }

        public int IdProducto { get; set; } // Corregido el nombre del campo

        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
    }
}
