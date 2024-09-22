using System.ComponentModel.DataAnnotations;

namespace Entidad
{
    public class Slider
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatorio")]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        [Display(Name = "Imagen")]
        public string RutaImagen { get; set; }

    }
}
