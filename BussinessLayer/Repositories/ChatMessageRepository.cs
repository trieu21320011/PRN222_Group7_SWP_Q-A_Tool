using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(SWP391QAContext dbContext) : base(dbContext) { }

        public async Task<ChatMessage?> GetChatMessageWithDetailsAsync(int chatMessageId)
        {
            return await _dbContext.ChatMessages
                .Include(m => m.Sender)
                .Include(m => m.ChatRoom)
                .FirstOrDefaultAsync(m => m.ChatMessageId == chatMessageId);
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesByChatRoomAsync(int chatRoomId)
        {
            return await _dbContext.ChatMessages
                .Where(m => m.ChatRoomId == chatRoomId)
                .Include(m => m.Sender)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesBySenderAsync(int senderId)
        {
            return await _dbContext.ChatMessages
                .Where(m => m.SenderId == senderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetRecentMessagesAsync(int chatRoomId, int count)
        {
            return await _dbContext.ChatMessages
                .Where(m => m.ChatRoomId == chatRoomId)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.CreatedAt)
                .Take(count)
                .Reverse()
                .ToListAsync();
        }
    }
}
