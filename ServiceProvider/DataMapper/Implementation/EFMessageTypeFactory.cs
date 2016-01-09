using DataMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DataMapper.Exceptions;

namespace DataMapper.Implementation
{
    class EFMessageTypeFactory : IMessageTypeFactory
    {
        public void AddMessageType(MessageType messageType)
        {
            using (var context = new DataMapperContext())
            {
                context.MessageTypes.Add(messageType);
                context.SaveChanges();
            }
        }

        public void DropMessageType(string messageTypeName)
        {
            MessageType mesType = GetMessageTypeByName(messageTypeName);
            if (mesType == null)
            {
                throw new EntityDoesNotExistException("Invalid Message Type Name.");
            }

            using (var context = new DataMapperContext())
            {
                context.MessageTypes.Attach(mesType);
                context.MessageTypes.Remove(mesType);
                context.SaveChanges();
            }
        }

        public MessageType GetMessageTypeByName(string messageTypeName)
        {
            using (var context = new DataMapperContext())
            {
                var mesTypeVar = (from mesType in context.MessageTypes
                                  where mesType.MessageTypeName.Equals(messageTypeName)
                                  select mesType).FirstOrDefault();
                return mesTypeVar;
            }
        }
    }
}
