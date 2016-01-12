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
    public class CurrenyRateService : ICurrencyRateService
    {

        private ServiceProviderLogger logger;

        public CurrenyRateService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddCurrencyRate(CurrencyRate currencyRate, Currency currency)
        {
            logger.logInfo("Attempting to add a new currency rate ... ");

            var validationResult = Validation.Validate<CurrencyRate>(currencyRate);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for currency rate";
                logger.logError(message);
                throw new ValidationException(message);
            }

            CurrencyRate oldCurrencyRate = GetCurrencyRateByMonth(currencyRate.Valability, currencyRate.Currency);
            if (oldCurrencyRate != null)
            {
                String message = "Currency Rate is already registered.";
                logger.logError(message);
                throw new DuplicateException(message);
            }

            DataMapperFactoryMethod.GetCurrentFactory().CurrencyRateFactory.AddCurrencyRate(currencyRate, currency);
            logger.logInfo("Add currency rate type operation ended.");
        }

        public void DropCurrencyRate(string currencyRateMonth, Currency currency)
        {
            logger.logInfo("Attempting to drop currency rate ...");
            if (currencyRateMonth.Equals(""))
            {
                String message = "Currency Rate valability field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CurrencyRateFactory.DropCurrencyRate(currencyRateMonth, currency);
            logger.logInfo("Drop currency rate operation ended.");
        }

        public CurrencyRate GetCurrencyRateByMonth(string currencyRateMonth, Currency currency)
        {
            logger.logInfo("Attempting to retrieve currency rate ...");
            if (currencyRateMonth.Equals(""))
            {
                String message = "Currency Rate field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve Currency Rate operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().CurrencyRateFactory.GetCurrencyRateByMonth(currencyRateMonth, currency);
        }
    }
}
