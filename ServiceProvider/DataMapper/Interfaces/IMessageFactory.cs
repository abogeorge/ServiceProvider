using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IMessageFactory
    {
        void AddMessage(Message message, MessageType messageType);
        Message GetMessageById(int id, MessageType messageType);
        void DropMessage(int id, MessageType messageType);
        void UpdateIncludedMessages(Message message);
        void UpdateExtraCharge(Message message);
    }
}
