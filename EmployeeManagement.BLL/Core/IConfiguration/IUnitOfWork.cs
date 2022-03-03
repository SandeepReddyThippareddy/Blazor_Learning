using EmployeeManagement.BLL.Core.IRepositories;
using System.Threading.Tasks;
using WebAPI.BLL.Core.IRepositories;

namespace WebAPI.BLL.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}
        IEmployeeRepository EmployeeRepository {get;}
        IDepartmentRepository DepartmentRepository { get;}
        bool Complete();
        Task<bool> CompleteAsync();
    }
}