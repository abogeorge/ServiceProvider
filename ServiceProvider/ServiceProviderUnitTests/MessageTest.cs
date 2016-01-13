using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.ServicesImplementation;
using DomainModel;
using DataMapper.Exceptions;

namespace ServiceProviderUnitTests
{
    [TestClass]
    public class MessageTest
    {

        private IMessageTypeService messageTypeService;
        private IMessageService messageService;
        private static String validMesType = "National";

        public MessageTest()
        {
            messageService = new MessageService();
            messageTypeService = new MessageTypeService();
        }

        // Add message type 
        [TestMethod]
        public void TestAddMessageType()
        {
            messageTypeService.AddMessageType(
                new MessageType { MessageTypeName = validMesType });
        }

        // Add a null message
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddNullMessage()
        {
            messageService.AddMessage(null, null);
        }

        // Add message with included messages < 0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMessageWithLTZeroIncludedMes()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = 7, IncludedMessages = -1 }, mesType);
        }

        // Add message with included messages > 100000
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMessageWithGTMaxIncludedMes()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = 7, IncludedMessages = 100001 }, mesType);
        }

        // Add message with extra charge < 0
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMessageWithLTZeroExtraCharge()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = -1, IncludedMessages = 2000 }, mesType);
        }

        // Add message with extra charge > 1000
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddMessageWithGTMaxExtraCharge()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = 1234, IncludedMessages = 2000 }, mesType);
        }

        // Add message with included messages = 0
        [TestMethod]
        public void TestAddMessageWithZeroIncludedMes()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = 7, IncludedMessages = 0 }, mesType);
        }

        // Add message with extra charge = 0
        [TestMethod]
        public void TestAddMessageWithZeroExtraCharge()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = 0, IncludedMessages = 2000 }, mesType);
        }

        // Add message with included messages = 100000
        [TestMethod]
        public void TestAddMessageWithMaxIncludedMes()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = 7, IncludedMessages = 100000 }, mesType);
        }

        // Add message with extra charge = 1000
        [TestMethod]
        public void TestAddMessageWithMaxExtraCharge()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = 1000, IncludedMessages = 2000 }, mesType);
        }

        // Add a message
        [TestMethod]
        public void TestAddMessage()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.AddMessage(new Message { MessageType = mesType, ExtraCharge = 7, IncludedMessages = 2000 }, mesType);
        }

        // Retrieve message by id
        [TestMethod]
        public void TestGetMessageByID()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(message.IncludedMessages, 0);
        }

        // Retrieve message with empty id
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestGetMessageByEmptyID()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(0, mesType);
        }

        // Retrieve message with false id
        [TestMethod]
        public void TestGetMessgeByFalseID()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1000, mesType);
            Assert.IsNull(message);
        }

        // Update message extra charge
        [TestMethod]
        public void TestUpdateMessageExtraCharge()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(message.IncludedMessages, 0);
            message.ExtraCharge = 12;
            messageService.UpdateExtraCharge(message);
            Message mesUpdated = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(mesUpdated.ExtraCharge, 12);
        }

        // Update message extra charge invalid 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateMessageExtraChargeInvalid()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(message.ExtraCharge, 12);
            message.ExtraCharge = -1;
            messageService.UpdateExtraCharge(message);
        }

        // Update minute extra charge invalid 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateMinuteExtraChargeInvalidGTMax()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(message.ExtraCharge, 12);
            message.ExtraCharge = 1500;
            messageService.UpdateExtraCharge(message);
        }

        // Update message included messages
        [TestMethod]
        public void TestUpdateMessageIncluded()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(message.IncludedMessages, 0);
            message.IncludedMessages = 5000;
            messageService.UpdateIncludedMessages(message);
            Message mesUpdated = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(mesUpdated.IncludedMessages, 5000);
        }

        // Update message included invalid 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateMessageIncludedInvalid()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(message.IncludedMessages, 5000);
            message.IncludedMessages = -1;
            messageService.UpdateIncludedMessages(message);
        }

        // Update message included invalid 
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateMessageIncludedInvalidGTMax()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1, mesType);
            Assert.AreEqual(message.IncludedMessages, 5000);
            message.IncludedMessages = 150000;
            messageService.UpdateIncludedMessages(message);
        }

        // -------------- DROPS

        // Drop message with null id
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestDropMessageWithNullId()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.DropMessage(0, mesType);
        }

        // Drop message with inexistent id
        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropMessageWithFalseId()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            messageService.DropMessage(1000, mesType);
        }

        // Drop Message
        [TestMethod]
        public void TestDropMessage()
        {
            MessageType mesType = messageTypeService.GetMessageTypeByName(validMesType);
            Message message = messageService.GetMessageById(1, mesType);
            Assert.IsNotNull(message);
            messageService.DropMessage(1, mesType);
            Message messageDel = messageService.GetMessageById(1, mesType);
            Assert.IsNull(messageDel);
        }

    }
    }
