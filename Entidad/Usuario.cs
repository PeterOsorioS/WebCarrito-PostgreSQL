using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entidad
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "La contraseña debe tener mas de 8")]
        public string Clave { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
        [Compare("Clave", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarClave { get; set; }

        [DefaultValue(true)]
        public bool Activo { get; set; }

        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        public DateTime FechaRegistro { get; set; }

        [NotMapped]
        public string FechaRegistroF
        {
            get { return FechaRegistro.ToString("dd/MM/yyyy"); }
        }
    }
}
