using AutoMapper;
using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeEditDTO>();
            CreateMap<EmployeeEditDTO, Employee>();
            CreateMap<EmployeeCreateDTO, Employee>();

             /* .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id))*/
        }
    }
}
