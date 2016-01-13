using DataMapper.Interfaces;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Implementation
{
    class EFSubscriptionFactory : ISubscriptionFactory
    {
        public void AddSubscription(Subscription subscription, SubscriptionType subType, Currency currency)
        {
            using (var context = new DataMapperContext())
            {
                context.SubscriptionTypes.Attach(subType);
                context.Currencies.Attach(currency);
                context.Subscriptions.Attach(subscription);
                context.Entry(subType).Collection(c => c.Subscriptions).Load();
                context.Entry(currency).Collection(c => c.Subscriptions).Load();
                context.Subscriptions.Add(subscription);
                context.SaveChanges();
            }
        }
    }
}
