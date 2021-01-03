using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();

            command.FirstName = "Joao";
            command.LastName = "Marcos";
            command.Document = "70449596010";
            command.Email = "Joao@gmail.com";
            command.Phone = "11912345678";

            Assert.AreEqual(true, command.Valid());


        }
    }
}
