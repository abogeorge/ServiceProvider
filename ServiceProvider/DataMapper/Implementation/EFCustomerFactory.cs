using DataMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DataMapper.Implementation
{
    class EFCustomerFactory : ICustomerFactory
    {
        public void AddCustomer(Customer customer)
        {
            using (var context = new DataMapperContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }
    }
}
