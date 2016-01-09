using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DataMapper.Exceptions;
using DomainModel;

namespace ServiceProviderUnitTests
{

    [TestClass]
    public class SubscriptionTypeTest
    {

        private ISubscriptionTypeService subscriptionTypeService;

        public SubscriptionTypeTest()
        {
            subscriptionTypeService = new SubscriptionTypeService();
        }

        // Add a null subscription type
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddNullSubscriptyionType()
        {
            subscriptionTypeService.AddSubscriptionType(null);
        }

        // Add subscription type with 1 character name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddSubscriptionTypeWithOneCharName()
        {
            subscriptionTypeService.AddSubscriptionType(
                new SubscriptionType { SubscriptionTypeName = "A" });
        }

        // Add subscription type with 31 character name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddSubscriptionTypeWithOverMaxCharName()
        {
            subscriptionTypeService.AddSubscriptionType(
                new SubscriptionType { SubscriptionTypeName = "abcdefghiklmnopqrstuvwxzyabcdef" });
        }

        // Add subscription type 
        [TestMethod]
        public void TestAddSubscriptionType()
        {
            subscriptionTypeService.AddSubscriptionType(
                new SubscriptionType { SubscriptionTypeName = "Mobil" });
        }

        // Retrieve subscription type
        [TestMethod]
        public void TestGetSubscriptionTypeByName()
        {
            String subTypeName = "Mobil";
            SubscriptionType subType = subscriptionTypeService.GetSubscriptionTypeByName(subTypeName);
            Assert.AreEqual(subType.SubscriptionTypeName, subTypeName);
        }

        // Retrieve subscription type with empty name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetSubscriptionTypeEmptyName()
        {
            String subTypeName = "";
            SubscriptionType subType = subscriptionTypeService.GetSubscriptionTypeByName(subTypeName);
        }

        // Retrieve subscription type with inexistent name
        [TestMethod]
        public void TestGetSubscriptionTypeWrongName()
        {
            String subTypeName = "WrongSubscription";
            SubscriptionType subType = subscriptionTypeService.GetSubscriptionTypeByName(subTypeName);
            Assert.IsNull(subType);
        }

        // -------------- DROPS

        // Drop subscription type with null name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropSubscriptionTypeWithEmptyName()
        {
            String subTypeName = "";
            subscriptionTypeService.DropSubscriptionType(subTypeName);
        }

        // Drop subscription type with invalid name
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropSubscriptionTypeWithInvalidName()
        {
            String subTypeName = "WrongSubscription";
            subscriptionTypeService.DropSubscriptionType(subTypeName);
        }

        // Drop subscription type
        [TestMethod]
        public void TestDropSubscriptionType()
        {
            String subTypeName = "Mobil";
            SubscriptionType subType = subscriptionTypeService.GetSubscriptionTypeByName(subTypeName);
            Assert.IsNotNull(subType);
            subscriptionTypeService.DropSubscriptionType(subTypeName);
            SubscriptionType subTypeRemoved = subscriptionTypeService.GetSubscriptionTypeByName(subTypeName);
            Assert.IsNull(subTypeRemoved);
        }

    }
}
