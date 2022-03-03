using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.Repositories.DepartmentRepo.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repositories.DepartmentRepo.Services
{
    public class DepartmentRepositoryService : IDepartmentRepositoryService
    {
        private readonly HttpClient _httpClient;

        public DepartmentRepositoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Department> GetDepartmentAsync(string Id)
        {
            return await _httpClient.GetJsonAsync<Department>($"api/Departments/{Int32.Parse(Id)}");
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _httpClient.GetJsonAsync<Department[]>("api/Departments");
        }
    }
}
