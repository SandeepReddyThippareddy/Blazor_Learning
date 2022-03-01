﻿using EmployeeManagement.Web.Repositories.EmployeeRepo.Interfaces;
using Microsoft.AspNetCore.Components;
using EmployeeManagement.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeRepositoryService EmployeeService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Employees = (IEnumerable<Employee>)(await EmployeeService.GetEmployeesAsync()).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
