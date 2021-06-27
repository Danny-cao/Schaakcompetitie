using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Schaakcompetitie.Frontend.DAL.DataMappers;
using Schaakcompetitie.Frontend.DAL.Models;
using Schaakcompetitie.Frontend.EventListeners;
using Schaakcompetitie.Frontend.EventListeners.Events;

namespace Schaakcompetitie.Frontend.Test
{
    [TestClass]
    public class StandBerekendEventListenerTest
    {
        [TestMethod]
        public void TestStandBerekendShouldCallInsertAndExists()
        {
            // Arrange
            var dataMapperMock = new Mock<IDataMapper<Stand, long>>();

            var sut = new StandBerekendEventListener(dataMapperMock.Object);

            var stand = new Stand() {Id = 1, Speler = "Hans", Gelijk = 1, Gewonnen = 1, Verloren = 1, Partij = 3};
            
            IEnumerable<Stand> standen = new List<Stand>()
            {
                stand
            };
            
            
            // Act
            sut.Handler(new StandBerekendEvent() {Standen = standen});

            dataMapperMock.Verify(dm => dm.Exists(stand));
            dataMapperMock.Verify(dm => dm.Insert(stand));
            dataMapperMock.VerifyNoOtherCalls();
        }
        
        [TestMethod]
        public void TestStandBerekendShouldCallFinbyNameAndDeleteIfExists()
        {
            // Arrange
            var dataMapperMock = new Mock<IDataMapper<Stand, long>>();

            var sut = new StandBerekendEventListener(dataMapperMock.Object);

            
            
            var stand = new Stand() {Id = 1, Speler = "Hans", Gelijk = 1, Gewonnen = 1, Verloren = 1, Partij = 3};
            
            IEnumerable<Stand> standen = new List<Stand>()
            {
                stand
            };
            
            dataMapperMock.Setup(datamapper => datamapper.Exists(It.IsAny<Stand>())).Returns(true);
            
            // Act
            sut.Handler(new StandBerekendEvent() {Standen = standen});
            dataMapperMock.Verify(dm => dm.FindByName(It.IsAny<Stand>()));
            dataMapperMock.Verify(dm => dm.Delete(It.IsAny<Stand>()));
        }
    }
}