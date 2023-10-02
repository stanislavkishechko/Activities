using AutoMapper;
using DAL.Domain.Entities;

namespace BLL.Mapping
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile()
        {
            CreateMap<Activity, Activity>();
        }
    }
}
