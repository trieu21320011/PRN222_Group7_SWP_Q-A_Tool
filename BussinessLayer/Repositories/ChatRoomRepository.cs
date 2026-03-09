using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class ChatRoomRepository : GenericRepository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(SWP391QAContext dbContext) : base(dbContext) { }

        public async Task<ChatRoom?> GetChatRoomWithDetailsAsync(int chatRoomId)
        {
            return await _dbContext.ChatRooms
                .Include(r => r.Team)
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(r => r.ChatRoomId == chatRoomId);
        }

        public async Task<IEnumerable<ChatRoom>> GetChatRoomsByTeamAsync(int teamId)
        {
            return await _dbContext.ChatRooms
                .Where(r => r.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatRoom>> GetChatRoomsByCreatorAsync(int creatorId)
        {
            return await _dbContext.ChatRooms
                .Where(r => r.CreatedById == creatorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatRoom>> GetActiveChatRoomsAsync()
        {
            return await _dbContext.ChatRooms
                .Where(r => r.IsActive == true)
                .ToListAsync();
        }
    }
}
