using System.Collections.Generic;
using System.Threading.Tasks;
using Roger.MicroServices.Events;
using Schaakcompetitie.Backend.DAL.DTO;
using Schaakcompetitie.Backend.DAL.Models;
using Schaakcompetitie.Backend.Events;

namespace Schaakcompetitie.Backend.Services
{
    public class StandService 
    {
        
        private readonly IEventSender _eventSender;
        private Partij _partij;
        private List<Speler> _spelers;
        private List<Partij> _partijen;

        public StandService(IEventSender eventSender, List<Speler> spelers, List<Partij> partijen)
        {
            _eventSender = eventSender;
            _spelers = spelers;
            _partijen = partijen;
        }

        public async Task sendEvent()
        {
            CalculateStanden service = new CalculateStanden(_partijen, _spelers);

            List<StandDTO> standen = service.Generate();
            
            await _eventSender.SendAsync(new StandBerekendEvent() 
            {
                Standen = standen
            });
        }
    }
}