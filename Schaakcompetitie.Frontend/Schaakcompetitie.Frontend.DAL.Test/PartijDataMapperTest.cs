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
    public class PartijDataMapperTest
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
        public void InsertShouldInsertPartij()
        {
            using var context = new FrontendContext(_options);
            var mapper = new PartijDataMapper(context);

            List<Partij> partijen = new List<Partij>();

            
            Partij partij = new Partij
            {
                Witspeler = "Pieter",
                Zwartspeler = "Hans",
                Uitslag = 1,
            };
            partijen.Add(partij);
            mapper.Insert(partij);

            var results = context.Partijen.ToList();

            Assert.AreEqual(partijen.Count, results.Count);
            CollectionAssert.AreEquivalent(partijen, results);
        }

        [TestMethod]
        public void FindAllShouldReturnAllPartijen()
        {
            using var context = new FrontendContext(_options);
            var mapper = new PartijDataMapper(context);

            mapper.Insert(new Partij
            {
                Witspeler = "Hans",
                Zwartspeler = "Jan",
                Uitslag = 1,
            });

            List<Partij> results = mapper.FindAll().ToList();

            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.Any(p => p.Witspeler == "Hans"));
            Assert.IsTrue(results.Any(p => p.Zwartspeler == "Jan"));
            Assert.IsTrue(results.Any(p => p.Uitslag == 1));
        }
    }
}