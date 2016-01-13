using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using ServiceLayer.Common;

namespace ServiceLayer.ServicesImplementation
{
    public class SubscriptionSerivce : ISubscriptionService
    {

        private ServiceProviderLogger logger;

        public SubscriptionSerivce()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddSubscription(Subscription subscription, SubscriptionType subType, Currency currency)
        {
            throw new NotImplementedException();
        }
    }
}
