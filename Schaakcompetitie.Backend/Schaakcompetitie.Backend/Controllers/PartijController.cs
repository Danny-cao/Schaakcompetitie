using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Roger.MicroServices.Events;
using Schaakcompetitie.Backend.Commands;
using Schaakcompetitie.Backend.DAL.DataMapper;
using Schaakcompetitie.Backend.DAL.DTO;
using Schaakcompetitie.Backend.DAL.Models;
using Schaakcompetitie.Backend.Services;

namespace Schaakcompetitie.Backend.Controllers
{
    [ApiController]
    [Route("partij")]
    public class PartijController : ControllerBase
    {
        private readonly IDataMapper<Partij, long> _partijDataMapper;
        private readonly IDataMapper<Speler, long> _spelerDataMapper;
        private readonly IEventSender _eventSender;

        public PartijController(IDataMapper<Partij, long> partijDataMapper, IDataMapper<Speler, long> spelerDataMapper, IEventSender eventSender)
        {
            _eventSender = eventSender;
            _partijDataMapper = partijDataMapper;
            _spelerDataMapper = spelerDataMapper;
        }
        
        [HttpPost]
        public async Task<ActionResult<PartijDTO>> Create(VoegPartijToeCommand command)
        {
            try
            {
                Speler spelerWit = new Speler();
                Speler spelerZwart = new Speler();
                
                if (_spelerDataMapper.Exists(command.partij.Witspeler))
                {
                    spelerWit = _spelerDataMapper.FindByName(command.partij.Witspeler);
                }
                
                if (_spelerDataMapper.Exists(command.partij.Zwartspeler))
                {
                    spelerZwart = _spelerDataMapper.FindByName(command.partij.Zwartspeler);
                }
                else
                {
                    spelerWit.Naam = command.partij.Witspeler;
                    spelerZwart.Naam = command.partij.Zwartspeler;
                }

                Partij newPartij = new Partij()
                {
                    Witspeler = spelerWit,
                    Zwartspeler = spelerZwart,
                    Uitslag = command.partij.Uitslag,
                };
                
                _partijDataMapper.Insert(newPartij);

                PartijService partijService = new PartijService(_eventSender, newPartij);
                await partijService.sendEvent();
                
                
                List<Partij> partijen = _partijDataMapper.FindAll().ToList();
                List<Speler> spelers = _spelerDataMapper.FindAll().ToList();
                
                StandService standService = new StandService(_eventSender, spelers, partijen);
                await standService.sendEvent();
                
                return Ok(command.partij);
            }
            catch (Exception e)
            {
                return StatusCode(404, e);
            }
        }
    }
}