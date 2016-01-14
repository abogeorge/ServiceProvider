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

        public IMessageFactory MessageFactory
        {
            get
            {
                return new EFMessageFactory();
            }
        }

        public IMessageTypeFactory MessageTypeFactory
        {
            get
            {
                return new EFMessageTypeFactory();
            }
        }

        public IMinuteFactory MinuteFactory
        {
            get
            {
                return new EFMinuteFactory();
            }
        }

        public IMinuteTypeFactory MinuteTypeFactory
        {
            get
            {
                return new EFMinuteTypeFactory();
            }
        }

        public ISubscriptionFactory SubscriptionFactory
        {
            get
            {
                return new EFSubscriptionFactory();
            }
        }

        public ISubscriptionTypeFactory SubscriptionTypeFactory
        {
            get
            {
                return new EFSubscriptionTypeFactory();
            }
        }

        public IContractFactory ContractFactory
        {
            get
            {
                return new EFContractFactory();
            }
        }


    }
}
