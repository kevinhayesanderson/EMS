using API;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Shared;
using System.Net;
using System.Net.Http.Json;
using static PubAppTest.APITests;

namespace Tests
{
    [TestClass]
    public partial class APITests
    {
        [TestMethod]
        public async Task IsWeatherApiRunning()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var response = await client.GetStringAsync("/weatherforecast");
            Assert.AreEqual("[{\"date", response.Substring(0, 7));
        }

        [TestMethod]
        public async Task IsEmployeeApiRunning()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var response = await client.GetStringAsync("/api/employees/");
            Assert.AreEqual("[{\"id\":", response.Substring(0, 7));
        }

        [TestMethod]
        public async Task CanRetrieveEmployees()
        {
            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();
            var employees = await client.GetFromJsonAsync<List<EmployeeDto>>("/api/employees/");
            Assert.IsInstanceOfType(employees, typeof(List<EmployeeDto>));
            Assert.IsTrue(employees.Any());
        }

        [TestMethod]
        public async Task CanRetrieveAnEmployee()
        {
            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();
            var employee = await client.GetFromJsonAsync<EmployeeDto>("/api/employees/1");
            Assert.IsInstanceOfType(employee, typeof(EmployeeDto));
        }

        [TestMethod]
        public async Task CanCreateEmployee()
        {
            var employeeDto = new EmployeeDto_Admin()
            {
                Id = 4,
                Name = "name4",
                Email = "email4",
                Position = "position4",
                Salary = 0,
                Role = "role4"
            };
            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();
            client.DefaultRequestHeaders.Add("RoleType", "Admin");
            var response = await client.PostAsJsonAsync("/api/employees/", employeeDto);
            var employee = await response.Content.ReadFromJsonAsync<EmployeeDto>();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreNotEqual(0, employee.Id);
        }

        [TestMethod]
        public async Task CanUpdateEmployee()
        {
            var employeeDto = new EmployeeDto()
            {
                Id = 3,
                Name = "updatedName",
                Email = "email4",
                Position = "position4",
                Role = "role4"
            };
            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();
            client.DefaultRequestHeaders.Add("RoleType", "Admin");
            var response = await client.PutAsJsonAsync("/api/employees/3", employeeDto);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
            var employee = await client.GetFromJsonAsync<EmployeeDto>("/api/employees/3");
            Assert.IsInstanceOfType(employee, typeof(EmployeeDto));
            Assert.AreEqual("updatedName", employee.Name);
        }

        [TestMethod]
        public async Task CanDeleteEmployee()
        {
            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();
            client.DefaultRequestHeaders.Add("RoleType", "Admin");
            var response = await client.DeleteAsync("/api/employees/1");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        private static void CreateAndSeedDatabase(WebApplicationFactory<Program> appFactory)
        {
            using var scope = appFactory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<Context>();
            db.Database.EnsureCreated();
        }
    }
}