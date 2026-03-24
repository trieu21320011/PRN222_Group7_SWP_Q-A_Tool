using BussinessLayer.IServices;
using BussinessLayer.ViewModels.ChatDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace SWP_QA_TOOL.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private static readonly Dictionary<int, string> _userConnections = new();

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetCurrentUserId();
            if (userId > 0)
            {
                _userConnections[userId] = Context.ConnectionId;
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetCurrentUserId();
            if (userId > 0)
            {
                _userConnections.Remove(userId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
            }
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Join a specific chat room
        /// </summary>
        public async Task JoinRoom(int chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"room_{chatRoomId}");
        }

        /// <summary>
        /// Leave a specific chat room
        /// </summary>
        public async Task LeaveRoom(int chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"room_{chatRoomId}");
        }

        /// <summary>
        /// Send a message to a chat room
        /// </summary>
        public async Task SendMessage(int chatRoomId, string messageText, string? messageType = "Text", int? replyToMessageId = null)
        {
            var userId = GetCurrentUserId();
            if (userId <= 0) return;

            var sendDto = new SendMessageDTO
            {
                ChatRoomId = chatRoomId,
                SenderId = userId,
                MessageText = messageText,
                MessageType = messageType ?? "Text",
                ReplyToMessageId = replyToMessageId
            };

            var message = await _chatService.SendMessageAsync(sendDto);

            // Send to all users in the room
            await Clients.Group($"room_{chatRoomId}").SendAsync("ReceiveMessage", message);

            // Also notify the other user if they're not in the room
            var room = await _chatService.GetChatRoomByIdAsync(chatRoomId, userId);
            if (room?.OtherUserId != null)
            {
                await Clients.Group($"user_{room.OtherUserId}").SendAsync("NewMessageNotification", new
                {
                    ChatRoomId = chatRoomId,
                    SenderName = message.SenderName,
                    MessagePreview = messageText.Length > 50 ? messageText.Substring(0, 50) + "..." : messageText
                });
            }
        }

        /// <summary>
        /// Start or get existing chat with a user
        /// </summary>
        public async Task<ChatRoomDTO?> StartOrGetChat(int otherUserId)
        {
            var userId = GetCurrentUserId();
            if (userId <= 0) return null;

            var chatRoom = await _chatService.GetOrCreateDirectChatRoomAsync(userId, otherUserId);
            if (chatRoom != null)
            {
                await JoinRoom(chatRoom.ChatRoomId);
            }
            return chatRoom;
        }

        /// <summary>
        /// Get messages for a chat room
        /// </summary>
        public async Task<IEnumerable<ChatMessageDTO>> GetMessages(int chatRoomId)
        {
            var userId = GetCurrentUserId();
            if (userId <= 0) return Enumerable.Empty<ChatMessageDTO>();

            return await _chatService.GetMessagesByChatRoomAsync(chatRoomId, userId);
        }

        /// <summary>
        /// Delete a message
        /// </summary>
        public async Task DeleteMessage(int messageId, int chatRoomId)
        {
            var userId = GetCurrentUserId();
            if (userId <= 0) return;

            var result = await _chatService.DeleteMessageAsync(messageId, userId);
            if (result)
            {
                await Clients.Group($"room_{chatRoomId}").SendAsync("MessageDeleted", messageId);
            }
        }

        /// <summary>
        /// Edit a message
        /// </summary>
        public async Task EditMessage(int messageId, string newText, int chatRoomId)
        {
            var userId = GetCurrentUserId();
            if (userId <= 0) return;

            var message = await _chatService.EditMessageAsync(messageId, newText, userId);
            if (message != null)
            {
                await Clients.Group($"room_{chatRoomId}").SendAsync("MessageEdited", message);
            }
        }

        /// <summary>
        /// Typing indicator
        /// </summary>
        public async Task SendTyping(int chatRoomId)
        {
            var userId = GetCurrentUserId();
            var userName = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            
            await Clients.OthersInGroup($"room_{chatRoomId}").SendAsync("UserTyping", new
            {
                UserId = userId,
                UserName = userName
            });
        }

        /// <summary>
        /// Stop typing indicator
        /// </summary>
        public async Task StopTyping(int chatRoomId)
        {
            var userId = GetCurrentUserId();
            await Clients.OthersInGroup($"room_{chatRoomId}").SendAsync("UserStoppedTyping", userId);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }
    }
}
