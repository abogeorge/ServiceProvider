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

        public void DropCustomerByCNP(String cnp)
        {
            if (cnp.Equals(""))
            {
                throw new ValidationException("CNP field is null!");
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.DropCustomerByCNP(cnp);
        }

        public Customer GetCustomerByCNP(String cnp)
        {
            if (cnp.Equals(""))
            {
                throw new ValidationException("CNP field is null!");
            }
            return DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.GetCustomerByCNP(cnp);
        }

        public void UpdateLastName(String cnp, string lastName)
        {
            Customer customer = GetCustomerByCNP(cnp);
            if(customer == null)
            {
                throw new EntityDoesNotExistException("The customer with CNP " + cnp + " does not exist.");
            }
            customer.LastName = lastName;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdateLastName(customer);
        }

        public void UpdateFirstName(String cnp, string firstName)
        {
            Customer customer = GetCustomerByCNP(cnp);
            if (customer == null)
            {
                throw new EntityDoesNotExistException("The customer with CNP " + cnp + " does not exist.");
            }
            customer.FirstName = firstName;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdateFirstName(customer);
        }

        public void UpdateAdress(String cnp, string adress)
        {
            Customer customer = GetCustomerByCNP(cnp);
            if (customer == null)
            {
                throw new EntityDoesNotExistException("The customer with CNP " + cnp + " does not exist.");
            }
            customer.Adress = adress;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdateAdress(customer);
        }

        public void UpdateEmail(String cnp, string email)
        {
            Customer customer = GetCustomerByCNP(cnp);
            if (customer == null)
            {
                throw new EntityDoesNotExistException("The customer with CNP " + cnp + " does not exist.");
            }
            customer.Email = email;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdateEmail(customer);
        }

        public void UpdatePhone(String cnp, string phone)
        {
            Customer customer = GetCustomerByCNP(cnp);
            if (customer == null)
            {
                throw new EntityDoesNotExistException("The customer with CNP " + cnp + " does not exist.");
            }
            customer.Phone = phone;
            var validationResult = Validation.Validate<Customer>(customer);
            if (!validationResult.IsValid || !customer.ValidateCNP())
            {
                String message = "Invalid fields for customer " + customer.FirstName + " " + customer.LastName;
                throw new ValidationException(message);
            }
            DataMapperFactoryMethod.GetCurrentFactory().CustomerFactory.UpdatePhone(customer);
        }

        void ICustomerService.AddCustomer(Customer customer)
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
        }
    }
}
