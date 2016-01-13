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
    public class EFMessageFactory : IMessageFactory
    {
        public void AddMessage(Message message, MessageType messageType)
        {
            using (var context = new DataMapperContext())
            {
                context.MessageTypes.Attach(messageType);
                context.Messages.Attach(message);
                context.Entry(messageType).Collection(c => c.Messages).Load();
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }

        public void DropMessage(int id, MessageType messageType)
        {
            Message message = GetMessageById(id, messageType);
            if (message == null)
            {
                throw new EntityDoesNotExistException("Invalid ID.");
            }

            using (var context = new DataMapperContext())
            {
                var mesType = context.MessageTypes.Find(messageType.MessageTypeId);
                var mes = context.Messages.Find(message.MessageId);
                context.Entry(mesType).Collection("Messages").Load();
                mesType.Messages.Remove(mes);
                context.Messages.Attach(mes);
                context.Messages.Remove(mes);
                context.SaveChanges();
            }
        }

        public Message GetMessageById(int id, MessageType messageType)
        {
            using (var context = new DataMapperContext())
            {
                var mesVar = (from message in context.Messages
                                   where message.MessageId == id &&
                                   message.MessageType.MessageTypeId == messageType.MessageTypeId
                                   select message).FirstOrDefault();
                return mesVar;
            }
        }

        public void UpdateExtraCharge(Message message)
        {
            using (var context = new DataMapperContext())
            {
                context.Messages.Attach(message);
                var entry = context.Entry(message);
                entry.Property(m => m.ExtraCharge).IsModified = true;
                context.SaveChanges();
            }
        }

        public void UpdateIncludedMessages(Message message)
        {
            using (var context = new DataMapperContext())
            {
                context.Messages.Attach(message);
                var entry = context.Entry(message);
                entry.Property(m => m.IncludedMessages).IsModified = true;
                context.SaveChanges();
            }
        }
    }
}
