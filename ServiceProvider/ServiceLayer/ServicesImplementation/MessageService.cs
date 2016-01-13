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
    public class MessageService : IMessageService
    {

        private ServiceProviderLogger logger;

        public MessageService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddMessage(Message message, MessageType messageType)
        {
            logger.logInfo("Attempting to add a new message ... ");

            var validationResult = Validation.Validate<Message>(message);
            if (!validationResult.IsValid)
            {
                String messageErr = "Invalid fields for message.";
                logger.logError(messageErr);
                throw new ValidationException(messageErr);
            }

            DataMapperFactoryMethod.GetCurrentFactory().MessageFactory.AddMessage(message, messageType);
            logger.logInfo("Add message operation ended.");
        }

        public void DropMessage(int id, MessageType messageType)
        {
            logger.logInfo("Attempting to drop message ...");
            if (id == 0)
            {
                String messageEr = "Id field is null!";
                logger.logError(messageEr);
                throw new ValidationException(messageEr);
            }
            DataMapperFactoryMethod.GetCurrentFactory().MessageFactory.DropMessage(id, messageType);
            logger.logInfo("Drop message operation ended.");
        }

        public Message GetMessageById(int id, MessageType messageType)
        {
            logger.logInfo("Attempting to retrieve message by id ...");
            if (id == 0)
            {
                String messageEr = "ID field is invalid!";
                logger.logError(messageEr);
                throw new ValidationException(messageEr);
            }
            logger.logInfo("Retrieve message operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().MessageFactory.GetMessageById(id, messageType);
        }

        public void UpdateExtraCharge(Message message)
        {
            logger.logInfo("Attempting to edit message extra charge ...");
            var validationResult = Validation.Validate<Message>(message);
            if (!validationResult.IsValid)
            {
                String messageEr = "Invalid fields for message.";
                logger.logError(messageEr);
                throw new ValidationException(messageEr);
            }
            DataMapperFactoryMethod.GetCurrentFactory().MessageFactory.UpdateExtraCharge(message);
        }

        public void UpdateIncludedMessages(Message message)
        {
            logger.logInfo("Attempting to edit message included messages ...");
            var validationResult = Validation.Validate<Message>(message);
            if (!validationResult.IsValid)
            {
                String messageEr = "Invalid fields for message.";
                logger.logError(messageEr);
                throw new ValidationException(messageEr);
            }
            DataMapperFactoryMethod.GetCurrentFactory().MessageFactory.UpdateIncludedMessages(message);
        }
    }
}
