using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaProject;
using PizzaProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class TestingCreateValidation
    {
        // ������������ ��������� ��������
        [TestMethod]
        public void TestCreateValidation1()
        {
            Assert.AreEqual(Validate.CreateValidatoin("A", 350, 1, 5), "����� �������� ������ ���� ������ 2� ��������");
        }

        [TestMethod]
        public void TestCreateValidation2()
        {
            Assert.AreEqual(Validate.CreateValidatoin("���������", -200, 1, 5), "���� �� ����� ���� �������������");
        }

        [TestMethod]
        public void TestCreateValidation3()
        {
            Assert.AreEqual(Validate.CreateValidatoin("���������", 350, -5, 5), "��������� �� ����� ���� �������������");
        }

        [TestMethod]
        public void TestCreateValidation4()
        {
            Assert.AreEqual(Validate.CreateValidatoin("���������", 350, 5, -40), "��������� �� ����� ���� �������������");
        }

        [TestMethod]
        public void TestCreateValidation5()
        {
            Assert.AreEqual(Validate.CreateValidatoin("���������", 350, 5, 9), "�������");
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
