using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Roger.MicroServices.Events;
using Schaakcompetitie.Backend.Controllers;
using Schaakcompetitie.Backend.DAL.DataMapper;
using Schaakcompetitie.Backend.DAL.DTO;
using Schaakcompetitie.Backend.DAL.Models;

namespace Schaakcompetitie.Backend.Test
{
    [TestClass]
    public class StandControllerTest
    {
        [TestMethod]
        public void ShouldCallPartijAndSpelerDataMapper()
        {
            // Arrange 
            var partijDataMapperMock = new Mock<IDataMapper<Partij, long>>();
            var spelerDataMapperMock = new Mock<IDataMapper<Speler, long>>();

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

            List<Partij> partijen = new List<Partij>()
            {
                partij
            };

            List<Stand> standen = new List<Stand>()
            {
                new Stand()
                {
                    Speler = speler1,
                    Partij = 1,
                    Gewonnen = 1,
                    Verloren = 0,
                    Gelijk = 0,
                    Score = 1f
                },
                new Stand()
                {
                    Speler = speler2,
                    Partij = 1,
                    Gewonnen = 0,
                    Verloren = 1,
                    Gelijk = 0,
                    Score = 0f
                }
            };

            partijDataMapperMock.Setup(d => d.FindAll()).Returns(partijen);
            spelerDataMapperMock.Setup(d => d.FindAll()).Returns(spelers);
            partijDataMapperMock.Setup(d => d.Insert(partij));

            var sut = new StandController(partijDataMapperMock.Object, spelerDataMapperMock.Object);

            var result = sut.Get();

            partijDataMapperMock.Verify(s => s.FindAll());
            spelerDataMapperMock.Verify(s => s.FindAll());
        }
    }
}