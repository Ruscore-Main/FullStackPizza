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
            Assert.Equal(Validate.CreateValidatoin("A", 350, 1, 5), "����� �������� ������ ���� ������ 2� ��������");
        }

        [Fact]
        public void TestCreateValidation2()
        {
            Assert.Equal(Validate.CreateValidatoin("���������", -200, 1, 5), "���� �� ����� ���� �������������");
        }

        [Fact]
        public void TestCreateValidation3()
        {
            Assert.Equal(Validate.CreateValidatoin("���������", 350, -5, 5), "��������� �� ����� ���� �������������");
        }

        [Fact]
        public void TestCreateValidation4()
        {
            Assert.Equal(Validate.CreateValidatoin("���������", 350, 5, -40), "������� �� ����� ���� �������������");
        }

        [Fact]
        public void TestCreateValidation5()
        {
            Assert.Equal(Validate.CreateValidatoin("���������", 350, 5, 9), "�������");
        }
    }
}


public class TestingCreate
{
    public TestingCreate()
    {
    }
}