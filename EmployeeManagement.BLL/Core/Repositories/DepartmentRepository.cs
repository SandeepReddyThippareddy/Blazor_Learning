using EmployeeManagement.BLL.Core.IRepositories;
using EmployeeManagement.Models.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Core.Repositories;
using WebAPI.BLL.Data;

namespace EmployeeManagement.BLL.Core.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(WebApiContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}
