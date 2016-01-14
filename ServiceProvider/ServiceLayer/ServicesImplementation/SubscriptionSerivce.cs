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
    public class SubscriptionSerivce : ISubscriptionService
    {

        private ServiceProviderLogger logger;

        public SubscriptionSerivce()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddSubscription(Subscription subscription, SubscriptionType subType, Currency currency)
        {
            logger.logInfo("Attempting to add a new subscription ... ");

            Subscription oldSub = GetSubscriptionByName(subscription.SubscriptionName);
            if(oldSub != null)
            {
                String message = "Subscription already exists.";
                logger.logError(message);
                throw new DuplicateException(message);
            }

            var validationResult = Validation.Validate<Subscription>(subscription);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for Subscription.";
                logger.logError(message);
                throw new ValidationException(message);
            }

            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().SubscriptionFactory.AddSubscription(subscription, subType, currency);
            logger.logInfo("Add message operation ended.");
        }

        public Subscription GetSubscriptionByName(string subName)
        {
            logger.logInfo("Attempting to retrieve subscription by name ...");
            if (subName.Equals(""))
            {
                String message = "Subscription name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve subscription operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().SubscriptionFactory.GetSubscriptionByName(subName);
        }

        public void UpdateSubscriptionPrice(String subName, Double price)
        {
            logger.logInfo("Attempting to edit subscription price ... ");
            Subscription subscription = GetSubscriptionByName(subName);
            if (subscription == null)
            {
                String message = "The subscription does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            subscription.Price = price;
            var validationResult = Validation.Validate<Subscription>(subscription);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for subscription";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().SubscriptionFactory.UpdateSubscriptionPrice(subscription);
            logger.logInfo("Edit subscription price operation ended.");
        }

        public void UpdateSubscriptionFixedPeriod(string subName, int period)
        {
            logger.logInfo("Attempting to edit subscription period ... ");
            Subscription subscription = GetSubscriptionByName(subName);
            if (subscription == null)
            {
                String message = "The subscription does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            subscription.FixedPeriod = period;
            var validationResult = Validation.Validate<Subscription>(subscription);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for subscription";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().SubscriptionFactory.UpdateSubscriptionFixedPeriod(subscription);
            logger.logInfo("Edit subscription period operation ended.");
        }

        public void UpdateSubscriptionAvailability(string subName, bool av)
        {
            logger.logInfo("Attempting to edit subscription availability ... ");
            Subscription subscription = GetSubscriptionByName(subName);
            if (subscription == null)
            {
                String message = "The subscription does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            subscription.Available = av;
            DataMapperFactoryMethod.GetCurrentFactory().SubscriptionFactory.UpdateSubscriptionAvailability(subscription);
            logger.logInfo("Edit subscription availability operation ended.");
        }

        public void DropSubscription(String subName, SubscriptionType subscriptionType, Currency currency)
        {
            logger.logInfo("Attempting to drop subscription ...");
            if (subName.Equals(""))
            {
                String message = "Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            Subscription subscription = GetSubscriptionByName(subName);
            if (subscription == null)
            {
                String message = "The subscription does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().SubscriptionFactory.DropSubscriptionByName(subscription, subscriptionType, currency);
            logger.logInfo("Drop subscription operation ended.");
        }
    }
}
