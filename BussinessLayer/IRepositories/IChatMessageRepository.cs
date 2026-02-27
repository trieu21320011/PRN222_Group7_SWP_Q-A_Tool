using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface IChatMessageRepository : IGenericRepository<ChatMessage>
    {
        Task<ChatMessage?> GetChatMessageWithDetailsAsync(int chatMessageId);
        Task<IEnumerable<ChatMessage>> GetMessagesByChatRoomAsync(int chatRoomId);
        Task<IEnumerable<ChatMessage>> GetMessagesBySenderAsync(int senderId);
        Task<IEnumerable<ChatMessage>> GetRecentMessagesAsync(int chatRoomId, int count);
    }
}
