using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecuteDog.Models
{
    [Table("Refugio")]
    public class Refugio
    {
        [Key]
        [Column("IDREFUGIO")]
        public int IdRefugio { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("LOCALIDAD")]
        public string Localidad { get; set; }
        [Column("UBICACION")]
        public string Ubicacion { get; set; }
        [Column("IMAGEN")]
        public string Valoracion { get; set; }
    }
}
