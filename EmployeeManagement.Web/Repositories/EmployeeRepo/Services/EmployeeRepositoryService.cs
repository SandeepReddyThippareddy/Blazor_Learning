using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.Repositories.EmployeeRepo.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repositories.EmployeeRepo.Services
{
    public class EmployeeRepositoryService : IEmployeeRepositoryService
    {
        private readonly HttpClient _httpClient;

        public EmployeeRepositoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> GetEmployeeAsync(string Id)
        {
            return await _httpClient.GetJsonAsync<Employee>($"api/Employee/{Int32.Parse(Id)}");
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _httpClient.GetJsonAsync<Employee[]>("api/Employee");
        }
    }
}
