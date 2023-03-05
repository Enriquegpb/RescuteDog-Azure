using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecuteDog.Models
{
    [Table("ADOPCION")]
    public class Adopcion
    {
        [Key]
        [Column("IDMASCOTA")]
        public int IdMascota { get; set; }
        [Column("IDUSER")]
        public int IdUser { get; set; } 
        [Column("Fecha_Adopcion")]
        public DateTime FechaAdopcion { get; set; } 
    }
}
