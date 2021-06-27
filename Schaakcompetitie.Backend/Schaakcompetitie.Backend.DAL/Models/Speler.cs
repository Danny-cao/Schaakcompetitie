using System.ComponentModel.DataAnnotations;

namespace Schaakcompetitie.Backend.DAL.Models
{
    public class Speler
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string Naam { get; set; }
    }
}