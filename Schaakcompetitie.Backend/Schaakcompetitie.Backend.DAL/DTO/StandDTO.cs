namespace Schaakcompetitie.Backend.DAL.DTO
{
    public class StandDTO
    {
        public string Speler { get; set; }
        public int Partij { get; set; }
        public int Gewonnen { get; set; }
        public int Gelijk { get; set; }
        public int Verloren { get; set; }
        public float Score { get; set; }
    }
}