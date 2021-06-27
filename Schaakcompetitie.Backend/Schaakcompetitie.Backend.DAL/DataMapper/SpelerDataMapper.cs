using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Schaakcompetitie.Backend.DAL.Models;

namespace Schaakcompetitie.Backend.DAL.DataMapper
{
    public class SpelerDataMapper : DataMapperBase<Speler, long>
    {
        public SpelerDataMapper(SchaakContext context) : base(context)
        {
        }

        public override Speler Find(long key)
        {
            return FindAll().Single(k => k.Id == key);
        }

        public override Speler FindByName(string key)
        {
            return FindAll().Single(k => k.Naam == key);
        }

        public override IEnumerable<Speler> FindAll()
        {
            return Context.Spelers;
        }

        public override IEnumerable<Speler> FindBy(Expression<Func<Speler, bool>> clause)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Speler item)
        {
            Context.Spelers.Add(item);
            Context.SaveChanges();
        }

        public override void Update(Speler item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long key)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(string item)
        {
            return FindAll().Any(n => n.Naam.ToLower() == item.ToLower());
        }
    }
}