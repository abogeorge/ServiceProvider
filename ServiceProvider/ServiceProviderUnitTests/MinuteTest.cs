using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DataMapper.Exceptions;
using DomainModel;

namespace ServiceProviderUnitTests
{
    [TestClass]
    public class MinuteTest
    {

        private IMinuteTypeService minuteTypeService;
        private IMinuteService minuteService;
        private static String validMinType = "National";

        public MinuteTest()
        {
            minuteTypeService = new MinuteTypeService();
            minuteService = new MinuteService();
        }

        // Add minute type 
        [TestMethod]
        public void TestAddMinuteType()
        {
            minuteTypeService.AddMinuteType(
                new MinuteType { MinuteTypeName = validMinType });
        }

        // Add a null minute
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddNullMinute()
        {
            minuteService.AddMinute(null);
        }

        // Add minute with included minutes < 0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteWithLTZeroIncludedMins()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = -1 });
        }

        // Add minute with included minutes > 100000
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteWithGTMaxIncludedMins()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = 100001 });
        }

        // Add minute with extra charge < 0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteWithLTZeroExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = -1, IncludedMinutes = 2000 });
        }

        // Add minute with extra charge > 1000.0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteWithGTMaxExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 1001, IncludedMinutes = 2000 });
        }

        // Add minute with included minutes = 0
        [TestMethod]
        public void TestAddMinuteWithZeroIncludedMins()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = 0 });
        }

        // Add minute with extra charge = 0
        [TestMethod]
        public void TestAddMinuteWithZeroExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 0, IncludedMinutes = 1000 });
        }

        // Add minute with included minutes = max
        [TestMethod]
        public void TestAddMinuteWithMaxIncludedMins()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = 100000 });
        }

        // Add minute with extra charge = max
        [TestMethod]
        public void TestAddMinuteWithMaxExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 1000, IncludedMinutes = 1000 });
        }

        // Add a minute
        [TestMethod]
        public void TestAddMinute()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = 500 });
        }

        // Retrieve minute by id
        [TestMethod]
        public void TestGetMinuteByID()
        {
            Minute minute = minuteService.GetMinuteById(1);
            Assert.AreEqual(minute.IncludedMinutes, 0);
        }

        // Retrieve minute with empty id
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetMinuteByEmptyID()
        {
            Minute minute = minuteService.GetMinuteById(0);
        }

        // Retrieve minute with false id
        [TestMethod]
        public void TestGetMinuteByFalseID()
        {
            Minute minute = minuteService.GetMinuteById(1000);
            Assert.IsNull(minute);
        }

    }
}
