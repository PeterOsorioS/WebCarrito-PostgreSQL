using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidad
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatoria")]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        [NotMapped]
        public string FechaRegistroF
        {
            get { return FechaRegistro.ToString("dd/MM/yyyy"); }
        }
        public string RutaImagen { get; set; }
    }
}
