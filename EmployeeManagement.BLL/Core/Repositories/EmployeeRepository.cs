using Microsoft.Extensions.Logging;
using WebAPI.BLL.Core.IRepositories;
using WebAPI.BLL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Text;
using EmployeeManagement.Models.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.BLL.Core.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSetting _jwtSetting;

        public EmployeeRepository(UserManager<ApplicationUser> userManager, JwtSetting options, WebApiContext context, ILogger logger) : base(context, logger)
        {
            _userManager = userManager;
            _jwtSetting = options;
        }

        public override async Task<Employee> GetById(string id)
        {
            return await _dbSet.Include(x => x.Department).FirstOrDefaultAsync( x1 => x1.EmployeeId == Int32.Parse(id));
        }
        public override async Task<IEnumerable<Employee>> All()
        {
            return await _dbSet.Include(x => x.Department).ToListAsync();
        }
        public async Task<ApplicationUser> AuthenticateUser(UserCred userCred)
        {
            var user = await _userManager.FindByEmailAsync(userCred.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, userCred.Password))
            {
                return new ApplicationUser()
                {
                    UserName = userCred.UserName,
                    Id  = user.Id,
                    BearerToken = GenerateAccessToken(user.Id)
                };
            }
            else
            {
                return new ApplicationUser();
            }

        }

        private string GenerateAccessToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetting.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string DownloadDataFromAzure(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationUser> GetUserData(string userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Employee> GetUserData(CursorParams cursorParams, out int? nextCursor)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UploadDataToAzure(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> UpdateEmployee(Employee entity)
        {
            var existingUser = await _dbSet.Where(x => x.EmployeeId == entity.EmployeeId).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                if (await Add(existingUser) != null)
                    return true;
                else
                    return false;
            }
            else
            {
                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                existingUser.PhotoPath = entity.PhotoPath;

                _dbSet.Update(existingUser);
                return true;

            }
            

        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {

            if (!string.IsNullOrEmpty(name))
            {
                return await _dbSet.Where(e => e.FirstName.Contains(name)
                            || e.LastName.Contains(name)).ToListAsync();
            }

            if (gender != null)
            {
                return await _dbSet.Where(e => e.Gender == gender).ToListAsync();
            }

            return null;
        }
    }
}