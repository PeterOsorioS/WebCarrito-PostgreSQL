using System.ComponentModel.DataAnnotations;

namespace Entidad
{
    public class Departamento
    {
        [Key]
        public string IdDepartamento { get; set; }
        public string Descripcion { get; set; }
    }
}
