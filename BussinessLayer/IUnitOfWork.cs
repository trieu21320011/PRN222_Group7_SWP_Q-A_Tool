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
        // User & Role
        public IUserRepository UserRepo { get; }
        public IRoleRepository RoleRepo { get; }

        // Question & Answer
        public IQuestionRepository QuestionRepo { get; }
        public IAnswerRepository AnswerRepo { get; }
        public ICommentRepository CommentRepo { get; }
        public IQuestionFollowerRepository QuestionFollowerRepo { get; }

        // Team
        public ITeamRepository TeamRepo { get; }
        public ITeamMemberRepository TeamMemberRepo { get; }

        // Core, Topic & Semester
        public ICoreRepository CoreRepo { get; }
        public ITopicRepository TopicRepo { get; }
        public ISemesterRepository SemesterRepo { get; }
        public ISystemInstructorRepository SystemInstructorRepo { get; }

        // Chat
        public IChatRoomRepository ChatRoomRepo { get; }
        public IChatMessageRepository ChatMessageRepo { get; }

        // Notification
        public INotificationRepository NotificationRepo { get; }
        public INotificationRecipientRepository NotificationRecipientRepo { get; }

        public Task<int> SaveChangeAsync();
    }
}
