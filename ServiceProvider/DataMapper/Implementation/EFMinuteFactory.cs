using DataMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DataMapper.Exceptions;

namespace DataMapper.Implementation
{
    class EFMinuteFactory : IMinuteFactory
    {
        public void AddMinute(Minute minute, MinuteType minuteType)
        {
            using (var context = new DataMapperContext())
            {
                context.MinuteTypes.Attach(minuteType);
                context.Minutes.Attach(minute);
                context.Entry(minuteType).Collection(c => c.Minutes).Load();
                context.Minutes.Add(minute);
                context.SaveChanges();
            }
            
        }

        public void DropMinute(int id, MinuteType minuteType)
        {
            Minute minute = GetMinuteById(id, minuteType);
            if (minute == null)
            {
                throw new EntityDoesNotExistException("Invalid ID.");
            }

            using (var context = new DataMapperContext())
            {
                var minType = context.MinuteTypes.Find(minuteType.MinuteTypeId);
                var min = context.Minutes.Find(minute.MinuteId);
                context.Entry(minType).Collection("Minutes").Load();
                minType.Minutes.Remove(min);
                context.Minutes.Attach(min);
                context.Minutes.Remove(min);
                context.SaveChanges();
            }
        }

        public Minute GetMinuteById(int id, MinuteType minuteType)
        {
            using (var context = new DataMapperContext())
            {
                var customerVar = (from minute in context.Minutes
                                   where minute.MinuteId == id &&
                                   minute.MinuteType.MinuteTypeId == minuteType.MinuteTypeId
                                   select minute).FirstOrDefault();
                return customerVar;
            }
        }

        public void UpdateExtraCharge(Minute minute)
        {
            using (var context = new DataMapperContext())
            {
                context.Minutes.Attach(minute);
                var entry = context.Entry(minute);
                entry.Property(m => m.ExtraCharge).IsModified = true;
                context.SaveChanges();
            }

        }

        public void UpdateIncludedMinutes(Minute minute)
        {
            using (var context = new DataMapperContext())
            {
                context.Minutes.Attach(minute);
                var entry = context.Entry(minute);
                entry.Property(m => m.IncludedMinutes).IsModified = true;
                context.SaveChanges();
            }
        }
    }
}
