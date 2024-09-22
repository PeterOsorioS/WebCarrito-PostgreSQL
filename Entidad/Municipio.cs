using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidad
{
    public class Municipio
    {
        [Key]
        public int IdMunicipio { get; set; }
        public string Descripcion { get; set; }
        public string IdDepartamento { get; set; }
    }
}
