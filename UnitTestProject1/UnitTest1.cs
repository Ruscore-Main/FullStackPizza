using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaProject;
using PizzaProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class TestingCreateValidation
    {
        // Тестирование валидации создания
        [TestMethod]
        public void TestCreateValidation1()
        {
            Assert.AreEqual(Validate.CreateValidatoin("A", 350, 1, 5), "Длина названия должна быть больше 2х символов");
        }

        [TestMethod]
        public void TestCreateValidation2()
        {
            Assert.AreEqual(Validate.CreateValidatoin("Пепперони", -200, 1, 5), "Цена не может быть отрицательной");
        }

        [TestMethod]
        public void TestCreateValidation3()
        {
            Assert.AreEqual(Validate.CreateValidatoin("Пепперони", 350, -5, 5), "Категория не может быть отрицательной");
        }

        [TestMethod]
        public void TestCreateValidation4()
        {
            Assert.AreEqual(Validate.CreateValidatoin("Пепперони", 350, 5, -40), "Категория не может быть отрицательной");
        }

        [TestMethod]
        public void TestCreateValidation5()
        {
            Assert.AreEqual(Validate.CreateValidatoin("Пепперони", 350, 5, 9), "Успешно");
        }

    }

    public class TestingCreate
    {
        HomeController controller;
        [TestMethod]
        public void TestCreate1()
        {
            var result = controller.Get(null, null);
            Assert.IsInstanceOfType<JsonResult>(result);
        }
    }
}
