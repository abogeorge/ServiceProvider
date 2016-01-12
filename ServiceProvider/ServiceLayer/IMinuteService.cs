﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IMinuteService
    {
        void AddMinute(Minute minute, MinuteType minuteType);
        Minute GetMinuteById(int id, MinuteType minuteType);
        void DropMinute(int id, MinuteType minuteType);
        void UpdateIncludedMinutes(Minute minute);
        void UpdateExtraCharge(Minute minute);
    }
}