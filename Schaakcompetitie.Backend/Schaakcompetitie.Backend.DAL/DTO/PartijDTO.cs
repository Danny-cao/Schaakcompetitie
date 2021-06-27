namespace Schaakcompetitie.Backend.DAL.DTO
{
    public class PartijDTO
    {
        public long Id { get; set; }
        public string Witspeler { get; set; }
        public string Zwartspeler { get; set; }
        public int Uitslag { get; set; }
    }
}