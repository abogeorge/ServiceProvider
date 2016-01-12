using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DataMapper.Exceptions;
using DomainModel;

namespace ServiceProviderUnitTests
{
    [TestClass]
    public class CurrencyRateTest
    {

        private ICurrencyService currencyService;
        private ICurrencyRateService currencyRateService;

        public CurrencyRateTest()
        {
            currencyService = new CurrencyService();
            currencyRateService = new CurrenyRateService();
        }

        // Add currency
        [TestMethod]
        public void TestAddCurrency()
        {
            currencyService.AddCurrency(
              new Currency { CurrencyName = "Euro" });
        }

        // Add a null currency rate
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddNullCurrencyRate()
        {
            currencyRateService.AddCurrencyRate(null, null);
        }

        // Add currency rate with < 0 rate
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCurrencyRateWithLTZeroRate()
        {
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            currencyRateService.AddCurrencyRate(new CurrencyRate { Currency = validCurrency, RateToRON = -1.0, Valability = "12.2015" }, validCurrency);
        }

        // Add currency rate with > 10000 rate
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCurrencyRateWithGTMaxRate()
        {
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            currencyRateService.AddCurrencyRate(new CurrencyRate { Currency = validCurrency, RateToRON = 10001, Valability = "12.2015" }, validCurrency);
        }

        // Add currency rate with double rate
        [TestMethod]
        public void TestAddCurrencyRateWithDoubleRate()
        {
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            currencyRateService.AddCurrencyRate(new CurrencyRate { Currency = validCurrency, RateToRON = 2.13, Valability = "10.2015" }, validCurrency);
        }

        // Add currency rate with invalid valability
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCurrencyRateWithInvalidValability()
        {
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            currencyRateService.AddCurrencyRate(new CurrencyRate { Currency = validCurrency, RateToRON = 2.13, Valability = "2015" }, validCurrency);
        }

        // Add currency rate with invalid valability 2
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddCurrencyRateWithMoreInvalidValability()
        {
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            currencyRateService.AddCurrencyRate(new CurrencyRate { Currency = validCurrency, RateToRON = 2.13, Valability = "20a" }, validCurrency);
        }

        // Add currency rate
        [TestMethod]
        public void TestAddCurrencyRate()
        {
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            currencyRateService.AddCurrencyRate(new CurrencyRate { Currency = validCurrency, RateToRON = 2.13, Valability = "12.2015" }, validCurrency);
        }

        // Retrieve currency rate
        [TestMethod]
        public void TestGetCurrencyRateByValability()
        {
            String currencyVal = "12.2015";
            Double rate = 2.13;
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            CurrencyRate currencyRate = currencyRateService.GetCurrencyRateByMonth(currencyVal, validCurrency);
            Assert.AreEqual(currencyRate.RateToRON, rate);
        }

        // Retrieve currency rate with inexistent Currency
        [TestMethod]
        public void TestGetCurrencyRateInvalidCurrency()
        {
            String currencyVal = "12.2015";
            Currency badCurrency = new Currency { CurrencyName = "USB" };
            CurrencyRate currencyRate = currencyRateService.GetCurrencyRateByMonth(currencyVal, badCurrency);
            Assert.IsNull(currencyRate);
        }

        // Retrieve currency rate with inexistent rate
        [TestMethod]
        public void TestGetCurrencyRateInvalidValabiility()
        {
            String currencyVal = "08.2015";
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            CurrencyRate currencyRate = currencyRateService.GetCurrencyRateByMonth(currencyVal, validCurrency);
            Assert.IsNull(currencyRate);
        }

        // Retrieve currency rate with empty valability
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetCurrencyRateEmptyValabiility()
        {
            String currencyVal = "";
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            CurrencyRate currencyRate = currencyRateService.GetCurrencyRateByMonth(currencyVal, validCurrency);
        }

        // -------------- DROPS

        // Drop currency rate with null valability
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropCurrencyRateWithEmptyName()
        {
            String currencyVal = "";
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            currencyRateService.DropCurrencyRate(currencyVal, validCurrency);
        }

        // Drop currency rate with invalid valability
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropCurrencyRateWithInvalidVal()
        {
            String currencyVal = "08.2015";
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            currencyRateService.DropCurrencyRate(currencyVal, validCurrency);
        }

        // Drop currency rate with inexistent Currency
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropCurrencyRateInvalidCurrency()
        {
            String currencyVal = "12.2015";
            Currency badCurrency = new Currency { CurrencyName = "USB" };
            currencyRateService.DropCurrencyRate(currencyVal, badCurrency);
        }

        // Drop currency rate
        [TestMethod]
        public void TestDropCurrencyRate()
        {
            String currencyVal = "12.2015";
            Double rate = 2.13;
            Currency validCurrency = currencyService.GetCurrencyByName("Euro");
            CurrencyRate currencyRate = currencyRateService.GetCurrencyRateByMonth(currencyVal, validCurrency);
            Assert.AreEqual(currencyRate.RateToRON, rate);

            currencyRateService.DropCurrencyRate(currencyVal, validCurrency);
            CurrencyRate currencyRateDeleted = currencyRateService.GetCurrencyRateByMonth(currencyVal, validCurrency);
            Assert.IsNull(currencyRateDeleted);
            //currencyService.DropCurrency(validCurrency.CurrencyName);
        }

    }
}
