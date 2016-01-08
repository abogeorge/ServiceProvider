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
        private ServiceProviderLogger logger;

        public CustomerService()
        {
            logger = ServiceProviderLogger.GetInstance();
        }

        public void DropCustomerByCNP(String cnp)
        {
            logger.logInfo("Attempting to drop customer ...");
            if (cnp.Equals(""))
            {
                String message = "CNP field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.DropCustomerByCNP(cnp);
            logger.logInfo("Drop customer operation ended.");
        }

        public Customer GetCustomerByCNP(String cnp)
        {
            logger.logInfo("Attempting to retrieve customer by CNP ...");
            if (cnp.Equals(""))
            {
                String message = "CNP field is null!";
                logger.logError(message);
                throw new ValidationException(message);
            }
            logger.logInfo("Retrieve customer operation ended.");
            return DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.GetCustomerByCNP(cnp);
        }

        public void UpdateLastName(String cnp, string lastName)
        {
            logger.logInfo("Attempting to edit customer last name ... ");
            Customer customer = GetCustomerByCNP(cnp);
            if(customer == null)
            {
                String message = "The customer with CNP " + cnp + " does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            customer.LastName = lastName;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdateLastName(customer);
            logger.logInfo("Edit customer last name operation ended.");
        }

        public void UpdateFirstName(String cnp, string firstName)
        {
            logger.logInfo("Attempting to edit customer first name ... ");
            Customer customer = GetCustomerByCNP(cnp);
            if (customer == null)
            {
                String message = "The customer with CNP " + cnp + " does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            customer.FirstName = firstName;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdateFirstName(customer);
            logger.logInfo("Edit customer first name operation ended.");
        }

        public void UpdateAdress(String cnp, string adress)
        {
            logger.logInfo("Attempting to edit customer adress ... ");
            Customer customer = GetCustomerByCNP(cnp);
            if (customer == null)
            {
                String message = "The customer with CNP " + cnp + " does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            customer.Adress = adress;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdateAdress(customer);
            logger.logInfo("Edit customer adress operation ended.");
        }

        public void UpdateEmail(String cnp, string email)
        {
            logger.logInfo("Attempting to edit customer email ... ");
            Customer customer = GetCustomerByCNP(cnp);
            if (customer == null)
            {
                String message = "The customer with CNP " + cnp + " does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            customer.Email = email;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdateEmail(customer);
            logger.logInfo("Edit customer email operation ended.");
        }

        public void UpdatePhone(String cnp, string phone)
        {
            logger.logInfo("Attempting to edit customer phone ... ");
            Customer customer = GetCustomerByCNP(cnp);
            if (customer == null)
            {
                String message = "The customer with CNP " + cnp + " does not exist.";
                logger.logError(message);
                throw new EntityDoesNotExistException(message);
            }
            customer.Phone = phone;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                logger.logError(message);
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdatePhone(customer);
            logger.logInfo("Edit customer email operation ended.");
        }

        void ICustomerService.AddCustomer(Customer customer)
        {
            logger.logInfo("Attempting to add a new customer ... ");

            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                logger.logError(message);
                throw new ValidationException(message);
            }

            Customer oldCustomer = GetCustomerByCNP(customer.CNP);
            if(oldCustomer != null)
            {
                String message = "Customer " + customer.FirstName + " " + customer.LastName + " is already registered.";
                logger.logError(message);
                throw new DuplicateException(message);
            }

            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.AddCustomer(customer);
            logger.logInfo("Add customer operation ended.");
        }
    }
}
