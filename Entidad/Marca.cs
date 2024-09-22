using System.ComponentModel.DataAnnotations;

namespace Entidad
{
    public class Marca
    {
        [Key]
        public int IdMarca { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatoria")]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroF
        {
            get { return FechaRegistro.ToString("dd/MM/yyyy"); }
        }
    }
}
