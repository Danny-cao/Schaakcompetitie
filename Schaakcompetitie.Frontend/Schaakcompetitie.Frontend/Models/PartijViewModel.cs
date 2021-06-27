using System;
using System.ComponentModel.DataAnnotations;

namespace Schaakcompetitie.Frontend.Models
{
    public class PartijViewModel
    {
        [Required]
        public string Witspeler { get; set; }
        [Required]
        public string Zwartspeler { get; set; }
        [Required]
        public int Uitslag { get; set; }
    }
}