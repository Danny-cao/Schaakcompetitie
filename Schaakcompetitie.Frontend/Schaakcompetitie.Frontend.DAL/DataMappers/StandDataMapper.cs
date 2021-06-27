using System.Linq;
using Schaakcompetitie.Frontend.DAL.Models;

namespace Schaakcompetitie.Frontend.DAL.DataMappers
{
    public class StandDataMapper : DataMapperBase<Stand, long>
    {
        public StandDataMapper(FrontendContext context) : base(context)
        {
        }

        public override Stand FindByName(Stand item)
        {
            return GetAll().Single(s => s.Speler == item.Speler);
        }

        public override void Insert(Stand item)
        {
            Context.Add(item);
            Context.SaveChanges();
        }

        protected override IQueryable<Stand> GetAll()
        {
            return Context.Standen;
        }

        protected override bool _Exists(Stand item)
        {
            return GetAll()
                .Any(k => k.Speler == item.Speler);
        }
    }
}