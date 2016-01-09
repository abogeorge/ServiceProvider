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
    public class MessageTypeService : IMessageTypeService
    {
        private ServiceProviderLogger logger;

        public MessageTypeService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddMessageType(MessageType messageType)
        {
            logger.logInfo("Attempting to add a new message type ... ");

            var validationResult = Validation.Validate<MessageType>(messageType);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for message type " + messageType.MessageTypeName;
                logger.logError(message);
                throw new ValidationException(message);
            }

            MessageType oldMessageType = GetMessageTypeByName(messageType.MessageTypeName);
            if (oldMessageType != null)
            {
                String message = "Message Type " + messageType.MessageTypeName + " is already registered.";
                logger.logError(message);
                throw new DuplicateException(message);
            }

            DataMapperFactoryMethod.GetCurrentFactory().MessageTypeFactory.AddMessageType(messageType);
            logger.logInfo("Add message type operation ended.");
        }

        public void DropMessageType(string messageTypeName)
        {
            logger.logInfo("Attempting to drop message type ...");
            if (messageTypeName.Equals(""))
            {
                String message = "Message Type Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().MessageTypeFactory.DropMessageType(messageTypeName);
            logger.logInfo("Drop message type operation ended.");
        }

        public MessageType GetMessageTypeByName(string messageTypeName)
        {
            logger.logInfo("Attempting to retrieve message type by name ...");
            if (messageTypeName.Equals(""))
            {
                String message = "Message Type Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve Subscription Type by Name operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().MessageTypeFactory.GetMessageTypeByName(messageTypeName);
        }
    }
}
