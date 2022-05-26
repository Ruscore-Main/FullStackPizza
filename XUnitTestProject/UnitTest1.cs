using System;
using Xunit;
using PizzaProject;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace XUnitTestProject
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

    public class UnitTest2
    {
        private readonly HttpClient _client;
        private int createdId;
        public UnitTest2()
        {
            var server = new TestServer(new WebHostBuilder().UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
        }

        // ������������ ��������� ������
        [Theory]
        [InlineData("GET")]
        public async Task GetTestAsync1(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/pizza");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // ������������ DELETE - ������ �������� ������ 
        [Theory]
        [InlineData(111111111)]
        public async Task DeleteTestAsync1(int? id = null)
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/pizza/{id}");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // ������������ DELETE - ������ �������� ������ 
        [Theory]
        [InlineData(null)]
        public async Task DeleteTestAsync2(int? id = null)
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/pizza/{id}");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }


}
