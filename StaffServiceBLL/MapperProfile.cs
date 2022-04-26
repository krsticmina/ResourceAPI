using AutoMapper;
using StaffServiceBLL.Models;
using StaffServiceDAL.Entities;

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
