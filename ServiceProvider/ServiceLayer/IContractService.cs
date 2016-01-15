using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IContractService
    {
        void AddContract(Contract contract, Customer customer, Subscription subscription);
        Contract GetContractByName(String contractName);
        void UpdateContractPrice(String contractName, Double price);
        void UpdateContractEndDate(String contractName, DateTime endDate);
        void DropContract(String contractName, Customer customer, Subscription subscription);
    }
}
