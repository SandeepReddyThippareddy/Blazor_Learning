using EmployeeManagement.Models.Models;
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

        [Parameter]
        public string Id { get; set; }


        public Employee Employee { get; set; } = new Employee();

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployeeAsync(Id);
        }
    }
}
