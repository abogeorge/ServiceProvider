using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface ISubscriptionFactory
    {
        void AddSubscription(Subscription subscription, SubscriptionType subType, Currency currency);
        Subscription GetSubscriptionByName(String subName);
        void UpdateSubscriptionPrice(Subscription subscription);
        void UpdateSubscriptionFixedPeriod(Subscription subscription);
        void UpdateSubscriptionAvailability(Subscription subscription);
        void UpdateSubscriptionEndDate(Subscription subscription);
        void DropSubscriptionByName(Subscription subscription, SubscriptionType subscriptionType, Currency currency);
        
    }
}
