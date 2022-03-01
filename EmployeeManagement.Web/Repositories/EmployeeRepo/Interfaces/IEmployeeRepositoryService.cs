using EmployeeManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repositories.EmployeeRepo.Interfaces
{
    public interface IEmployeeRepositoryService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(string Id);
    }
}
