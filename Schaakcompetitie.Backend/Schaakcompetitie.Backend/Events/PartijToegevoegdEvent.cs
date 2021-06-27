using System;
using Roger.MicroServices.Events;

namespace Schaakcompetitie.Backend.Events
{
    public class PartijToegevoegdEvent : DomainEvent
    {
        public PartijToegevoegdEvent() : base("Schaakcompetitie.Backend.PartijToegevoegd") { }
        
        public long Id { get; set; }
        public string Witspeler { get; set; }
        public string Zwartspeler { get; set; }
        public int Uitslag { get; set; }
    }
}