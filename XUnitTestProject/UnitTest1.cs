using System;
using Xunit;
using PizzaProject;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using PizzaProject.Controllers;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

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
            Assert.Equal(Validate.CreateValidatoin("���������", 350, 5, 9, new List<int>() {  }), "� ������ ������ ���� ������� �������!");
        }

        [Fact]
        public void TestCreateValidation6()
        {
            Assert.Equal(Validate.CreateValidatoin("���������", 350, 5, 9, new List<int>() { 1 }), "����� �������������� ������!");
        }

        [Fact]
        public void TestCreateValidation7()
        {
            Assert.Equal(Validate.CreateValidatoin("���������", 350, 5, 9, new List<int>() { 26, 30 }, new List<int>() {  }), "� ������ ������ ���� ������� ����!");
        }

        [Fact]
        public void TestCreateValidation8()
        {
            Assert.Equal(Validate.CreateValidatoin("���������", 350, 5, 9, new List<int>() { 26, 30 }, new List<int>() { 11 }), "����� �������������� ���!");
        }

        [Fact]
        public void TestCreateValidation9()
        {
            Assert.Equal(Validate.CreateValidatoin("���������", 350, 5, 9, new List<int>() { 26, 30 }, new List<int>() { 0, 1 }, new List<string>() { }), "� ������ ������ ���� ���� �� ���� �����������!");
        }

        [Fact]
        public void TestCreateValidation10()
        {
            var sizes = new List<int>() { 26, 30 };
            var types = new List<int>() { 0, 1 };
            var imageUrls = new List<string>() { "https://dodopizza.azureedge.net/static/Img/Products/Pizza/ru-RU/d2e337e9-e07a-4199-9cc1-501cc44cb8f8.jpg" };
            Assert.Equal(Validate.CreateValidatoin("���������", 350, 5, 9, sizes, types, imageUrls), "�������");
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

        // GET - ������������ ��������� ������
        [Theory]
        [InlineData("GET")]
        public async Task GetTestAsync(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/pizza");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // ������������ ��������� ������ � �����������
        [Theory]
        [InlineData("GET")]
        public async Task GetSortTestAsync(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/pizza?_sort=price");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // ������������ ��������� ������ � ����������
        [Theory]
        [InlineData("GET")]
        public async Task GetCategoryTestAsync(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/pizza?category=1");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // ������������ ��������� ������ � ���������� � �����������
        [Theory]
        [InlineData("GET")]
        public async Task GetCategorySortTestAsync(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/pizza?category=1&_sort=price");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // ������������ � ������������ ����������
        [Theory]
        [InlineData("GET")]
        public async Task GetFakeCategoryTestAsync(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/pizza?category=afaf");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //////////////////////

        // ������������ POST - ������ ���������� ������ 
        [Fact]
        public async Task PostTestAsync1()
        {
            PizzaJson pizzaJson = null;
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/pizza");
            request.Content = new StringContent(
                JsonConvert.SerializeObject(pizzaJson),
                Encoding.UTF8, "application/json"
            );
            var response = await _client.SendAsync(request);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostTestAsync2()
        {
            PizzaJson pizzaJson = new PizzaJson { name = "", category = -100 };
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/pizza");
            request.Content = new StringContent(
                JsonConvert.SerializeObject(pizzaJson),
                Encoding.UTF8, "application/json"
            );
            var response = await _client.SendAsync(request);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostTestAsync3()
        {

            PizzaJson pizzaJson = new PizzaJson
            {
                name = "Test",
                price = 200,
                rating = 5,
                category = 3,
                sizes = new List<int>() { 26, 30, 40 },
                types = new List<int>() { 0, 1 },
                imageUrls = new List<string>() {
                    "https://dodopizza.azureedge.net/static/Img/Products/Pizza/ru-RU/b750f576-4a83-48e6-a283-5a8efb68c35d.jpg",
                    "https://dodopizza-a.akamaihd.net/static/Img/Products/8a813e3b734e457c848a60fc70a100d5_1875x1875.jpeg",
                    "https://dodopizza-a.akamaihd.net/static/Img/Products/9763da8d31b64427bd4d75dbfe19038b_1875x1875.jpeg"
                }

            };


            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/pizza");
            request.Content = new StringContent(
                JsonConvert.SerializeObject(pizzaJson),
                Encoding.UTF8, "application/json"
            );
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // ������������ Put - ������ ���������� ������
        [Fact]
        public async Task PutTestAsync1()
        {
            PizzaJson pizzaJson = null;
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/pizza/1");
            request.Content = new StringContent(
                JsonConvert.SerializeObject(pizzaJson),
                Encoding.UTF8, "application/json"
            );
            var response = await _client.SendAsync(request);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutTestAsync2()
        {
            PizzaJson pizzaJson = new PizzaJson()
            {
                name = ""
            };
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/pizza/1");
            request.Content = new StringContent(
                JsonConvert.SerializeObject(pizzaJson),
                Encoding.UTF8, "application/json"
            );
            var response = await _client.SendAsync(request);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutTestAsync3()
        {
            PizzaJson pizzaJson = new PizzaJson()
            {
                id = 11111111,
                name = "Test",
                price = 200,
                rating = 5,
                category = 3,
                sizes = new List<int>() { 26, 30, 40 },
                types = new List<int>() { 0, 1 },
                imageUrls = new List<string>() {
                    "https://dodopizza.azureedge.net/static/Img/Products/Pizza/ru-RU/b750f576-4a83-48e6-a283-5a8efb68c35d.jpg",
                    "https://dodopizza-a.akamaihd.net/static/Img/Products/8a813e3b734e457c848a60fc70a100d5_1875x1875.jpeg",
                    "https://dodopizza-a.akamaihd.net/static/Img/Products/9763da8d31b64427bd4d75dbfe19038b_1875x1875.jpeg"
                }
            };
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/pizza/1");
            request.Content = new StringContent(
                JsonConvert.SerializeObject(pizzaJson),
                Encoding.UTF8, "application/json"
            );
            var response = await _client.SendAsync(request);


            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        // ������������ DELETE - ������ �������� ������ 
        [Theory]
        [InlineData(111111111)]
        public async Task DeleteTestAsync1(int id)
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/pizza/{id}");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Theory]
        [InlineData("dfdfa")]
        public async Task DeleteTestAsync2(string id)
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/pizza/{id}");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(5000)]
        public async Task DeleteTestAsync3(int id)
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/pizza/{id}");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }


}
