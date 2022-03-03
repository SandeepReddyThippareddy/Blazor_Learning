using EmployeeManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repositories.DepartmentRepo.Interfaces
{
    public interface IDepartmentRepositoryService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentAsync(string Id);
    }
}
