using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Schaakcompetitie.Frontend.Controllers;
using Schaakcompetitie.Frontend.DAL.DataMappers;
using Schaakcompetitie.Frontend.DAL.Models;
using Schaakcompetitie.Frontend.Models;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Schaakcompetitie.Frontend.Test
{
    [TestClass]
    public class HomeControllerTest
    {
        private HttpTest _httpTest;
        private HomeController _target;
        private readonly string _schaakApiUrl = "http://schaakapiservice-service";

        [TestInitialize]
        public void InitializeTests()
        {
            _httpTest = new HttpTest();            
            
            var partijDataMapperMock = new Mock<IDataMapper<Partij, long>>();
            var standDataMapperMock = new Mock<IDataMapper<Stand, long>>();


            _target = new HomeController(standDataMapperMock.Object, partijDataMapperMock.Object);
        }

        [TestCleanup]
        public void CleanupTests() {
            _httpTest.Dispose();
		}

        [TestMethod]
        public async Task ShouldShowAllStanden()
        {
            
            var partijDataMapperMock = new Mock<IDataMapper<Partij, long>>();
            var standDataMapperMock = new Mock<IDataMapper<Stand, long>>();



            IEnumerable<Stand> standen = new List<Stand>()
            {
                new Stand() { Speler = "Sjaak", Partij = 3, Gelijk = 1, Gewonnen = 1 , Verloren = 1, Score = 1.5f},
                new Stand() { Speler = "hans", Partij = 3, Gelijk = 1, Gewonnen = 1 , Verloren = 1, Score = 1.5f},
            };
            
            standDataMapperMock.Setup(datamapper => datamapper.FindAll()).Returns(standen);

            var target = new HomeController(standDataMapperMock.Object, partijDataMapperMock.Object);
            
            
            // Act
            IActionResult result = await target.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(IEnumerable<Stand>));
            IEnumerable<Stand> model = viewResult.Model as IEnumerable<Stand>;
            Assert.AreEqual(2, model.Count());
            Assert.IsTrue(model.Any(stand =>

                stand.Speler == "hans" &&
                stand.Partij == 3 &&
                stand.Gelijk == 1 &&
                stand.Gewonnen == 1 &&
                stand.Verloren == 1 ));
        }

        [TestMethod]
        public void showCreateView()
        {
            var result = _target.Create();
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        
        [TestMethod]
        public void ShouldReturnAViewResult()
        {
            var result = _target.Index();
            
            Assert.IsInstanceOfType(result.Result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task CreateShouldCallFlurlPost()
        {
            // Arrange
            PartijViewModel partij = new PartijViewModel()
            {
                Witspeler = "Hans",
                Zwartspeler = "Sjaak",
                Uitslag = 1,
            };

            _httpTest.ForCallsTo($"{_schaakApiUrl}/partij")
                .WithVerb(HttpMethod.Post)
                .RespondWithJson(partij);
            
            // Act
            await _target.Create(partij);

            // Assert
            _httpTest.ShouldHaveCalled($"{_schaakApiUrl}/partij")
                .WithVerb(HttpMethod.Post);
        }
        
        [TestMethod]
        public async Task CreateShouldCallInsert()
        {
            
            var partijDataMapperMock = new Mock<IDataMapper<Partij, long>>();
            var standDataMapperMock = new Mock<IDataMapper<Stand, long>>();

            PartijViewModel partij = new PartijViewModel()
            {
                Witspeler = "Hans",
                Zwartspeler = "Sjaak",
                Uitslag = 1,
            };

            var target = new HomeController(standDataMapperMock.Object, partijDataMapperMock.Object);

            await target.Create(partij);
            
            
            partijDataMapperMock.Verify(dm => dm.Insert(It.IsAny<Partij>()));
            
        }
    }
}