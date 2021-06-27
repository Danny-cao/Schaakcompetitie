using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Roger.MicroServices.Events;
using Schaakcompetitie.Backend.Commands;
using Schaakcompetitie.Backend.Controllers;
using Schaakcompetitie.Backend.DAL.DataMapper;
using Schaakcompetitie.Backend.DAL.DTO;
using Schaakcompetitie.Backend.DAL.Models;

namespace Schaakcompetitie.Backend.Test
{
    [TestClass]
    public class PartijControllerTest
    {
        [TestMethod]
        public void CreateCallsPartijInsertAndSpelerExistFromDataMapper()
        {
            var partijDataMapperMock = new Mock<IDataMapper<Partij, long>>();
            var spelerDataMapperMock = new Mock<IDataMapper<Speler, long>>();
            var eventSenderMock = new Mock<IEventSender>();
            
            Speler speler1 = new Speler()
            {
                Id = 0,
                Naam = "hans"
            };

            Speler speler2 = new Speler()
            {
                Id = 0,
                Naam = "Sjaak"
            };
            
            Partij partij = new Partij()
            {
                Id = 1,
                Witspeler = speler1,
                Zwartspeler = speler2,
                Uitslag = 1,
            };
            
            PartijDTO partijDTO = new PartijDTO()
            {
                Id = 1,
                Witspeler = "Hans",
                Zwartspeler = "Sjaak",
                Uitslag = 1,
            };
            
            partijDataMapperMock.Setup(d => d.Insert(partij));
            
            PartijController sut = new PartijController(partijDataMapperMock.Object, spelerDataMapperMock.Object, eventSenderMock.Object);

            Task<ActionResult<PartijDTO>> result = sut.Create(new VoegPartijToeCommand()
            {
                partij = partijDTO
            });
            
            partijDataMapperMock.Verify(s => s.Insert(It.IsAny<Partij>()));
            spelerDataMapperMock.Verify(s => s.Exists(It.IsAny<string>()));
        }
        
        
        [TestMethod]
        public void CreateCallsPartijFindAllAndSpelerFindAllFromDataMapper()
        {
            var partijDataMapperMock = new Mock<IDataMapper<Partij, long>>();
            var spelerDataMapperMock = new Mock<IDataMapper<Speler, long>>();
            var eventSenderMock = new Mock<IEventSender>();
            
            Speler speler1 = new Speler()
            {
                Id = 0,
                Naam = "hans"
            };

            Speler speler2 = new Speler()
            {
                Id = 0,
                Naam = "Sjaak"
            };

            List<Speler> spelers = new List<Speler>()
            {
                speler1, speler2
            };

            Partij partij = new Partij()
            {
                Id = 1,
                Witspeler = speler1,
                Zwartspeler = speler2,
                Uitslag = 1,
            };
            
            PartijDTO partijDTO = new PartijDTO()
            {
                Id = 1,
                Witspeler = "Hans",
                Zwartspeler = "Sjaak",
                Uitslag = 1,
            };
                
            List<Partij> partijen = new List<Partij>()
            {
                partij
            };

            partijDataMapperMock.Setup(d => d.Insert(partij));
            partijDataMapperMock.Setup(d => d.FindAll()).Returns(partijen);
            spelerDataMapperMock.Setup(d => d.FindAll()).Returns(spelers);
            
            PartijController sut = new PartijController(partijDataMapperMock.Object, spelerDataMapperMock.Object, eventSenderMock.Object);

            Task<ActionResult<PartijDTO>> result = sut.Create(new VoegPartijToeCommand()
            {
                partij = partijDTO
            });
            
            partijDataMapperMock.Setup(d => d.FindAll()).Returns(It.IsAny<IEnumerable<Partij>>());
            spelerDataMapperMock.Setup(d => d.FindAll()).Returns(It.IsAny<IEnumerable<Speler>>());
        }
    }
}