using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface ISubscriptionTypeFactory
    {
        void AddSubscriptionType(SubscriptionType subscriptionType);
        SubscriptionType GetSubscriptionTypeByName(String subscriptionTypeName);
        void DropSubscriptionType(String subscriptionTypeName);
    }
}
