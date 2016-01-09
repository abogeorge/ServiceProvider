using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IMessageTypeService
    {
        void AddMessageType(MessageType messageType);
        MessageType GetMessageTypeByName(String messageTypeName);
        void DropMessageType(String messageTypeName);
    }
}
