
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IMessageTypeFactory
    {
        void AddMessageType(MessageType messageType);
        MessageType GetMessageTypeByName(String messageTypeName);
        void DropMessageType(String messageTypeName);
    }
}
