using AutoMapper;
using ExportToExcel.Models;

namespace AuotMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest => dest.HiringDate, src => src.MapFrom(src => src.HiringDate.ToString("MM.dd.yyyy")))
                 .ReverseMap();
            
                 
        }
    }
}