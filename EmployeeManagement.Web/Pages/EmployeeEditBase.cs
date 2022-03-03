using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.Repositories.DepartmentRepo.Interfaces;
using EmployeeManagement.Web.Repositories.EmployeeRepo.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeEditBase : ComponentBase
    {
        [Inject]
        public IEmployeeRepositoryService EmployeeService { get; set; }
        [Inject]
        public IDepartmentRepositoryService DepartmentService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public string Reason { get; set; } = string.Empty;

        public Employee Employee { get; set; } = new Employee();

        public IEnumerable<Department> Departments { get; set; } = new List<Department>();

        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetDepartmentsAsync();

            Employee = await EmployeeService.GetEmployeeAsync(Id);

            Departments = await DepartmentService.GetDepartmentsAsync();

        }

        protected void HandleValidSubmit()
        {

        }
    }
}
