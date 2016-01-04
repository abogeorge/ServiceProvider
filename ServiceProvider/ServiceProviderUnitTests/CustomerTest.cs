using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DomainModel;

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

        [TestMethod]
        public void TestMethod1()
        {
            customerService.AddCustomer(new Customer { FirstName = "George", LastName = "Abordioaie", Adress = "2 Codlea", CNP = "1920417081662", Email = "george@yahoo.com", Phone = "0749782465" });
        }
    }
}
