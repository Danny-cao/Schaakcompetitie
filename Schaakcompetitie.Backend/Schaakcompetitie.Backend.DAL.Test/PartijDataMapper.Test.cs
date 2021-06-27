using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schaakcompetitie.Backend.DAL.DataMapper;
using Schaakcompetitie.Backend.DAL.Models;

namespace Schaakcompetitie.Backend.DAL.Test
{
    [TestClass]
    public class PartijDataMapper_Test
    {
        private SqliteConnection _connection;
        private DbContextOptions<SchaakContext> _options;

        [TestInitialize]
        public void TestInit()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<SchaakContext>()
                .UseSqlite(_connection)
                .Options;

            using(var ctx = new SchaakContext(_options))
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
        public void Insert()
        {
            using var context = new SchaakContext(_options);
            var mapper = new PartijDataMapper(context);
            
            Speler speler1 = new Speler()
            {
                Naam = "Hans",
            };
            
            Speler speler2 = new Speler()
            {
                Naam = "Jan",
            };

            List<Partij> partijen = new List<Partij>();

            
            Partij partij = new Partij
            {
                Witspeler = speler1,
                Zwartspeler = speler2,
                Uitslag = 1,
            };
            partijen.Add(partij);
            mapper.Insert(partij);

            var results = context.Partijen.ToList();

            Assert.AreEqual(partijen.Count, results.Count);
            CollectionAssert.AreEquivalent(partijen, results);
        }

        [TestMethod]
        public void Findall()
        {
            using var context = new SchaakContext(_options);
            var mapper = new PartijDataMapper(context);

            Speler speler1 = new Speler()
            {
                Naam = "Hans",
            };
            
            Speler speler2 = new Speler()
            {
                Naam = "Jan",
            };
            
            mapper.Insert(new Partij
            {
                Witspeler = speler1,
                Zwartspeler = speler2,
                Uitslag = 1,
            });

            List<Partij> results = mapper.FindAll().ToList();

            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.Any(p => p.Witspeler.Naam == "Hans"));
            Assert.IsTrue(results.Any(p => p.Zwartspeler.Naam == "Jan"));
            Assert.IsTrue(results.Any(p => p.Uitslag == 1));
        }
        
        [TestMethod]
        public void FindShouldReturnOnePartij()
        {
            using var context = new SchaakContext(_options);
            var mapper = new PartijDataMapper(context);

            Speler speler1 = new Speler()
            {
                Id = 1,
                Naam = "Hans",
            };
            
            Speler speler2 = new Speler()
            {
                Id = 2,
                Naam = "Jan",
            };

            mapper.Insert(new Partij
            {
                Witspeler = speler1,
                Zwartspeler = speler2,
                Uitslag = 1,
            });

            Partij results = mapper.Find(1);
            
            Assert.IsTrue(results.Witspeler.Naam == "Hans");
            Assert.IsTrue(results.Zwartspeler.Naam == "Jan");
            Assert.IsTrue(results.Uitslag == 1);
        }
    }
}