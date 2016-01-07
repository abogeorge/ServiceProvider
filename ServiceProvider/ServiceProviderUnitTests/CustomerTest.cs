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

        // Add customer with short cnp 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWithShortCNP()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                Email = "client.valid@provider.com",
                Phone = "0268.111.222",
                CNP = "192041708166"
            });
        }

        // Add customer with wrong cnp 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCustomerWithWrongCNP()
        {
            customerService.AddCustomer(new Customer
            {
                FirstName = "Validfn",
                LastName = "Validln",
                Adress = "ValidAdress",
                Email = "client.valid@provider.com",
                Phone = "0268.111.222",
                CNP = "1920417081661"
            });
        }

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

        // Check CNP unicity
        [TestMethod]
        [ExpectedException(typeof(DuplicateException))]
        public void TestAddCustomerWithDuplicateCNP()
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

        // Retrieve customer by cnp
        [TestMethod]
        public void TestGetUserByCNP()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.IsNotNull(customer);
        }

        // Retrieve customer with empty cnp
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetUserByCNPEmptyString()
        {
            String cnp = "";
            Customer customer = customerService.GetCustomerByCNP(cnp);
        }

        // Retrieve customer with inexistent cnp
        [TestMethod]
        public void TestGetUserByWrongCNP()
        {
            String cnp = "1234567890123";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.IsNull(customer);
        }

        // Update last name to valid customer
        [TestMethod]
        public void TestUpdateLastName()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.LastName, "Abordioaie");
            customerService.UpdateLastName(cnp, "Popescu");
            Customer customerUpdated = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customerUpdated.LastName, "Popescu");
        }

        // Update last name invalid last name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateInvalidLastName()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.LastName, "Popescu");
            customerService.UpdateLastName(cnp, "P");
        }

        // Update last name invalid cnp
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateLastNameInvalidCNP()
        {
            String cnp = "1234567890123";
            customerService.UpdateLastName(cnp, "Popel");
        }

        // Update first name to valid customer
        [TestMethod]
        public void TestUpdateFirstName()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.FirstName, "George");
            customerService.UpdateFirstName(cnp, "Alexandru");
            Customer customerUpdated = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customerUpdated.FirstName, "Alexandru");
        }

        // Update last name invalid first name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateInvalidFirstName()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.FirstName, "Alexandru");
            customerService.UpdateFirstName(cnp, "A");
        }

        // Update first name invalid cnp
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateFirstNameInvalidCNP()
        {
            String cnp = "1234567890123";
            customerService.UpdateFirstName(cnp, "Aa");
        }

        // Update adress to valid customer
        [TestMethod]
        public void TestUpdateAdress()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.Adress, "2 Codlea");
            customerService.UpdateAdress(cnp, "3 Brasov");
            Customer customerUpdated = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customerUpdated.Adress, "3 Brasov");
        }

        // Update invalid adress
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateInvalidAdress()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.Adress, "3 Brasov");
            customerService.UpdateAdress(cnp, "ABC");
        }

        // Update email to valid customer
        [TestMethod]
        public void TestUpdateEmail()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.Email, "george@yahoo.com");
            customerService.UpdateEmail(cnp, "alexandru@yahoo.com");
            Customer customerUpdated = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customerUpdated.Email, "alexandru@yahoo.com");
        }

        // Update invalid email
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateInvalidEmail()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.Email, "alexandru@yahoo.com");
            customerService.UpdateEmail(cnp, "george@");
        }

        // Update phone to valid customer
        [TestMethod]
        public void TestUpdatePhone()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.Phone, "0749782465");
            customerService.UpdatePhone(cnp, "0268.455.455");
            Customer customerUpdated = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customerUpdated.Phone, "0268.455.455");
        }

        // Update invalid phone
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateInvalidPhone()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.AreEqual(customer.Phone, "0268.455.455");
            customerService.UpdatePhone(cnp, "0777");
        }

        // -------------- DROPS

        // Drop Customer
        [TestMethod]
        public void TestDropCustomer()
        {
            String cnp = "1920417081662";
            Customer customer = customerService.GetCustomerByCNP(cnp);
            Assert.IsNotNull(customer);
            customerService.DropCustomerByCNP(cnp);
            Customer customerRemoved = customerService.GetCustomerByCNP(cnp);
            Assert.IsNull(customerRemoved);
        }

        // Drop Customer without CNP
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropCustomerWithEmptyString()
        {
            String cnp = "";
            customerService.DropCustomerByCNP(cnp);
        }

        // Drop Customer with invalid CNP
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropCustomerWithInvalidCNP()
        {
            String cnp = "1234567890123";
            customerService.DropCustomerByCNP(cnp);
        }

    }
}
