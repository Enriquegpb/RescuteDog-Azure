using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecuteDog.Models
{
    [Table("VOLUNTARIOS")]
    public class Voluntario
    {
        [Key]
        [Column("IDVOLUNTARIO")]
        public int IdVoluntario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("MENSAJE")]
        public string Mensaje { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("CORREO")]
        public string Correo { get; set; }
        [Column("MUNICIPIO")]
        public string Municipio { get; set; }
        [Column("FECHA_NACIMIENTO")]
        public string Fecha_Nacimiento { get; set; }
        [Column("IDREFUGIO")]
        public int IdRefugio { get; set; }
    }
}
