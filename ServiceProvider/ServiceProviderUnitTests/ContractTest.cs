using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DomainModel;
using DataMapper.Exceptions;

namespace ServiceProviderUnitTests
{
    [TestClass]
    public class ContractTest
    {

        private IContractService contractService;
        private ICustomerService customerService;
        private ISubscriptionService subscriptionService;

        public ContractTest()
        {
            this.contractService = new ContractService();
            this.customerService = new CustomerService();
            this.subscriptionService = new SubscriptionSerivce();
        }

        [TestMethod]
        public void TestAddGetCustomer()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            if (customer == null)
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

        [TestMethod]
        public void TestAddGetSubscription()
        {
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            Assert.IsNotNull(subscription);
        }

        // Add a null contract
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddNullContract()
        {
            contractService.AddContract(null, null, null);
        }

        // Add contract
        [TestMethod]
        public void TestAddContract()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            DateTime dateEnd = new DateTime(2016, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName="Contract Abo 1", StartDate = date, EndDate = dateEnd, Price = 100, Subscription = subscription }, customer, subscription);
        }

        // Add contract with short name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddContractShortName()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "C", StartDate = date, EndDate = DateTime.Now, Price = 100, Subscription = subscription }, customer, subscription);
        }

        // Add contract with long name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddContractLongName()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "abcdefghiklmnopqrstuvwxzyabcdef", StartDate = date, EndDate = DateTime.Now, Price = 100, Subscription = subscription }, customer, subscription);
        }

        // Add contract with three char name
        [TestMethod]
        public void TestAddContractExactMinName()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "abo", StartDate = date, EndDate = DateTime.Now, Price = 100, Subscription = subscription }, customer, subscription);
        }

        // Add contract with negative price
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddContractNegativePrice()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "Abonamet Test", StartDate = date, EndDate = DateTime.Now, Price = -1, Subscription = subscription }, customer, subscription);
        }

        // Add contract with over max price
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddContractGTMaxPrice()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "Abonamet Test", StartDate = date, EndDate = DateTime.Now, Price = 1001, Subscription = subscription }, customer, subscription);
        }

        // Add contract with double price
        [TestMethod]
        public void TestAddContractDoublePrice()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "Abonamet Test", StartDate = date, EndDate = DateTime.Now, Price = 20.5, Subscription = subscription }, customer, subscription);
        }

        // Add contract with missning price
        [TestMethod]
        public void TestAddContractPriceMissing()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "Abonamet Test 2", StartDate = date, EndDate = DateTime.Now, Price = 0, Subscription = subscription }, customer, subscription);
        }

        // Add contract with start date > end date
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddContractStartdDateGTEndDate()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime startDate = new DateTime(2017, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "Abonamet Test 3", StartDate = startDate, EndDate = DateTime.Now, Price = 0, Subscription = subscription }, customer, subscription);
        }

        // Add contract duplicate
        [TestMethod]
        [ExpectedException(typeof(DuplicateException))]
        public void TestAddContractDuplicate()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            DateTime date = new DateTime(2015, 12, 30);
            contractService.AddContract(new Contract { Customer = customer, ContractName = "Contract Abo 1", StartDate = date, EndDate = DateTime.Now, Price = 100, Subscription = subscription }, customer, subscription);
        }

        // Get contract
        [TestMethod]
        public void TestGetContract()
        {
            String conName = "Contract Abo 1";
            Contract contract = contractService.GetContractByName(conName);
            Assert.AreEqual(contract.ContractName, conName);
        }

        // Get contract with null name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetContractInvalid()
        {
            String conName = "";
            Contract contract = contractService.GetContractByName(conName);
        }

        // Get contract inexistent
        [TestMethod]
        public void TestGetContractInexistent()
        {
            String conName = "WrongCon";
            Contract contract = contractService.GetContractByName(conName);
            Assert.IsNull(contract);
        }

        // Update contract price
        [TestMethod]
        public void TestUpdateContractPrice()
        {
            String conName = "Contract Abo 1";
            Double oldPrice = 100;
            Double newPrice = 50;
            Contract contract = contractService.GetContractByName(conName);
            Assert.AreEqual(contract.Price, oldPrice);
            contractService.UpdateContractPrice(conName, newPrice);
            Contract newContract = contractService.GetContractByName(conName);
            Assert.AreEqual(newContract.Price, newPrice);
        }

        // Update contract price wrong contract
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateContractPriceInexistent()
        {
            String conName = "Wrong";
            contractService.UpdateContractPrice(conName, 30);
        }

        // Update contract price invalid
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateContractPriceInvalid()
        {
            String conName = "";
            contractService.UpdateContractPrice(conName, 30);
        }

        // Update contract price below zero
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateContractPriceLTZero()
        {
            String conName = "Contract Abo 1";
            contractService.UpdateContractPrice(conName, -2);
        }

        // Update contract price over max
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateContractPriceGTMax()
        {
            String conName = "Contract Abo 1";
            contractService.UpdateContractPrice(conName, 1002);
        }

        // Update contract end date
        [TestMethod]
        public void TestUpdateContractEndDate()
        {
            String conName = "Contract Abo 1";
            DateTime oldEndDate = new DateTime(2016, 12, 30);
            DateTime newEndDate = new DateTime(2018, 12, 30);
            Contract contract = contractService.GetContractByName(conName);
            Assert.AreEqual(contract.EndDate, oldEndDate);
            contractService.UpdateContractEndDate(conName, newEndDate);
            Contract newContract = contractService.GetContractByName(conName);
            Assert.AreEqual(newContract.EndDate, newEndDate);
        }

        // Update contract end date wrong contract
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateContractEndDateInexistent()
        {
            String conName = "Wrong";
            DateTime newEndDate = new DateTime(2018, 12, 30);
            contractService.UpdateContractEndDate(conName, newEndDate);
        }

        // Update contract end date invalid
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateContractEndDateInvalid()
        {
            String conName = "";
            DateTime newEndDate = new DateTime(2018, 12, 30);
            contractService.UpdateContractEndDate(conName, newEndDate);
        }

        // Update contract end date < start date
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateContractEndDateLTStartDate()
        {
            String conName = "Contract Abo 1";
            DateTime oldEndDate = new DateTime(2018, 12, 30);
            DateTime newEndDate = new DateTime(2012, 12, 30);
            Contract contract = contractService.GetContractByName(conName);
            Assert.AreEqual(contract.EndDate, oldEndDate);
            contractService.UpdateContractEndDate(conName, newEndDate);
        }

        // DROPS ...............
         
        // Drop contract null name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropContractNullName()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            String conName = "";
            contractService.DropContract(conName, customer, subscription);
        }

        // Drop contract inexstent name
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropContractInexistent()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            String conName = "Wrong";
            contractService.DropContract(conName, customer, subscription);
        }

        // Drop contract
        [TestMethod]
        public void TestDropContract()
        {
            Customer customer = customerService.GetCustomerByCNP("1920417081662");
            Subscription subscription = subscriptionService.GetSubscriptionByName("Abonament 10");
            String conName = "abo";
            Contract contract = contractService.GetContractByName(conName);
            Assert.IsNotNull(contract);
            contractService.DropContract(conName, customer, subscription);
            Contract delContract = contractService.GetContractByName(conName);
            Assert.IsNull(delContract);
        }

    }
}
