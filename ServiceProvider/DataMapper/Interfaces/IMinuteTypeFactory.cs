using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IMinuteTypeFactory
    {
        void AddMinuteType(MinuteType minuteType);
        MinuteType GetMinuteTypeByName(String minuteTypeName);
        void DropMinuteType(String minuteTypeName);
    }
}
