using AutoMapper;
using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.DTO_s;
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
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        public string Reason { get; set; } = string.Empty;

        public EmployeeEditDTO EmployeeEditDTO { get; set; } = new EmployeeEditDTO();

        public Employee Employee { get; set; } = new Employee();

        public IEnumerable<Department> Departments { get; set; } = new List<Department>();

        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetDepartmentsAsync();

            Employee = await EmployeeService.GetEmployeeAsync(Id);

            Mapper.Map(Employee, EmployeeEditDTO);

            Departments = await DepartmentService.GetDepartmentsAsync();

        }

        protected void HandleValidEmployeeEditSubmit()
        {
            try
            {

                Mapper.Map(EmployeeEditDTO, Employee);

                var res = Employee;
                var res1 = EmployeeEditDTO;

                var result = EmployeeService.UpdateEmployeeAsync(Employee);

                if (result != null)
                {
                    NavigationManager.NavigateTo("/");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
