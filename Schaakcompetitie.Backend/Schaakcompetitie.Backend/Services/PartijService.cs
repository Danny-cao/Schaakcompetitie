using System.Threading.Tasks;
using Roger.MicroServices.Events;
using Schaakcompetitie.Backend.DAL.Models;
using Schaakcompetitie.Backend.Events;

namespace Schaakcompetitie.Backend.Services
{
    public class PartijService
    {
        private readonly IEventSender _eventSender;
        private Partij _partij;
        
        public PartijService(IEventSender eventSender, Partij partij)
        {
            _eventSender = eventSender;
            _partij = partij;
        }

        public async Task sendEvent()
        {
            await _eventSender.SendAsync(new PartijToegevoegdEvent() 
                {
                    Witspeler = _partij.Witspeler.Naam, 
                    Zwartspeler = _partij.Zwartspeler.Naam, 
                    Uitslag = _partij.Uitslag
                });
        }
    }
}