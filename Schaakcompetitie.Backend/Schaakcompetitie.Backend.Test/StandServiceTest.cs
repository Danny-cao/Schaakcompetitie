using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Roger.MicroServices.Events;
using Schaakcompetitie.Backend.DAL.Models;
using Schaakcompetitie.Backend.Events;
using Schaakcompetitie.Backend.Services;

namespace Schaakcompetitie.Backend.Test
{
    [TestClass]
    public class StandServiceTest
    {
        [TestMethod]
        public async Task shouldCallSendEvent()
        {
            var eventSenderMock = new Mock<IEventSender>();

            List<Partij> partijen = new List<Partij>()
            {
                new Partij()
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
                },
            };

            List<Speler> spelers = new List<Speler>()
            {
                new Speler()
                {
                    Naam = "Hans",
                },

                new Speler()
                {
                    Naam = "Sjaak",
                },
            };

            var sut = new StandService(eventSenderMock.Object, spelers, partijen);

            await sut.sendEvent();

            eventSenderMock.Verify(es => es.SendAsync(It.IsAny<StandBerekendEvent>()));
        }
    }
}