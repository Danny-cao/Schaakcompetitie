using System.Linq;
using Schaakcompetitie.Frontend.DAL.Models;

namespace Schaakcompetitie.Frontend.DAL.DataMappers
{
    public class PartijDataMapper : DataMapperBase<Partij, long>
    {
        public PartijDataMapper(FrontendContext context) : base(context)
        {
        }

        public override void Insert(Partij item)
        {
            Context.Add(item);
            Context.SaveChanges();
        }

        public override Partij FindByName(Partij item)
        {
            throw new System.NotImplementedException();
        }

        protected override IQueryable<Partij> GetAll()
        {
            return Context.Partijen;
        }

        protected override bool _Exists(Partij item)
        {
            throw new System.NotImplementedException();
        }
    }
}