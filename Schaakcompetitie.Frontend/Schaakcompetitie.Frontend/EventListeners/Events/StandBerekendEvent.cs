using System.Collections.Generic;
using Roger.MicroServices.Events;
using Schaakcompetitie.Frontend.DAL.Models;

namespace Schaakcompetitie.Frontend.EventListeners.Events
{
    public class StandBerekendEvent : DomainEvent
    {
        public StandBerekendEvent() : base("Schaakcompetitie.Backend.StandBerekend") { }
        
        public IEnumerable<Stand> Standen { get; set; }
    }
}