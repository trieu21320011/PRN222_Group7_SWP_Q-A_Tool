using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface IChatRoomRepository : IGenericRepository<ChatRoom>
    {
        Task<ChatRoom?> GetChatRoomWithDetailsAsync(int chatRoomId);
        Task<IEnumerable<ChatRoom>> GetChatRoomsByTeamAsync(int teamId);
        Task<IEnumerable<ChatRoom>> GetChatRoomsByCreatorAsync(int creatorId);
        Task<IEnumerable<ChatRoom>> GetActiveChatRoomsAsync();
    }
}
