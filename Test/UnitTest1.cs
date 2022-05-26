using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PizzaProject;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateValidation1()
        {
            Assert.AreEqual(Validate.CreateValidatoin(), "Успешно");

        }

        [TestMethod]
        public void TestCreateValidation2()
        {

        }
    }
}
