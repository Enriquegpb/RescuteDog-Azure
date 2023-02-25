﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RecuteDog.Models
{
    [Table("Mascota")]
    public class Mascota
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("RAZA")]
        public string Raza { get; set; }
        [Column("EDAD")]
        public int Edad { get; set; }
        [Column("ANCHO")]
        public double Ancho { get; set; }
        [Column("ALTO")]
        public double Alto { get; set; }
        [Column("PESO")]
        public double Peso { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("PELIGROSIDAD")]
        public bool Peligrosidad { get; set; }

    }
}
