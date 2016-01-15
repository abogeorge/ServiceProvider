using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using ServiceLayer.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DataMapper.Exceptions;
using DataMapper;

namespace ServiceLayer.ServicesImplementation
{
    public class ContractService : IContractService
    {

        private ServiceProviderLogger logger;

        public ContractService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void AddContract(Contract contract, Customer customer, Subscription subscription)
        {
            logger.logInfo("Attempting to add a new contract ... ");

            Contract oldContract = GetContractByName(contract.ContractName);
            if (oldContract != null)
            {
                String message = "Contract already exists.";
                logger.logError(message);
                throw new DuplicateException(message);
            }

            // Deduct age
            if (DeductCustomerAge(customer.CNP) < 18)
            {
                throw new ValidationException("Customer must be at least 18 yo!");
            }

             // Price if is missing
            if (contract.Price == 0)
            {
                contract.Price = subscription.Price; 
            }
            
            var validationResult = Validation.Validate<Contract>(contract);
            if (!validationResult.IsValid || !contract.CheckDateValidity())
            {
                String message = "Invalid fields for Contract.";
                logger.logError(message);
                throw new ValidationException(message);
            }

            DataMapperFactoryMethod.GetCurrentFactory().ContractFactory.AddContract(contract, customer, subscription);
            logger.logInfo("Add contract operation ended.");
        }

        public void DropContract(string contractName, Customer customer, Subscription subscription)
        {
            logger.logInfo("Attempting to drop contract ...");
            if (contractName.Equals(""))
            {
                String message = "Name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            Contract contract = GetContractByName(contractName);
            if (contract == null)
            {
                String message = "The contract does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().ContractFactory.DropContractByName(contract, customer, subscription);
            logger.logInfo("Drop contract operation ended.");
        }

        public Contract GetContractByName(string contractName)
        {
            logger.logInfo("Attempting to retrieve contract by name ...");
            if (contractName.Equals(""))
            {
                String message = "Contract name field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve contract operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().ContractFactory.GetContractByName(contractName);
        }

        public void UpdateContractEndDate(string contractName, DateTime endDate)
        {
            logger.logInfo("Attempting to edit contract end date ... ");
            Contract contract = GetContractByName(contractName);
            if (contract == null)
            {
                String message = "The contract does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            contract.EndDate = endDate;
            var validationResult = Validation.Validate<Contract>(contract);
            if (!validationResult.IsValid || !contract.CheckDateValidity())
            {
                String message = "Invalid fields for contract";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().ContractFactory.UpdateContractEndDate(contract);
            logger.logInfo("Edit contract end date operation ended.");
        }

        public void UpdateContractPrice(string contractName, double price)
        {
            logger.logInfo("Attempting to edit contract price ... ");
            Contract contract = GetContractByName(contractName);
            if (contract == null)
            {
                String message = "The contract does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            contract.Price = price;
            var validationResult = Validation.Validate<Contract>(contract);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for contract";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().ContractFactory.UpdateContractPrice(contract);
            logger.logInfo("Edit contract price operation ended.");
        }

        private int DeductCustomerAge(string CNP)
        {
            String year = "";
            switch(CNP[0].ToString())
            {
                case "1": year += "19"; break;
                case "2": year += "19"; break;
                case "3": year += "18"; break;
                case "4": year += "18"; break;
                case "5": year += "20"; break;
                case "6": year += "20"; break;
                default: throw new ValidationException("Invalid CNP!");
            }
            year += CNP[1].ToString() + CNP[2].ToString();
            String month = CNP[3].ToString() + CNP[4].ToString();
            String day = CNP[5].ToString() + CNP[6].ToString();
            try
            {
                DateTime birthDate = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(month));
                TimeSpan timePassed = DateTime.Now.Subtract(birthDate);
                return ((int)timePassed.TotalDays / 365);
            }
            catch(Exception)
            {
                throw new ValidationException("Invalid CNP!");
            }
        }

    }
}
