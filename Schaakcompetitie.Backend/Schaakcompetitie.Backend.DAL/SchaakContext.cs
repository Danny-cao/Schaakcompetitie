using Microsoft.EntityFrameworkCore;
using Schaakcompetitie.Backend.DAL.Models;

namespace Schaakcompetitie.Backend.DAL
{
    public class SchaakContext : DbContext
    {
        public SchaakContext(DbContextOptions<SchaakContext> options) : base(options) 
        {
        }
        
        public DbSet<Speler> Spelers { get; set; }
        public DbSet<Partij> Partijen { get; set; }
    }
}