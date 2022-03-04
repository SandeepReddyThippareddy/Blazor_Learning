using AutoMapper;
using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.DTO_s;
using EmployeeManagement.Web.Repositories.EmployeeRepo.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeAddBase : ComponentBase
    {
        [Inject]
        public IEmployeeRepositoryService EmployeeService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public EmployeeCreateDTO EmployeeCreateDTO { get; set; } = new EmployeeCreateDTO();
        public Employee Employee { get; set; } = new Employee();

        protected async void HandleValidEmployeeAddSubmit()
        {
            Mapper.Map(EmployeeCreateDTO, Employee);

            var result = await EmployeeService.CreateEmployeeAsync(Employee);

            if(result != null)
            {
                NavigationManager.NavigateTo($"/EmployeeDetails/{result.EmployeeId}");
            }
        }
    }
}
