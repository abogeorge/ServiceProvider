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
    }
}
