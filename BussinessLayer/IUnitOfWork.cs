using BussinessLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepo { get; }
        public IMeetingRepository MeetingRepo { get; }
        public IQuestionRepository QuestionRepo { get; }
        public ITagRepository TagRepo { get; }
        public ITeamRepository TeamRepo { get; }
        public IRoleRepository RoleRepo { get; }
        public IAnswerRepository AnswerRepo { get; }
        public ICommentRepository CommentRepo { get; }
        public Task<int> SaveChangeAsync();
    }
}
