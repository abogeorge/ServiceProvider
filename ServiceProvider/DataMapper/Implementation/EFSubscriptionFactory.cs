using DataMapper.Exceptions;
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

        public Subscription GetSubscriptionByName(string subName)
        {
            using (var context = new DataMapperContext())
            {
                var subVar = (from subscription in context.Subscriptions
                                   where subscription.SubscriptionName.Equals(subName)
                                   select subscription).FirstOrDefault();
                return subVar;
            }
        }


        public void UpdateSubscriptionPrice(Subscription subscription)
        {
            using (var context = new DataMapperContext())
            {
                context.Subscriptions.Attach(subscription);
                var entry = context.Entry(subscription);
                entry.Property(u => u.Price).IsModified = true;
                context.SaveChanges();
            }
        }
        public void UpdateSubscriptionFixedPeriod(Subscription subscription)
        {
            using (var context = new DataMapperContext())
            {
                context.Subscriptions.Attach(subscription);
                var entry = context.Entry(subscription);
                entry.Property(u => u.FixedPeriod).IsModified = true;
                context.SaveChanges();
            }
        }

        public void UpdateSubscriptionAvailability(Subscription subscription)
        {
            using (var context = new DataMapperContext())
            {
                context.Subscriptions.Attach(subscription);
                var entry = context.Entry(subscription);
                entry.Property(u => u.Available).IsModified = true;
                context.SaveChanges();
            }
        }

        public void DropSubscriptionByName(Subscription subscription, SubscriptionType subscriptionType, Currency currency)
        {
            
            using (var context = new DataMapperContext())
            {
                var subType = context.SubscriptionTypes.Find(subscriptionType.SubscriptionTypeId);
                var cur = context.Currencies.Find(currency.CurrencyId);
                var sub = context.Subscriptions.Find(subscription.SubscriptionId);
                context.Entry(subType).Collection("Subscriptions").Load();
                context.Entry(cur).Collection("Subscriptions").Load();
                subType.Subscriptions.Remove(sub);
                cur.Subscriptions.Remove(sub);
                context.Subscriptions.Attach(sub);
                context.Subscriptions.Remove(sub);
                context.SaveChanges();
                
            }
        }
    }
}
