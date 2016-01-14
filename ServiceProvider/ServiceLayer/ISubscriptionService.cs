using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ISubscriptionService
    {
        void AddSubscription(Subscription subscription, SubscriptionType subType, Currency currency);
        Subscription GetSubscriptionByName(String subName);
        void UpdateSubscriptionPrice(String subName, Double price);
        void UpdateSubscriptionFixedPeriod(String subName, int period);
        void UpdateSubscriptionAvailability(String subName, bool av);
        void DropSubscription(String subName, SubscriptionType subscriptionType, Currency currency);
    }
}
