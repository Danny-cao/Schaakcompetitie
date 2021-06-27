using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Roger.MicroServices.Events;
using Schaakcompetitie.Backend.DAL.Models;
using Schaakcompetitie.Backend.Events;
using Schaakcompetitie.Backend.Services;

namespace Schaakcompetitie.Backend.Test
{
    [TestClass]
    public class PartijServiceTest
    {
        [TestMethod]
        public async Task shouldCallSendEvent()
        {
            var eventSenderMock = new Mock<IEventSender>();

            Partij partij = new Partij()
            {
                Witspeler = new Speler()
                {
                    Naam = "Hans",
                },
                Zwartspeler = new Speler()
                {
                    Naam = "Sjaak",
                },
                Uitslag = 1,
            };

            var sut = new PartijService(eventSenderMock.Object, partij);

            await sut.sendEvent();

            eventSenderMock.Verify(es => es.SendAsync(It.Is<PartijToegevoegdEvent>(val => 
                val.Witspeler == "Hans" && val.Zwartspeler == "Sjaak" && val.Uitslag == 1)));
        }
    }
}