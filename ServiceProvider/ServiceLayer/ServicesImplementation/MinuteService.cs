using DataMapper.Interfaces;
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
    public class MinuteService : IMinuteService
    {

        private ServiceProviderLogger logger;

        public MinuteService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddMinute(Minute minute)
        {
            logger.logInfo("Attempting to add a new minute ... ");

            var validationResult = Validation.Validate<Minute>(minute);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for minute.";
                logger.logError(message);
                throw new ValidationException(message);
            }

            DataMapperFactoryMethod.GetCurrentFactory().MinuteFactory.AddMinute(minute);
            logger.logInfo("Add minute operation ended.");
        }

        public void DropMinute(int id)
        {
            throw new NotImplementedException();
        }

        public Minute GetMinuteById(int id)
        {
            logger.logInfo("Attempting to retrieve minute by id ...");
            if (id == 0)
            {
                String message = "ID field is invalid!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve minute operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().MinuteFactory.GetMinuteById(id);
        }

        public void UpdateExtraCharge(Minute minute)
        {
            throw new NotImplementedException();
        }

        public void UpdateIncludedMinutes(Minute minute)
        {
            throw new NotImplementedException();
        }
    }
}
