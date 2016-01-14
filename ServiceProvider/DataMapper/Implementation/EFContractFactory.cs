using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DataMapper.Implementation
{
    class EFContractFactory : IContractFactory
    {
        public void AddContract(Contract contract, Customer customer, Subscription subscription)
        {
            using (var context = new DataMapperContext())
            {
                context.Subscriptions.Attach(subscription);
                context.Customers.Attach(customer);
                context.Contracts.Attach(contract);
                context.Entry(subscription).Collection(c => c.Contracts).Load();
                context.Entry(customer).Collection(c => c.Contracts).Load();
                context.Contracts.Add(contract);
                context.SaveChanges();
            }
        }
    }
}
