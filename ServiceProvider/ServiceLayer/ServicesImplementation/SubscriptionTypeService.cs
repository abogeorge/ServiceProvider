using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using ServiceLayer.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DataMapper.Exceptions;
using DataMapper;

namespace ServiceLayer.ServicesImplementation
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private ServiceProviderLogger logger;

        public SubscriptionTypeService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddSubscriptionType(SubscriptionType subscriptionType)
        {
            logger.logInfo("Attempting to add a new subscription type ... ");

            var validationResult = Validation.Validate<SubscriptionType>(subscriptionType);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for subscription type " + subscriptionType.SubscriptionTypeName;
                logger.logError(message);
                throw new ValidationException(message);
            }

            SubscriptionType oldSubscriptionType = GetSubscriptionTypeByName(subscriptionType.SubscriptionTypeName);
            if (oldSubscriptionType != null)
            {
                String message = "Subscription Type " + subscriptionType.SubscriptionTypeName + " is already registered.";
                logger.logError(message);
                throw new DuplicateException(message);
            }

            DataMapperFactoryMethod.GetCurrentFactory().SubscriptionTypeFactory.AddSubscriptionType(subscriptionType);
            logger.logInfo("Add subscription type operation ended.");
        }

        public void DropSubscriptionType(string subscriptionTypeName)
        {
            logger.logInfo("Attempting to drop subscription type ...");
            if (subscriptionTypeName.Equals(""))
            {
                String message = "Subscription Type Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().SubscriptionTypeFactory.DropSubscriptionType(subscriptionTypeName);
            logger.logInfo("Drop subscription type operation ended.");
        }

        public SubscriptionType GetSubscriptionTypeByName(string subscriptionTypeName)
        {
            logger.logInfo("Attempting to retrieve subscription type by name ...");
            if (subscriptionTypeName.Equals(""))
            {
                String message = "Subscription Type Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve Subscription Type by Name operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().SubscriptionTypeFactory.GetSubscriptionTypeByName(subscriptionTypeName);
        }
    }
}
