using System.ComponentModel.DataAnnotations;

namespace Entidad
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
    }
}
