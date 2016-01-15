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

        public void DropContractByName(Contract contract, Customer customer, Subscription subscription)
        {
            using (var context = new DataMapperContext())
            {
                var sub = context.Subscriptions.Find(subscription.SubscriptionId);
                var cus = context.Customers.Find(customer.CustomerId);
                var con = context.Contracts.Find(contract.ContractId);
                context.Entry(sub).Collection("Contracts").Load();
                context.Entry(cus).Collection("Contracts").Load();
                sub.Contracts.Remove(con);
                cus.Contracts.Remove(con);
                context.Contracts.Attach(con);
                context.Contracts.Remove(con);
                context.SaveChanges();

            }
        }

        public Contract GetContractByName(string contractName)
        {
            using (var context = new DataMapperContext())
            {
                var subVar = (from contract in context.Contracts
                              where contract.ContractName.Equals(contractName)
                              select contract).FirstOrDefault();
                return subVar;
            }
        }

        public void UpdateContractEndDate(Contract contract)
        {
            using (var context = new DataMapperContext())
            {
                context.Contracts.Attach(contract);
                var entry = context.Entry(contract);
                entry.Property(u => u.EndDate).IsModified = true;
                context.SaveChanges();
            }
        }

        public void UpdateContractPrice(Contract contract)
        {
            using (var context = new DataMapperContext())
            {
                context.Contracts.Attach(contract);
                var entry = context.Entry(contract);
                entry.Property(u => u.Price).IsModified = true;
                context.SaveChanges();
            }
        }
    }
}
