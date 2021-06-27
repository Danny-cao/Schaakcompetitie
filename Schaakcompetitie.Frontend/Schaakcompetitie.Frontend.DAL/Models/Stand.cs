namespace Schaakcompetitie.Frontend.DAL.Models
{
    public class Stand
    {
        public long Id { get; set; }
        public string Speler { get; set; }
        public int Partij { get; set; }
        public int Gewonnen { get; set; }
        public int Gelijk { get; set; }
        public int Verloren { get; set; }
        public float Score { get; set; }
    }
}