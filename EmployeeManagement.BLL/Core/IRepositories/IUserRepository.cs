using EmployeeManagement.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WebAPI.BLL.Data;

namespace WebAPI.BLL.Core.IRepositories
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
         //You can put the operations spesific to user here

         Task<string> GetEmail(string id);
    }
}