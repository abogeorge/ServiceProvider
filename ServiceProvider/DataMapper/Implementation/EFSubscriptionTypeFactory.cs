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
    class EFSubscriptionTypeFactory : ISubscriptionTypeFactory
    {
        public void AddSubscriptionType(SubscriptionType subscriptionType)
        {
            using (var context = new DataMapperContext())
            {
                context.SubscriptionTypes.Add(subscriptionType);
                context.SaveChanges();
            }
        }

        public void DropSubscriptionType(string subscriptionTypeName)
        {
            SubscriptionType subType = GetSubscriptionTypeByName(subscriptionTypeName);
            if (subType == null)
            {
                throw new EntityDoesNotExistException("Invalid Subscription Type Name.");
            }

            using (var context = new DataMapperContext())
            {
                context.SubscriptionTypes.Attach(subType);
                context.SubscriptionTypes.Remove(subType);
                context.SaveChanges();
            }
        }

        public SubscriptionType GetSubscriptionTypeByName(string subscriptionTypeName)
        {
            using (var context = new DataMapperContext())
            {
                var subTypeVar = (from subType in context.SubscriptionTypes
                                   where subType.SubscriptionTypeName.Equals(subscriptionTypeName)
                                   select subType).FirstOrDefault();
                return subTypeVar;
            }
        }
    }
}
