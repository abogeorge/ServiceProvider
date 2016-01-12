using DataMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DataMapper.Implementation
{
    class EFMinuteFactory : IMinuteFactory
    {
        public void AddMinute(Minute minute)
        {
            using (var context = new DataMapperContext())
            {
                context.Minutes.Add(minute);
                context.SaveChanges();
            }
        }

        public void DropMinute(int id)
        {
            throw new NotImplementedException();
        }

        public Minute GetMinuteById(int id)
        {
            using (var context = new DataMapperContext())
            {
                var customerVar = (from minute in context.Minutes
                                   where minute.MinuteId == id
                                   select minute).FirstOrDefault();
                return customerVar;
            }
        }

        public void UpdateExtraCharge(Minute minute)
        {
            throw new NotImplementedException();
        }

        public void UpdateIncludedMinutes(Minute minute)
        {
            throw new NotImplementedException();
        }
    }
}
