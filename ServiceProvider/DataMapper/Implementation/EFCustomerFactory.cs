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

        public Customer GetCustomerByCNP(string cnp)
        {
            using (var context = new DataMapperContext())
            {
                var customerVar = (from customer in context.Customers
                                   where customer.CNP.Equals(cnp)
                                   select customer).FirstOrDefault();
                return customerVar;
            }
        }
    }
}
