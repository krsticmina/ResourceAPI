using AutoMapper;
using StaffServiceBLL.Models;
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

            CreateMap<EmployeeModel, Employee>().ReverseMap();
            CreateMap<EmployeeForUpdateModel, Employee>().ReverseMap();
            CreateMap<EmployeeForInsertionModel, Employee>().ReverseMap();
        }
    }
}
