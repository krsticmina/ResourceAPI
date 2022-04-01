using AutoMapper;

namespace ResourceAPI.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Entities.Employee, Models.EmployeeDto>().ReverseMap();
            CreateMap<Models.EmployeeForInsertionDto, Entities.Employee>().ReverseMap();

        }

    }
}
