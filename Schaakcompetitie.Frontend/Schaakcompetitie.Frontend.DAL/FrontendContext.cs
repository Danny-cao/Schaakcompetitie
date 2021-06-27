using Microsoft.EntityFrameworkCore;
using Schaakcompetitie.Frontend.DAL.Models;

namespace Schaakcompetitie.Frontend.DAL
{
    public class FrontendContext : DbContext
    {
        public FrontendContext(DbContextOptions<FrontendContext> options) : base(options)
        {
        }
        
        public DbSet<Partij> Partijen { get; set; }
        public DbSet<Stand> Standen { get; set; }
    }
}