using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecuteDog.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NAME")]
        public string Name { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("BIRDTHDAY")]
        public string  Birdthday { get; set; }
        [Column("PHONE")]
        public string Phone { get; set; }
        [Column("USERNAME")]
        public string Username { get; set; }
        [Column("PASSWORD")]
        public string Password { get; set; }
    }
}
