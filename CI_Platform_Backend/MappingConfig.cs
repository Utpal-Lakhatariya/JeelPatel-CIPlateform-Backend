
using AutoMapper;
using CI_Platform.Models;
using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using Microsoft.OpenApi.Writers;
using System.Reflection;

namespace CI_Platform_Backend
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<User, SignupRequestModel>().ReverseMap();
            CreateMap<CreateMissionRequestModel, Mission>().ReverseMap();
            CreateMap<CreateMissionRequestModel, MissionMedia>().ReverseMap();
            CreateMap<MissionResponseSubModel, Mission>().ReverseMap()
                .ForMember(src => src.City, opt => opt.MapFrom(des => des.City!=null ? des.City.CityName : null))
                .ForMember(src => src.Country, opt => opt.MapFrom(des => des.Country!=null ? des.Country.CountryName: null));
            CreateMap<MissionMediaResponseSubModel, MissionMedia>().ReverseMap();
            //CreateMap<DropdownResponseModel, City>()
            //    .ForMember(src => src.CityId, opt => opt.MapFrom(des => des.Value))
            //    .ForMember(src => src.CityName, opt => opt.MapFrom(des => des.Text)).ReverseMap();
            //CreateMap<DropdownResponseModel, Country>()
            //    .ForMember(src => src.CountryId, opt => opt.MapFrom(des => des.Value))
            //    .ForMember(src => src.CountryName, opt => opt.MapFrom(des => des.Text)).ReverseMap();
            //CreateMap<DropdownResponseModel, Skill>()
            //    .ForMember(src => src.SkillId, opt => opt.MapFrom(des => des.Value))
            //    .ForMember(src => src.Skills, opt => opt.MapFrom(des => des.Text)).ReverseMap();
           
        }
    }
}
