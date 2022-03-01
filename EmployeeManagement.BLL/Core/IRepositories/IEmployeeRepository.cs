using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Models.Models;
using Microsoft.AspNetCore.Identity;
using WebAPI.BLL.Data;

namespace WebAPI.BLL.Core.IRepositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<ApplicationUser> AuthenticateUser(UserCred userCred);
        Task<bool> UploadDataToAzure(string userId);
        string DownloadDataFromAzure(string userId);
        Task<ApplicationUser> GetUserData(string userId);

        IEnumerable<Employee> GetUserData(CursorParams cursorParams, out int? nextCursor);

        Task<Employee> GetEmployeeByEmail(string email);

        Task<bool> UpdateEmployee(Employee entity);

        Task<IEnumerable<Employee>> Search(string name, Gender? gender);
    }
}