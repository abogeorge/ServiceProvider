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
            minuteService.AddMinute(null, null);
        }

        // Add minute with included minutes < 0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteWithLTZeroIncludedMins()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = -1 }, minType);
        }

        // Add minute with included minutes > 100000
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteWithGTMaxIncludedMins()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = 100001 }, minType);
        }

        // Add minute with extra charge < 0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteWithLTZeroExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = -1, IncludedMinutes = 2000 }, minType);
        }

        // Add minute with extra charge > 1000.0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteWithGTMaxExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 1001, IncludedMinutes = 2000 }, minType);
        }

        // Add minute with included minutes = 0
        [TestMethod]
        public void TestAddMinuteWithZeroIncludedMins()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = 0 }, minType);
        }

        // Add minute with extra charge = 0
        [TestMethod]
        public void TestAddMinuteWithZeroExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 0, IncludedMinutes = 1000 }, minType);
        }

        // Add minute with included minutes = max
        [TestMethod]
        public void TestAddMinuteWithMaxIncludedMins()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = 100000 }, minType);
        }

        // Add minute with extra charge = max
        [TestMethod]
        public void TestAddMinuteWithMaxExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 1000, IncludedMinutes = 1000 }, minType);
        }

        // Add a minute
        [TestMethod]
        public void TestAddMinute()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.AddMinute(new Minute { MinuteType = minType, ExtraCharge = 7, IncludedMinutes = 500 }, minType);
        }

        // Retrieve minute by id
        [TestMethod]
        public void TestGetMinuteByID()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minute.IncludedMinutes, 0);
        }

        // Retrieve minute with empty id
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetMinuteByEmptyID()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(0, minType);
        }

        // Retrieve minute with false id
        [TestMethod]
        public void TestGetMinuteByFalseID()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1000, minType);
            Assert.IsNull(minute);
        }

        // Update minute extra charge by id
        [TestMethod]
        public void TestUpdateMinuteExtraCharge()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minute.ExtraCharge, 7);
            minute.ExtraCharge = 12;
            minuteService.UpdateExtraCharge(minute);
            Minute minuteUpdated = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minuteUpdated.ExtraCharge, 12);
        }

        // Update minute extra charge invalid 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateMinuteExtraChargeInvalid()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minute.ExtraCharge, 12);
            minute.ExtraCharge = -1;
            minuteService.UpdateExtraCharge(minute);
        }

        // Update minute extra charge invalid 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateMinuteExtraChargeInvalidGTMax()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minute.ExtraCharge, 12);
            minute.ExtraCharge = 15000;
            minuteService.UpdateExtraCharge(minute);
        }

        // Update minute included minutes
        [TestMethod]
        public void TestUpdateMinuteIncluded()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minute.IncludedMinutes, 0);
            minute.IncludedMinutes = 10;
            minuteService.UpdateIncludedMinutes(minute);
            Minute minuteUpdated = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minuteUpdated.IncludedMinutes, 10);
        }

        // Update minutes included invalid 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateMinuteIncludedInvalid()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minute.IncludedMinutes, 10);
            minute.IncludedMinutes = -1;
            minuteService.UpdateIncludedMinutes(minute);
        }

        // Update minutes included invalid 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateMinuteIncludedInvalidGTMax()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1, minType);
            Assert.AreEqual(minute.IncludedMinutes, 10);
            minute.IncludedMinutes = 150000;
            minuteService.UpdateIncludedMinutes(minute);
        }

        // -------------- DROPS

        // Drop minute with null id
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropMinuteWithNullId()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.DropMinute(0, minType);
        }

        // Drop minute with inexistent id
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropMinuteWithFalseId()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            minuteService.DropMinute(5000, minType);
        }

        // Drop Minute
        [TestMethod]
        public void TestDropMinute()
        {
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(validMinType);
            Minute minute = minuteService.GetMinuteById(1, minType);
            Assert.IsNotNull(minute);
            minuteService.DropMinute(1, minType);
            Minute minuteDropped = minuteService.GetMinuteById(1, minType);
            Assert.IsNull(minuteDropped);
        }

    }
}
