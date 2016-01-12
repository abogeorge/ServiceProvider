using DataMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Implementation
{
    public class EFDataMapperFactory : IDataMapperFactory
    {
        public ICurrencyFactory CurrencyFactory
        {
            get
            {
                return new EFCurrencyFactory();
            }
        }

        public ICurrencyRateFactory CurrencyRateFactory
        {
            get
            {
                return new EFCurrencyRateFactory();
            }
        }

        public ICustomerFactory CustomerFactory
        {
            get
            {
                return new EFCustomerFactory();
            }
        }

        public IMessageTypeFactory MessageTypeFactory
        {
            get
            {
                return new EFMessageTypeFactory();
            }
        }

        public IMinuteTypeFactory MinuteTypeFactory
        {
            get
            {
                return new EFMinuteTypeFactory();
            }
        }

        public ISubscriptionTypeFactory SubscriptionTypeFactory
        {
            get
            {
                return new EFSubscriptionTypeFactory();
            }
        }

    }
}
