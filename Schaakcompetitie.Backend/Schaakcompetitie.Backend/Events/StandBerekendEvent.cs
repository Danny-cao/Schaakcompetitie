using System.Collections.Generic;
using Roger.MicroServices.Events;
using Schaakcompetitie.Backend.DAL.DTO;

namespace Schaakcompetitie.Backend.Events
{
    public class StandBerekendEvent : DomainEvent
    {
        public StandBerekendEvent() : base("Schaakcompetitie.StandBerekend") { }
        
        public IEnumerable<StandDTO> Standen { get; set; }
    }
}