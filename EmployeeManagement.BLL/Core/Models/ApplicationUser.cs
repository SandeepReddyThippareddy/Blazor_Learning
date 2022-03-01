using Microsoft.AspNetCore.Identity;
using System;

namespace EmployeeManagement.Models.Models
{
    [Serializable]
    public class ApplicationUser : IdentityUser
    {
        public string BearerToken { get; set; }
    }

}
