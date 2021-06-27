using System.ComponentModel.DataAnnotations;

namespace Schaakcompetitie.Frontend.Models
{
    public class SpelerViewModel
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string Naam { get; set; }
    }
}