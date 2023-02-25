namespace RecuteDog.Models
{
    public class Mascota
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public double Ancho { get; set; }
        public double Alto { get; set; }
        public double Peso { get; set; }
        public string Descripcion { get; set; }
        public bool Peligrosidad { get; set; }

    }
}
