using AutoMapper;
using StaffServiceCore.Models;
using StaffServiceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<EmployeeForInsertionDto, Employee>().ReverseMap();
        }
    }
}
