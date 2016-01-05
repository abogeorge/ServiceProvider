using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DomainModel;
using DataMapper.Exceptions;

namespace ServiceProviderUnitTests
{
    [TestClass]
    public class CustomerTest
    {

        private ICustomerService customerService;

        public CustomerTest()
        {
            customerService = new CustomerService();
        }

        // Add a null customer
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddCustomerNull()
        {
            customerService.AddCustomer(null);
        }

        // Add customer with 1 character first name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWith1CharcterFirstName()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "G",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "2830911278945"
            });
        }

        // Add customer with 31 character first name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWith31CharcterFirstName()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "abcdefghiklmnopqrstuvwxzyabcdef",
                LastName = "Validln",
                Adress = "Valid Adress",
                CNP = "4201126460362",
            });
        }

        // Add customer with not null fields 
        // (reason: phone and email are using regex but they are not mandatory)
        [TestMethod]
        public void TestAddCustomerWithRequiredFieldsOnly()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "3711014035041"
            });
        }

        // Add customer without all not null fields 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWithoutAllRequiredFields()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
            });
        }

        // Add customer with correct email adress 
        [TestMethod]
        public void TestAddCustomerWithEmailAdress()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "3840323235114",
                Email = "client.valid@provider.ro"
            });
        }

        // Add customer with wrong email adress 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWithBadEmailAdress()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "2930416139312",
                Email = "client.invalid.ro"
            });
        }

        // Add customer with wrong email adress 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWithBadEmailAdress2()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "6070507294972",
                Email = "yainvalid.client@"
            });
        }

        // Add customer with correct phone number without any special chars
        [TestMethod]
        public void TestAddCustomerWithPhoneWOSpecialChars()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "1430726331029",
                Email = "client.valid@provider.com",
                Phone = "0749782465"
            });
        }

        // Add customer with correct phone number with "-"
        [TestMethod]
        public void TestAddCustomerWithPhoneWithSpecialChars()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "2180407515439",
                Email = "client.valid@provider.com",
                Phone = "0268-782-465"
            });
        }

        // Add customer with correct phone number with "."
        [TestMethod]
        public void TestAddCustomerWithPhoneWithMoreSpecialChars()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "1321211013584",
                Email = "client.valid@provider.com",
                Phone = "0268.782.465"
            });
        }

        // Add customer with wrong phone 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWithShortPhone()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "3451006257438",
                Email = "client.valid@provider.com",
                Phone = "0268999"
            });
        }

        // Add customer with wrong phone 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWithWrongPhone()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                CNP = "4860314305307",
                Email = "client.valid@provider.com",
                Phone = "0268.1a1.222"
            });
        }

        // TODO: Add CNP "extensive" tests

        // Add a complete customer
        [TestMethod]
        public void TestAddCustomer()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "George",
                LastName = "Abordioaie",
                Adress = "2 Codlea",
                CNP = "1920417081662",
                Email = "george@yahoo.com",
                Phone = "0749782465"
            });
        }
    }
}
