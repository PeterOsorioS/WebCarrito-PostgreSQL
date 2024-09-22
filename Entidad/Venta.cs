using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidad
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        public int TotalProducto { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MontoTotal { get; set; }

        [Required, MaxLength(100)]
        public string Contacto { get; set; }

        [Required, MaxLength(20)]
        public string Telefono { get; set; }

        [Required, MaxLength(200)]
        public string Direccion { get; set; }

        public DateTime FechaVenta { get; set; }

        public List<Detalle_Venta> DetalleVenta { get; set; }

        public Venta()
        {
            DetalleVenta = new List<Detalle_Venta>();
        }
    }
}
