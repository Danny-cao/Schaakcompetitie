using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schaakcompetitie.Frontend.DAL.DataMappers;
using Schaakcompetitie.Frontend.DAL.Models;

namespace Schaakcompetitie.Frontend.DAL.Test
{
    [TestClass]
    public class StandDataMapperTest
    {
        private SqliteConnection _connection;
        private DbContextOptions<FrontendContext> _options;

        [TestInitialize]
        public void TestInit()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<FrontendContext>()
                .UseSqlite(_connection)
                .Options;

            using(var ctx = new FrontendContext(_options))
            {
                ctx.Database.EnsureCreated();
            }

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _connection.Dispose();
        }

        [TestMethod]
        public void InsertShouldInsertStand()
        {
            using var context = new FrontendContext(_options);
            var mapper = new StandDataMapper(context);

            List<Stand> standen = new List<Stand>();

            
            Stand stand = new Stand
            {
                Speler = "Hans",
                Gelijk = 1,
                Gewonnen = 1,
                Partij = 3,
                Verloren = 1,
            };
            
            standen.Add(stand);
            mapper.Insert(stand);

            var results = context.Standen.ToList();

            Assert.AreEqual(standen.Count, results.Count);
            CollectionAssert.AreEquivalent(standen, results);
        }

        [TestMethod]
        public void FindAll()
        {
            using var context = new FrontendContext(_options);
            var mapper = new StandDataMapper(context);

            mapper.Insert(new Stand
            {
                Speler = "Hans",
                Gelijk = 1,
                Gewonnen = 1,
                Partij = 3,
                Verloren = 1,
            });

            List<Stand> results = mapper.FindAll().ToList();

            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.Any(p => p.Speler == "Hans"));
            Assert.IsTrue(results.Any(p => p.Gelijk == 1));
            Assert.IsTrue(results.Any(p => p.Gewonnen == 1));
            Assert.IsTrue(results.Any(p => p.Partij == 3));
            Assert.IsTrue(results.Any(p => p.Verloren == 1));
        }
    }
}