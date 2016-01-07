using DataMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DataMapper.Exceptions;

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

        public void DropCustomerByCNP(string cnp)
        {
            Customer customer = GetCustomerByCNP(cnp);
            if(customer == null)
            {
                throw new EntityDoesNotExistException("Invalid CNP.");
            }
            
            using (var context = new DataMapperContext())
            {
                context.Customers.Attach(customer);
                context.Customers.Remove(customer);
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

        public void UpdateLastName(Customer customer)
        {
                using (var context = new DataMapperContext())
                {
                    context.Customers.Attach(customer);
                    var entry = context.Entry(customer);
                    entry.Property(u => u.LastName).IsModified = true;
                    context.SaveChanges();
                }
        }

        public void UpdateFirstName(Customer customer)
        {
            using (var context = new DataMapperContext())
            {
                context.Customers.Attach(customer);
                var entry = context.Entry(customer);
                entry.Property(u => u.FirstName).IsModified = true;
                context.SaveChanges();
            }
        }

        public void UpdateAdress(Customer customer)
        {
            using (var context = new DataMapperContext())
            {
                context.Customers.Attach(customer);
                var entry = context.Entry(customer);
                entry.Property(u => u.Adress).IsModified = true;
                context.SaveChanges();
            }
        }

        public void UpdateEmail(Customer customer)
        {
            using (var context = new DataMapperContext())
            {
                context.Customers.Attach(customer);
                var entry = context.Entry(customer);
                entry.Property(u => u.Email).IsModified = true;
                context.SaveChanges();
            }
        }

        public void UpdatePhone(Customer customer)
        {
            using (var context = new DataMapperContext())
            {
                context.Customers.Attach(customer);
                var entry = context.Entry(customer);
                entry.Property(u => u.Phone).IsModified = true;
                context.SaveChanges();
            }
        }

    }
}
