using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecuteDog.Models
{
    [Table("COMENTARIOS")]
    public class Comentario
    {
        [Key]
        [Column("IDCOMENTARIO")]
        public int IdComentario { get; set; }
        [Column("IDPOST")]
        public int IdPost { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("COMENTARIO")]
        public string ComentarioDesc { get; set; }

    }
}
