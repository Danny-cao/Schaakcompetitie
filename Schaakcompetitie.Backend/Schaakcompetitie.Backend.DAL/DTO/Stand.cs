using Schaakcompetitie.Backend.DAL.Models;

namespace Schaakcompetitie.Backend.DAL.DTO
{
    public class Stand
    {
        public Speler Speler { get; set; }
        public int Partij { get; set; }
        public int Gewonnen { get; set; }
        public int Gelijk { get; set; }
        public int Verloren { get; set; }
        public float Score { get; set; }
    }
}