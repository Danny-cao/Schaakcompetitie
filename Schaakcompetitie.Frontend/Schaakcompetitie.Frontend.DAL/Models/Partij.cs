namespace Schaakcompetitie.Frontend.DAL.Models
{
    public class Partij
    {
        public long Id { get; set; }
        public string Witspeler { get; set; }
        public string Zwartspeler { get; set; }
        public int Uitslag { get; set; }
    }
}