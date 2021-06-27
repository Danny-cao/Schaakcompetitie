namespace Schaakcompetitie.Frontend.Models
{
    public class StandViewModel
    {
        public string Speler { get; set; }
        public int Partij { get; set; }
        public int Gewonnen { get; set; }
        public int Gelijk { get; set; }
        public int Verloren { get; set; }
        public float Score { get; set; }
    }
}