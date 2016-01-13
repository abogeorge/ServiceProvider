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
    }
}
