
namespace Schaakcompetitie.Backend.DAL.Models
{
    public class Partij
    {
        public long Id { get; set; }
        public Speler Witspeler { get; set; }
        public Speler Zwartspeler { get; set; }
        public int Uitslag { get; set; }
    }
}