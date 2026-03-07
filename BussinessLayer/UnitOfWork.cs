using BussinessLayer.IRepositories;
using BussinessLayer.Repositories;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SWP391QAContext _context;

        public UnitOfWork(SWP391QAContext context)
        {
            _context = context;
        }

        private IUserRepository _userRepo;
        public IUserRepository UserRepo => _userRepo ??= new UserRepository(_context);

        private IRoleRepository _roleRepo;
        public IRoleRepository RoleRepo => _roleRepo ??= new RoleRepository(_context);

        private IQuestionRepository _questionRepo;
        public IQuestionRepository QuestionRepo => _questionRepo ??= new QuestionRepository(_context);

        private IAnswerRepository _answerRepo;
        public IAnswerRepository AnswerRepo => _answerRepo ??= new AnswerRepository(_context);

        private ICommentRepository _commentRepo;
        public ICommentRepository CommentRepo => _commentRepo ??= new CommentRepository(_context);

        private IQuestionFollowerRepository _questionFollowerRepo;
        public IQuestionFollowerRepository QuestionFollowerRepo => _questionFollowerRepo ??= new QuestionFollowerRepository(_context);

        private ITeamRepository _teamRepo;
        public ITeamRepository TeamRepo => _teamRepo ??= new TeamRepository(_context);

        private ITeamMemberRepository _teamMemberRepo;
        public ITeamMemberRepository TeamMemberRepo => _teamMemberRepo ??= new TeamMemberRepository(_context);

        private ICoreRepository _coreRepo;
        public ICoreRepository CoreRepo => _coreRepo ??= new CoreRepository(_context);

        private ITopicRepository _topicRepo;
        public ITopicRepository TopicRepo => _topicRepo ??= new TopicRepository(_context);

        private ISemesterRepository _semesterRepo;
        public ISemesterRepository SemesterRepo => _semesterRepo ??= new SemesterRepository(_context);

        private ISystemInstructorRepository _systemInstructorRepo;
        public ISystemInstructorRepository SystemInstructorRepo => _systemInstructorRepo ??= new SystemInstructorRepository(_context);

        private IChatRoomRepository _chatRoomRepo;
        public IChatRoomRepository ChatRoomRepo => _chatRoomRepo ??= new ChatRoomRepository(_context);

        private IChatMessageRepository _chatMessageRepo;
        public IChatMessageRepository ChatMessageRepo => _chatMessageRepo ??= new ChatMessageRepository(_context);

        private INotificationRepository _notificationRepo;
        public INotificationRepository NotificationRepo => _notificationRepo ??= new NotificationRepository(_context);

        private INotificationRecipientRepository _notificationRecipientRepo;
        public INotificationRecipientRepository NotificationRecipientRepo => _notificationRecipientRepo ??= new NotificationRecipientRepository(_context);

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
