using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DataMapper.Exceptions;
using DomainModel;

namespace ServiceProviderUnitTests
{

    [TestClass]
    public class MessageTypeTest
    {

        private IMessageTypeService messageTypeService;

        public MessageTypeTest()
        {
            messageTypeService = new MessageTypeService();
        }

        // Add null message type
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddNullMessageType()
        {
            messageTypeService.AddMessageType(null);
        }

        // Add message type with 1 character name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMessageTypeWithOneCharName()
        {
            messageTypeService.AddMessageType(
                new MessageType { MessageTypeName = "A" });
        }

        // Add message type with 31 character name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMessageTypeWithOverMaxCharName()
        {
            messageTypeService.AddMessageType(
                new MessageType { MessageTypeName = "abcdefghiklmnopqrstuvwxzyabcdef" });
        }

        // Add message type 
        [TestMethod]
        public void TestAddMessageType()
        {
            messageTypeService.AddMessageType(
                new MessageType { MessageTypeName = "Mobil" });
        }

        // Retrieve message type
        [TestMethod]
        public void TestGetMessageTypeByName()
        {
            String mesTypeName = "Mobil";
            MessageType mesType = messageTypeService.GetMessageTypeByName(mesTypeName);
            Assert.AreEqual(mesType.MessageTypeName, mesTypeName);
        }

        // Retrieve message type with empty name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetMessageTypeEmptyName()
        {
            String mesTypeName = "";
            MessageType mesType = messageTypeService.GetMessageTypeByName(mesTypeName);
        }

        // Retrieve message type with inexistent name
        [TestMethod]
        public void TestGetMessageTypeWrongName()
        {
            String mesTypeName = "WrongMessageType";
            MessageType mesType = messageTypeService.GetMessageTypeByName(mesTypeName);
            Assert.IsNull(mesType);
        }

        // -------------- DROPS

        // Drop message type with null name
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropMessageTypeWithEmptyName()
        {
            String mesTypeName = "";
            messageTypeService.DropMessageType(mesTypeName);
        }

        // Drop message type with invalid name
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropMessageTypeWithInvalidName()
        {
            String mesTypeName = "WrongSubscription";
            messageTypeService.DropMessageType(mesTypeName);
        }

        // Drop message type
        [TestMethod]
        public void TestDropMessageType()
        {
            String mesTypeName = "Mobil";
            MessageType mesType = messageTypeService.GetMessageTypeByName(mesTypeName);
            Assert.IsNotNull(mesType);
            messageTypeService.DropMessageType(mesTypeName);
            MessageType mesTypeRemoved = messageTypeService.GetMessageTypeByName(mesTypeName);
            Assert.IsNull(mesTypeRemoved);
        }

    }
}
