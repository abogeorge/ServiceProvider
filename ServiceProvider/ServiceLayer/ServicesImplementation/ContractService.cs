using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using ServiceLayer.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DataMapper.Exceptions;

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

            //Contract oldContract = GetContract(contract.ContractName);
            //if (oldContract != null)
            //{
            //    String message = "Contract already exists.";
            //    logger.logError(message);
            //    throw new DuplicateException(message);
            //}

            var validationResult = Validation.Validate<Contract>(contract);
            if (!validationResult.IsValid)
            {
                String message = "Invalid fields for Contract.";
                logger.logError(message);
                throw new ValidationException(message);
            }

            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().ContractFactory.AddContract(contract, customer, subscription);
            logger.logInfo("Add message operation ended.");
        }
    }
}
