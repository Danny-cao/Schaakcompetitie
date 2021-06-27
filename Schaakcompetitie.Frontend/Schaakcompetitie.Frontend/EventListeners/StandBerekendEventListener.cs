using System;
using Roger.MicroServices.Events;
using Schaakcompetitie.Frontend.DAL.DataMappers;
using Schaakcompetitie.Frontend.DAL.Models;
using Schaakcompetitie.Frontend.EventListeners.Events;

namespace Schaakcompetitie.Frontend.EventListeners
{
    [EventListener]
    public class StandBerekendEventListener
    {
        private readonly IDataMapper<Stand, long> _standDataMapper;

        public StandBerekendEventListener(IDataMapper<Stand, long> standDataMapper)
        {
            _standDataMapper = standDataMapper;
        }

        [Handler("Schaakcompetitie.StandBerekend")]
        public void Handler(StandBerekendEvent @event)
        {
            try
            {
                foreach (var stand in @event.Standen)
                {
                    if (_standDataMapper.Exists(stand))
                    {
                        Stand dbStand = _standDataMapper.FindByName(stand);
                        _standDataMapper.Delete(dbStand);
                    }
                    _standDataMapper.Insert(stand);
                }
            }
            catch (InvalidOperationException){
                
            }
        }
    }
}