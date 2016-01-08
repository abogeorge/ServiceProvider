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
        public ICustomerFactory CustomerFactory
        {
            get
            {
                return new EFCustomerFactory();
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
