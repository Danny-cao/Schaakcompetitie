using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schaakcompetitie.Backend.DAL.DTO;
using Schaakcompetitie.Backend.DAL.Models;
using Schaakcompetitie.Backend.Services;

namespace Schaakcompetitie.Backend.Test
{
    [TestClass]
    public class CalculateStandenTest
    {
        [TestMethod]
        public void CalculateStandenShouldCalculateTheRightStanden()
        {
            
            // Arrange 
            List<Speler> spelers = new List<Speler>();
            List<Partij> partijen = new List<Partij>();
            
            
            Speler speler = new Speler()
            {
                Id = 0,
                Naam = "hans"
            };
            
            Speler speler2 = new Speler()
            {
                Id= 0,
                Naam = "Sjaak"
            };

            Partij partij1 = new Partij()
            {
                Witspeler = speler,
                Zwartspeler = speler2,
                Uitslag = 1,
            };
            
            spelers.Add(speler);
            spelers.Add(speler2);
            
            partijen.Add(partij1);
            
            List<Stand> standen = new List<Stand>()
            {
                new Stand()
                {
                    Speler = speler,
                    Partij = 1,
                    Gewonnen = 1,
                    Verloren = 0,
                    Gelijk = 0,
                    Score = 1
                },
                new Stand()
                {
                Speler = speler2,
                Partij = 1,
                Gewonnen = 0,
                Verloren = 1,
                Gelijk = 0,
                Score = 0
                }
            };

            // Act
            var sut =  new CalculateStanden(partijen, spelers);

            var result = sut.Generate();
            
            // Assert
            Assert.IsTrue(result.Any((s => s.Speler == speler.Naam)));
            Assert.IsTrue(result.Any(s => s.Speler == speler2.Naam));
            //CollectionAssert.AreEquivalent(result, standen);

        }
    }
}