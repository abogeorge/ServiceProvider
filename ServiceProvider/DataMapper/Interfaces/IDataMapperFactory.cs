using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IDataMapperFactory
    {
        ICustomerFactory CustomerFactory
        {
            get;
        }

        ISubscriptionTypeFactory SubscriptionTypeFactory
        {
            get;
        }

        IMinuteTypeFactory MinuteTypeFactory
        {
            get;
        }

        IMessageTypeFactory MessageTypeFactory
        {
            get;
        }

        ICurrencyFactory CurrencyFactory
        {
            get;
        }

        ICurrencyRateFactory CurrencyRateFactory
        {
            get;
        }

        IMinuteFactory MinuteFactory
        {
            get;
        }

        IMessageFactory MessageFactory
        {
            get;
        }

        ISubscriptionFactory SubscriptionFactory
        {
            get;
        }

    }
}
