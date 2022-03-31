using AutoMapper;

namespace ResourceAPI.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Entities.Employee, Models.EmployeeDto>();
            CreateMap<Models.EmployeeDto, Entities.Employee>();
            CreateMap<Models.EmployeeForInsertionDto, Entities.Employee>().ReverseMap();

        }

    }
}
