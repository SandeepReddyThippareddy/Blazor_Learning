using System;
using System.Threading.Tasks;
using EmployeeManagement.BLL.Core.IRepositories;
using EmployeeManagement.BLL.Core.Repositories;
using EmployeeManagement.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI.BLL.Core.IConfiguration;
using WebAPI.BLL.Core.IRepositories;
using WebAPI.BLL.Core.Repositories;

namespace WebAPI.BLL.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly WebApiContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSetting _jwtSetting;
        public IUserRepository UserRepository { get; private set; }

        public IEmployeeRepository EmployeeRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository  { get; private set; }

        public UnitOfWork(UserManager<ApplicationUser> userManager, IOptions<JwtSetting> options, WebApiContext webApiContext, ILoggerFactory logger)
        {
            _context = webApiContext;
            _logger = logger.CreateLogger("Logs");
            _userManager = userManager;
            _jwtSetting = options.Value;
            UserRepository = new UserRepository(_context, _logger);
            EmployeeRepository = new EmployeeRepository(_userManager, _jwtSetting, _context, _logger);
            DepartmentRepository = new DepartmentRepository(_context, _logger);
        }

        //Another way of instantiating the repositories.
        //public IUserRepository UserRepository => new UserRepository(_context, _logger);
        public bool Complete()
        {
           return _context.SaveChanges() > 0;
        }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}