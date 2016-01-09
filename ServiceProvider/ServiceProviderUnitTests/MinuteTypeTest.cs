using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DataMapper.Exceptions;
using DomainModel;

namespace ServiceProviderUnitTests
{
    [TestClass]
    public class MinuteTypeTest
    {

        private IMinuteTypeService minuteTypeService;

        public MinuteTypeTest()
        {
            minuteTypeService = new MinuteTypeService();
        }

        // Add a null minute type
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddNullMinuteType()
        {
            minuteTypeService.AddMinuteType(null);
        }

        // Add minute type with 1 character name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteTypeWithOneCharName()
        {
            minuteTypeService.AddMinuteType(
                new MinuteType { MinuteTypeName = "A" });
        }

        // Add minute type with 31 character name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMinuteTypeWithOverMaxCharName()
        {
            minuteTypeService.AddMinuteType(
                new MinuteType { MinuteTypeName = "abcdefghiklmnopqrstuvwxzyabcdef" });
        }

        // Add minute type 
        [TestMethod]
        public void TestAddMinuteType()
        {
            minuteTypeService.AddMinuteType(
                new MinuteType { MinuteTypeName = "National" });
        }

        // Retrieve minute type
        [TestMethod]
        public void TestGetMinuteTypeByName()
        {
            String minTypeName = "National";
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(minTypeName);
            Assert.AreEqual(minType.MinuteTypeName, minTypeName);
        }

        // Retrieve minute type with empty name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetMinuteTypeEmptyName()
        {
            String minTypeName = "";
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(minTypeName);
        }

        // Retrieve minute type with inexistent name
        [TestMethod]
        public void TestGetMinuteTypeWrongName()
        {
            String minTypeName = "WrongMinuteType";
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(minTypeName);
            Assert.IsNull(minType);
        }

        // -------------- DROPS

        // Drop minute type with null name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropMinuteTypeWithEmptyName()
        {
            String minTypeName = "";
            minuteTypeService.DropMinuteType(minTypeName);
        }

        // Drop minute type with invalid name
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropMinuteTypeWithInvalidName()
        {
            String minTypeName = "WrongMinuteType";
            minuteTypeService.DropMinuteType(minTypeName);
        }

        // Drop minute type
        [TestMethod]
        public void TestDropMinuteType()
        {
            String minTypeName = "National";
            MinuteType minType = minuteTypeService.GetMinuteTypeByName(minTypeName);
            Assert.IsNotNull(minType);
            minuteTypeService.DropMinuteType(minTypeName);
            MinuteType minTypeRemoved = minuteTypeService.GetMinuteTypeByName(minTypeName);
            Assert.IsNull(minTypeRemoved);
        }

    }
}
