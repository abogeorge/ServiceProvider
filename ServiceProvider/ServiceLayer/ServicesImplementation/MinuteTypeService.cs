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
    public class MinuteTypeService : IMinuteTypeService
    {
        private ServiceProviderLogger logger;

        public MinuteTypeService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddMinuteType(MinuteType minuteType)
        {
            logger.logInfo("Attempting to add a new minute type ... ");

            var validationResult = Validation.Validate<MinuteType>(minuteType);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for minuteType type " + minuteType.MinuteTypeName;
                logger.logError(message);
                throw new ValidationException(message);
            }

            MinuteType oldMinuteType = GetMinuteTypeByName(minuteType.MinuteTypeName);
            if (oldMinuteType != null)
            {
                String message = "Minute Type " + minuteType.MinuteTypeName + " is already registered.";
                logger.logError(message);
                throw new DuplicateException(message);
            }

            DataMapperFactoryMethod.GetCurrentFactory().MinuteTypeFactory.AddMinuteType(minuteType);
            logger.logInfo("Add minute type operation ended.");
        }

        public void DropMinuteType(string minuteTypeName)
        {
            logger.logInfo("Attempting to drop minute type ...");
            if (minuteTypeName.Equals(""))
            {
                String message = "Minute Type Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().MinuteTypeFactory.DropMinuteType(minuteTypeName);
            logger.logInfo("Drop minute type operation ended.");
        }

        public MinuteType GetMinuteTypeByName(string minuteTypeName)
        {
            logger.logInfo("Attempting to retrieve minute type by name ...");
            if (minuteTypeName.Equals(""))
            {
                String message = "Minute Type Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve Minute Type by Name operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().MinuteTypeFactory.GetMinuteTypeByName(minuteTypeName);
        }
    }
}
