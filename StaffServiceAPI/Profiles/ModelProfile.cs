using AutoMapper;
using StaffServiceAPI.Models;
using StaffServiceBLL.Models;

namespace StaffServiceAPI.Profiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile() 
        {
            CreateMap<StaffServiceAPI.Models.EmployeeDto, StaffServiceBLL.Models.EmployeeModel>().ReverseMap();
            CreateMap<StaffServiceAPI.Models.EmployeeForInsertionDto, StaffServiceBLL.Models.EmployeeForInsertionModel>().ReverseMap();
            CreateMap<StaffServiceAPI.Models.EmployeeForUpdateDto, StaffServiceBLL.Models.EmployeeForUpdateModel>().ReverseMap();
            CreateMap<StaffServiceBLL.Models.EmployeeModel, StaffServiceAPI.Models.EmployeeForUpdateDto>().ReverseMap();
        
        }
    }
}
