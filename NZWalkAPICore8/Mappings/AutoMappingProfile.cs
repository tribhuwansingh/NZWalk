using AutoMapper;
using NZWalkAPICore8.Model.Domain;
using NZWalkAPICore8.Model.DTO;

namespace NZWalkAPICore8.Mappings
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile() 
        {
            CreateMap<Region ,RegionDto>().ReverseMap();
            CreateMap<AddRegionDto,Region>().ReverseMap();
            CreateMap<UpdateRegionDto,Region>().ReverseMap();

            //Source to Destination (Reverse Map : Destination to Source)
            CreateMap<AddWalkDto,Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DiffercultyDto>().ReverseMap();
            CreateMap<UpdateWalkDto, Walk>().ReverseMap();

        }

    }
}
