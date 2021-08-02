using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Schaakcompetitie.Backend.DAL.DataMapper;
using Schaakcompetitie.Backend.DAL.DTO;
using Schaakcompetitie.Backend.DAL.Models;
using Schaakcompetitie.Backend.Services;

namespace Schaakcompetitie.Backend.Controllers
{
    [ApiController]
    [Route("standen")]
    public class StandController : ControllerBase
    {
        private readonly IDataMapper<Partij, long> _dataMapperPartij;
        private readonly IDataMapper<Speler, long> _dataMapperSpeler;

        public StandController(IDataMapper<Partij, long> dataMapperPartij, IDataMapper<Speler, long> dataMapperSpeler)
        {
            _dataMapperPartij = dataMapperPartij;
            _dataMapperSpeler = dataMapperSpeler;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Stand>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                List<Partij> partijen = _dataMapperPartij.FindAll().ToList();
                List<Speler> spelers = _dataMapperSpeler.FindAll().ToList();

                CalculateStanden calculate = new CalculateStanden(partijen, spelers);

                List<StandDTO> standen = calculate.Generate();

                return Ok(standen);
            }
            catch (Exception e)
            {
                return StatusCode(404, e);
            }
        }
    }
}