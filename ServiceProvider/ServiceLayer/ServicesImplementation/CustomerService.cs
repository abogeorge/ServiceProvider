using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DataMapper.Exceptions;
using DataMapper.Interfaces;
using DataMapper;
using ServiceLayer.Common;

namespace ServiceLayer.ServicesImplementation
{
    public class CustomerService : ICustomerService
    {
        //private ServiceProviderLogger logger;

        public CustomerService()
        {
            //logger = ServiceProviderLogger.GetInstance();
        }

        public Customer GetCustomerByCNP(string cnp)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.GetCustomerByCNP(cnp);
        }

        bool ICustomerService.AddCustomer(Customer customer)
        {
           // logger.logInfo("Attempting to add a new customer ... ");

            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                throw new ValidationException(message);
            }

            Customer oldCustomer = GetCustomerByCNP(customer.CNP);
            if(oldCustomer != null)
            {
                throw new DuplicateException("Customer " + customer.FirstName + " " + customer.LastName + " is already registered.");
            }

            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.AddCustomer(customer);
            //logger.logInfo("Customer " + customer.FirstName + " " + customer.LastName + " was added!");
            return true;
        }
    }
}
