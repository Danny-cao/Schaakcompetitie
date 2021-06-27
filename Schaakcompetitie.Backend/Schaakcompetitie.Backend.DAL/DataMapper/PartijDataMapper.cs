using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Schaakcompetitie.Backend.DAL.Models;

namespace Schaakcompetitie.Backend.DAL.DataMapper
{
    public class PartijDataMapper : DataMapperBase<Partij, long>
    {
        public PartijDataMapper(SchaakContext context) : base(context)
        {
        }

        public override Partij Find(long key)
        {
            return FindAll().Single(p => p.Id == key);
        }

        public override Partij FindByName(string key)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Partij> FindAll()
        {
            return Context.Partijen.Include(s => s.Witspeler).Include(s => s.Zwartspeler);
        }

        public override IEnumerable<Partij> FindBy(Expression<Func<Partij, bool>> clause)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Partij item)
        {
            Context.Add(item);
            Context.SaveChanges();
        }

        public override void Update(Partij item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long key)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(string item)
        {
            throw new NotImplementedException();
        }
    }
}