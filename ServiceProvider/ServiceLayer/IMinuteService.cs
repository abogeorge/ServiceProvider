using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IMinuteService
    {
        void AddMinute(Minute minute);
        Minute GetMinuteById(int id);
        void DropMinute(int id);
        void UpdateIncludedMinutes(Minute minute);
        void UpdateExtraCharge(Minute minute);
    }
}
