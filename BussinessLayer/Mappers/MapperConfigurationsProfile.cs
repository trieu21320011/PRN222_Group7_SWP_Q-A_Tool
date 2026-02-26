using AutoMapper;
using BussinessLayer.ViewModels.MeetingDTOs;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.TagDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using BussinessLayer.ViewModels.UserDTOs;
using DataLayer.DataLayer;

namespace BussinessLayer.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            // User mappings
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
                .ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<User, GetUserDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
                .ReverseMap();

            // Meeting mappings
            CreateMap<Meeting, MeetingDTO>()
                .ForMember(dest => dest.OrganizerName, opt => opt.MapFrom(src => src.Organizer.FullName))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.TeamName : null))
                .ReverseMap();
            CreateMap<Meeting, CreateMeetingDTO>().ReverseMap();
            CreateMap<Meeting, UpdateMeetingDTO>().ReverseMap();
            CreateMap<Meeting, GetMeetingDTO>()
                .ForMember(dest => dest.OrganizerName, opt => opt.MapFrom(src => src.Organizer.FullName))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.TeamName : null))
                .ReverseMap();

            // Question mappings
            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.TeamName : null))
                .ReverseMap();
            CreateMap<Question, CreateQuestionDTO>().ReverseMap();
            CreateMap<Question, UpdateQuestionDTO>().ReverseMap();
            CreateMap<Question, GetQuestionDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.TeamName : null))
                .ReverseMap();

            // Tag mappings
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<Tag, CreateTagDTO>().ReverseMap();
            CreateMap<Tag, UpdateTagDTO>().ReverseMap();
            CreateMap<Tag, GetTagDTO>().ReverseMap();

            // Team mappings
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader != null ? src.Leader.FullName : null))
                .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.Mentor != null ? src.Mentor.FullName : null))
                .ReverseMap();
            CreateMap<Team, CreateTeamDTO>().ReverseMap();
            CreateMap<Team, UpdateTeamDTO>().ReverseMap();
            CreateMap<Team, GetTeamDTO>()
                .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader != null ? src.Leader.FullName : null))
                .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.Mentor != null ? src.Mentor.FullName : null))
                .ReverseMap();
        }
    }
}
