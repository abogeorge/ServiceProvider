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
    public class CurrencyService : ICurrencyService
    {
        private ServiceProviderLogger logger;

        public CurrencyService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddCurrency(Currency currency)
        {
            logger.logInfo("Attempting to add a new currency ... ");

            var validationResult = Validation.Validate<Currency>(currency);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for currency " + currency.CurrencyName;
                logger.logError(message);
                throw new ValidationException(message);
            }

            Currency oldCurrency = GetCurrencyByName(currency.CurrencyName);
            if (oldCurrency != null)
            {
                String message = "Currency " + currency.CurrencyName + " is already registered.";
                logger.logError(message);
                throw new DuplicateException(message);
            }

            DataMapperFactoryMethod.GetCurrentFactory().CurrencyFactory.AddCurrency(currency);
            logger.logInfo("Add currency type operation ended.");
        }

        public void DropCurrency(string currencyName)
        {
            logger.logInfo("Attempting to drop currency ...");
            if (currencyName.Equals(""))
            {
                String message = "Currency Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CurrencyFactory.DropCurrency(currencyName);
            logger.logInfo("Drop currency operation ended.");
        }

        public Currency GetCurrencyByName(string currencyName)
        {
            logger.logInfo("Attempting to retrieve currency by name ...");
            if (currencyName.Equals(""))
            {
                String message = "Currency Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve Currency by Name operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().CurrencyFactory.GetCurrencyByName(currencyName);
        }
    }
}
