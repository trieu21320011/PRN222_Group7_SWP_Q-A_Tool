using AutoMapper;
using BussinessLayer.ViewModels.AnswerDTOs;
using BussinessLayer.ViewModels.CommentDTOs;
using BussinessLayer.ViewModels.CoreDTOs;
using BussinessLayer.ViewModels.QuestionDTOs;
using BussinessLayer.ViewModels.SemesterDTOs;
using BussinessLayer.ViewModels.TeamDTOs;
using BussinessLayer.ViewModels.TopicDTOs;
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

            // Question mappings
            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.TeamName : null))
                .ForMember(dest => dest.CoreName, opt => opt.MapFrom(src => src.Core != null ? src.Core.CoreName : null))
                .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic != null ? src.Topic.TopicName : null))
                .ForMember(dest => dest.AssignedInstructorName, opt => opt.MapFrom(src => src.AssignedInstructor != null ? src.AssignedInstructor.FullName : null))
                .ReverseMap();
            CreateMap<Question, CreateQuestionDTO>().ReverseMap();
            CreateMap<Question, UpdateQuestionDTO>().ReverseMap();
            CreateMap<Question, GetQuestionDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.TeamName : null))
                .ForMember(dest => dest.CoreName, opt => opt.MapFrom(src => src.Core != null ? src.Core.CoreName : null))
                .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic != null ? src.Topic.TopicName : null))
                .ForMember(dest => dest.AssignedInstructorName, opt => opt.MapFrom(src => src.AssignedInstructor != null ? src.AssignedInstructor.FullName : null))
                .ReverseMap();

            // Team mappings
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader != null ? src.Leader.FullName : null))
                .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.Mentor != null ? src.Mentor.FullName : null))
                .ForMember(dest => dest.CoreName, opt => opt.MapFrom(src => src.Core != null ? src.Core.CoreName : null))
                .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic != null ? src.Topic.TopicName : null))
                .ForMember(dest => dest.SemesterCode, opt => opt.MapFrom(src => src.SemesterNavigation != null ? src.SemesterNavigation.SemesterCode : null))
                .ReverseMap();
            CreateMap<Team, CreateTeamDTO>().ReverseMap();
            CreateMap<Team, UpdateTeamDTO>().ReverseMap();
            CreateMap<Team, GetTeamDTO>()
                .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader != null ? src.Leader.FullName : null))
                .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.Mentor != null ? src.Mentor.FullName : null))
                .ForMember(dest => dest.CoreName, opt => opt.MapFrom(src => src.Core != null ? src.Core.CoreName : null))
                .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic != null ? src.Topic.TopicName : null))
                .ForMember(dest => dest.SemesterCode, opt => opt.MapFrom(src => src.SemesterNavigation != null ? src.SemesterNavigation.SemesterCode : null))
                .ReverseMap();

            // Core mappings
            CreateMap<Core, CoreDTO>()
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName))
                .ForMember(dest => dest.SemesterCode, opt => opt.MapFrom(src => src.Semester.SemesterCode))
                .ReverseMap();
            CreateMap<Core, CreateCoreDTO>().ReverseMap();
            CreateMap<Core, UpdateCoreDTO>().ReverseMap();
            CreateMap<Core, GetCoreDTO>()
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName))
                .ForMember(dest => dest.SemesterCode, opt => opt.MapFrom(src => src.Semester.SemesterCode))
                .ReverseMap();

            // Topic mappings
            CreateMap<Topic, TopicDTO>()
                .ForMember(dest => dest.SemesterCode, opt => opt.MapFrom(src => src.Semester.SemesterCode))
                .ReverseMap();
            CreateMap<Topic, CreateTopicDTO>().ReverseMap();
            CreateMap<Topic, UpdateTopicDTO>().ReverseMap();
            CreateMap<Topic, GetTopicDTO>()
                .ForMember(dest => dest.SemesterCode, opt => opt.MapFrom(src => src.Semester.SemesterCode))
                .ReverseMap();

            // Semester mappings
            CreateMap<Semester, SemesterDTO>().ReverseMap();
            CreateMap<Semester, CreateSemesterDTO>().ReverseMap();
            CreateMap<Semester, UpdateSemesterDTO>().ReverseMap();
            CreateMap<Semester, GetSemesterDTO>().ReverseMap();

            // Answer mappings
            CreateMap<Answer, AnswerDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ForMember(dest => dest.QuestionTitle, opt => opt.MapFrom(src => src.Question.Title))
                .ReverseMap();
            CreateMap<Answer, CreateAnswerDTO>().ReverseMap();
            CreateMap<Answer, UpdateAnswerDTO>().ReverseMap();
            CreateMap<Answer, GetAnswerDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ForMember(dest => dest.QuestionTitle, opt => opt.MapFrom(src => src.Question.Title))
                .ReverseMap();

            // Comment mappings
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ReverseMap();
            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
            CreateMap<Comment, GetCommentDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ReverseMap();
        }
    }
}
