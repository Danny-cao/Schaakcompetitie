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
    public class SpelerDataMapper_Test
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

            Speler speler1 = new Speler()
            {
                Naam = "Hans",
            };

            context.Add(speler1);
            context.SaveChanges();

            var results = context.Spelers.ToList().Single(n => n.Naam == "Hans");
            
            Assert.AreEqual(speler1, results);
        }

        [TestMethod]
        public void Findall()
        {
            using var context = new SchaakContext(_options);
            var mapper = new SpelerDataMapper(context);

            mapper.Insert(new Speler()
            {
                Naam = "Hans"
            });
            
            mapper.Insert(new Speler()
            {
                Naam = "Jan"
            });

            List<Speler> results = mapper.FindAll().ToList();

            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Any(p => p.Naam == "Hans"));
        }
        
        [TestMethod]
        public void FindShouldReturnOneSpeler()
        {
            using var context = new SchaakContext(_options);
            var mapper = new SpelerDataMapper(context);
            
            mapper.Insert(new Speler()
            {
                Id = 1,
                Naam = "Jan"
            });

            Speler results = mapper.Find(1);
            
            Assert.IsTrue(results.Naam == "Jan");
        }
        
        [TestMethod]
        public void ExistShouldReturnTrueIfExists()
        {
            using var context = new SchaakContext(_options);
            var mapper = new SpelerDataMapper(context);
            
            mapper.Insert(new Speler()
            {
                Id = 1,
                Naam = "Jan"
            });

            bool results = mapper.Exists("Jan");
            
            Assert.IsTrue(results);
        }
    }
}