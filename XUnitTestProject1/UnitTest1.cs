using System;
using Xunit;
using PizzaProject;
using Microsoft.AspNetCore.Mvc;
using PizzaProject.Controllers;
using PizzaProject.Models;
using Microsoft.AspNetCore.Hosting;


namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestCreateValidation1()
        {
            Assert.Equal(Validate.CreateValidatoin("A", 350, 1, 5), "Длина названия должна быть больше 2х символов");
        }

        [Fact]
        public void TestCreateValidation2()
        {
            Assert.Equal(Validate.CreateValidatoin("Пепперони", -200, 1, 5), "Цена не может быть отрицательной");
        }

        [Fact]
        public void TestCreateValidation3()
        {
            Assert.Equal(Validate.CreateValidatoin("Пепперони", 350, -5, 5), "Категория не может быть отрицательной");
        }

        [Fact]
        public void TestCreateValidation4()
        {
            Assert.Equal(Validate.CreateValidatoin("Пепперони", 350, 5, -40), "Рейтинг не может быть отрицательной");
        }

        [Fact]
        public void TestCreateValidation5()
        {
            Assert.Equal(Validate.CreateValidatoin("Пепперони", 350, 5, 9), "Успешно");
        }
    }
}


public class TestingCreate
{
    public TestingCreate()
    {
    }
}