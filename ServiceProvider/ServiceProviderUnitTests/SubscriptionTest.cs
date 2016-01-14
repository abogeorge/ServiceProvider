using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DomainModel;
using DataMapper.Exceptions;

namespace ServiceProviderUnitTests
{
    [TestClass]
    public class SubscriptionTest
    {
        private ISubscriptionService subscriptionService;
        private ISubscriptionTypeService subTypeService;
        private ICurrencyService currencyService;

        private static String validSubType = "Mobil";
        private static String validCurrency = "Euro";

        public SubscriptionTest()
        {
            this.subscriptionService = new SubscriptionSerivce();
            this.subTypeService = new SubscriptionTypeService();
            this.currencyService = new CurrencyService();
        }

        [TestMethod]
        public void TestAddGetSubscriptionType()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            if (subType == null)
            {
                subTypeService.AddSubscriptionType(new SubscriptionType { SubscriptionTypeName = validSubType });
            }
        }

        [TestMethod]
        public void TestAddGetCurrency()
        {
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            if (currency == null)
            {
                currencyService.AddCurrency(new Currency { CurrencyName = validCurrency });
            }
        }

        // Add a null subscription
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddNullSubscription()
        {
            subscriptionService.AddSubscription(null, null, null);
        }

        // Add subscription
        [TestMethod]
        public void TestAddSubscription()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription { Available = true, Currency = currency,
                FixedPeriod = 24, Price = 10, SubscriptionName = "Abonament 10", SubType = subType }, 
                subType, currency);
        }

        // Add subscription with short name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddSubscriptionWithShortName()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 24,
                Price = 10,
                SubscriptionName = "Ab",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription with long name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddSubscriptionWithLongName()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 24,
                Price = 10,
                SubscriptionName = "abcdefghiklmnopqrstuvwxzyabcdef",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription with fixed name
        [TestMethod]
        public void TestAddSubscriptionWithFixName()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 24,
                Price = 10,
                SubscriptionName = "Abo",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription with negative price
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddSubscriptionWithNegativePrice()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 24,
                Price = -2,
                SubscriptionName = "Abonament Neg",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription with over max price
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddSubscriptionWithOverMaxPrice()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 24,
                Price = 1001,
                SubscriptionName = "Abonament 1001",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription with double price
        [TestMethod]
        public void TestAddSubscriptionWithDoublePrice()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 24,
                Price = 4.3,
                SubscriptionName = "Abonament D",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription with negative valability
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddSubscriptionWithNegativeValability()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = -2,
                Price = 7,
                SubscriptionName = "Abonament Neg",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription with over max valability
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddSubscriptionWithOverMaxValability()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 50,
                Price = 7,
                SubscriptionName = "Abonament OvMax",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription with min valability
        [TestMethod]
        public void TestAddSubscriptionWithMinValability()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 0,
                Price = 7,
                SubscriptionName = "Abonament Test",
                SubType = subType
            },
                subType, currency);
        }

        // Add subscription duplicate
        [TestMethod]
        [ExpectedException(typeof(DuplicateException))]
        public void TestAddSubscriptionDuplicate()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            subscriptionService.AddSubscription(new Subscription
            {
                Available = true,
                Currency = currency,
                FixedPeriod = 0,
                Price = 7,
                SubscriptionName = "Abonament Test",
                SubType = subType
            },
                subType, currency);
        }

        // Get subscription
        [TestMethod]
        public void TestGetSubscription()
        {
            String subName = "Abonament Test";
            Subscription sub = subscriptionService.GetSubscriptionByName(subName);
            Assert.AreEqual(sub.SubscriptionName, subName);
        }

        // Get subscription with empty name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetSubscriptionWithEmptyName()
        {
            String subName = "";
            Subscription sub = subscriptionService.GetSubscriptionByName(subName);
        }

        // Get subscription with inexistent name
        [TestMethod]
        public void TestGetSubscriptionInexistent()
        {
            String subName = "Inexistent";
            Subscription sub = subscriptionService.GetSubscriptionByName(subName);
            Assert.IsNull(sub);
        }

        // Update subscription price
        [TestMethod]
        public void TestUpdateSubscriptionPrice()
        {
            String subName = "Abonament Test";
            Double oldPrice = 7;
            Double newPrice = 8.5;
            Subscription sub = subscriptionService.GetSubscriptionByName(subName);
            Assert.AreEqual(sub.Price, oldPrice);
            subscriptionService.UpdateSubscriptionPrice(subName, newPrice);
            Subscription subNew = subscriptionService.GetSubscriptionByName(subName);
            Assert.AreEqual(subNew.Price, newPrice);
        }

        // Update subscription price invalid name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdatePriceSubscriptionInvalid()
        {
            String subName = "";
            subscriptionService.UpdateSubscriptionPrice(subName, 100);
        }

        // Update subscription price inexitent name
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdatePriceSubscriptionInexistent()
        {
            String subName = "Invalid";
            subscriptionService.UpdateSubscriptionPrice(subName, 100);
        }

        // Update subscription price invalid price < 0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdatePriceSubscriptionInvalidPrice()
        {
            String subName = "Abonament Test";
            subscriptionService.UpdateSubscriptionPrice(subName, -2);
        }

        // Update subscription price invalid price > MAX
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdatePriceSubscriptionInvalidPriceGTMax()
        {
            String subName = "Abonament Test";
            subscriptionService.UpdateSubscriptionPrice(subName, 1001);
        }

        // Update subscription period
        [TestMethod]
        public void TestUpdateSubscriptionPeriod()
        {
            String subName = "Abonament Test";
            int oldPeriod = 0;
            int newPeriod = 12;
            Subscription sub = subscriptionService.GetSubscriptionByName(subName);
            Assert.AreEqual(sub.FixedPeriod, oldPeriod);
            subscriptionService.UpdateSubscriptionFixedPeriod(subName, newPeriod);
            Subscription subNew = subscriptionService.GetSubscriptionByName(subName);
            Assert.AreEqual(subNew.FixedPeriod, newPeriod);
        }

        // Update subscription period invalid name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdatePeriodSubscriptionInvalid()
        {
            String subName = "";
            subscriptionService.UpdateSubscriptionFixedPeriod(subName, 24);
        }

        // Update subscription period inexitent name
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdatePeriodSubscriptionInexistent()
        {
            String subName = "Invalid";
            subscriptionService.UpdateSubscriptionFixedPeriod(subName, 24);
        }

        // Update subscription period invalid period < 0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdatePriceSubscriptionInvalidPeriod()
        {
            String subName = "Abonament Test";
            subscriptionService.UpdateSubscriptionFixedPeriod(subName, -1);
        }

        // Update subscription period invalid period > MAX
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdatePeriodSubscriptionInvalidPeriodTMax()
        {
            String subName = "Abonament Test";
            subscriptionService.UpdateSubscriptionFixedPeriod(subName, 49);
        }

        // Update subscription availability
        [TestMethod]
        public void TestUpdateSubscriptionAvailability()
        {
            String subName = "Abonament Test";
            Subscription sub = subscriptionService.GetSubscriptionByName(subName);
            Assert.AreEqual(sub.Available, true);
            subscriptionService.UpdateSubscriptionAvailability(subName, false);
            Subscription subNew = subscriptionService.GetSubscriptionByName(subName);
            Assert.AreEqual(subNew.Available, false);
        }

        // Update subscription period invalid name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateAvailabilitySubscriptionInvalid()
        {
            String subName = "";
            subscriptionService.UpdateSubscriptionAvailability(subName, false);
        }

        // Update subscription availability inexistent name
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateAvailabilitySubscriptionInexistent()
        {
            String subName = "Invalid";
            subscriptionService.UpdateSubscriptionAvailability(subName, false);
        }

        // DROPS ...............

        // Drop subscription 
        [TestMethod]
        public void TestDropSubscription()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            String subName = "Abonament Test";
            Subscription sub = subscriptionService.GetSubscriptionByName(subName);
            Assert.IsNotNull(sub);
            subscriptionService.DropSubscription(subName, subType, currency);
            Subscription subDel = subscriptionService.GetSubscriptionByName(subName);
            Assert.IsNull(subDel);
        }

        // Drop subscription 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropSubscriptionEmptyName()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            String subName = "";
            subscriptionService.DropSubscription(subName, subType, currency);
        }

        // Drop subscription 
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropSubscriptionInvalidName()
        {
            SubscriptionType subType = subTypeService.GetSubscriptionTypeByName(validSubType);
            Currency currency = currencyService.GetCurrencyByName(validCurrency);
            String subName = "Invalid";
            subscriptionService.DropSubscription(subName, subType, currency);
        }

    }
}
