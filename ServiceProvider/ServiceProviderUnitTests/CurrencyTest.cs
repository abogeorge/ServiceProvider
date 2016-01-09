using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DataMapper.Exceptions;
using DomainModel;

namespace ServiceProviderUnitTests
{
    [TestClass]
    public class CurrencyTest
    {

        private ICurrencyService currencyService;

        public CurrencyTest()
        {
            currencyService = new CurrencyService();
        }

        // Add a null currency
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddNullCurrency()
        {
            currencyService.AddCurrency(null);
        }

        // Add currency with 1 character name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCurrencyWithOneCharName()
        {
            currencyService.AddCurrency(
                new Currency { CurrencyName = "A" });
        }

        // Add currency with 31 character name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCurrencyWithOverMaxCharName()
        {
            currencyService.AddCurrency(
               new Currency { CurrencyName = "abcdefghiklmnopqrstuvwxzyabcdef" });
        }

        // Add currency
        [TestMethod]
        public void TestAddSubscriptionType()
        {
            currencyService.AddCurrency(
              new Currency { CurrencyName = "Euro" });
        }

        // Retrieve currency
        [TestMethod]
        public void TestGetCurrencyByName()
        {
            String currencyName = "Euro";
            Currency currency = currencyService.GetCurrencyByName(currencyName);
            Assert.AreEqual(currency.CurrencyName, currencyName);
        }

        // Retrieve currency with empty name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetCurrencyEmptyName()
        {
            String currencyName = "";
            Currency currency = currencyService.GetCurrencyByName(currencyName);
        }

        // Retrieve currency with inexistent name
        [TestMethod]
        public void TestGetCurrencyWrongName()
        {
            String currencyName = "WrongCurrency";
            Currency currency = currencyService.GetCurrencyByName(currencyName);
            Assert.IsNull(currency);
        }

        // -------------- DROPS

        // Drop currency with null name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropCurrencyWithEmptyName()
        {
            String currencyName = "";
            currencyService.DropCurrency(currencyName);
        }

        // Drop currency with invalid name
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropSubscriptionTypeWithInvalidName()
        {
            String currencyName = "WrongCurrency";
            currencyService.DropCurrency(currencyName);
        }

        // Drop currency
        [TestMethod]
        public void TestDropCurrency()
        {
            String currencyName = "Euro";
            Currency currency = currencyService.GetCurrencyByName(currencyName);
            Assert.IsNotNull(currency);
            currencyService.DropCurrency(currencyName);
            Currency currencyRemoved = currencyService.GetCurrencyByName(currencyName);
            Assert.IsNull(currencyRemoved);
        }

    }
}
