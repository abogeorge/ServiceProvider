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
    class EFMinuteTypeFactory : IMinuteTypeFactory
    {
        public void AddMinuteType(MinuteType minuteType)
        {
            using (var context = new DataMapperContext())
            {
                context.MinuteTypes.Add(minuteType);
                context.SaveChanges();
            }
        }

        public void DropMinuteType(string minuteTypeName)
        {
            MinuteType minuteType = GetMinuteTypeByName(minuteTypeName);
            if (minuteType == null)
            {
                throw new EntityDoesNotExistException("Invalid Minute Type Name.");
            }

            using (var context = new DataMapperContext())
            {
                context.MinuteTypes.Attach(minuteType);
                context.MinuteTypes.Remove(minuteType);
                context.SaveChanges();
            }
        }

        public MinuteType GetMinuteTypeByName(string minuteTypeName)
        {
            using (var context = new DataMapperContext())
            {
                var minTypeVar = (from subMinute in context.MinuteTypes
                                  where subMinute.MinuteTypeName.Equals(minuteTypeName)
                                  select subMinute).FirstOrDefault();
                return minTypeVar;
            }
        }
    }
}
