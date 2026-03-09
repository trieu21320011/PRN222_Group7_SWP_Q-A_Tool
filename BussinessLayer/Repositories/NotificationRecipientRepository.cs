using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class NotificationRecipientRepository : GenericRepository<NotificationRecipient>, INotificationRecipientRepository
    {
        public NotificationRecipientRepository(SWP391QAContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<NotificationRecipient>> GetRecipientsByNotificationAsync(int notificationId)
        {
            return await _dbContext.NotificationRecipients
                .Where(r => r.NotificationId == notificationId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<NotificationRecipient>> GetNotificationsByUserAsync(int userId)
        {
            return await _dbContext.NotificationRecipients
                .Where(r => r.UserId == userId)
                .Include(r => r.Notification)
                .OrderByDescending(r => r.Notification.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<NotificationRecipient>> GetUnreadNotificationsByUserAsync(int userId)
        {
            return await _dbContext.NotificationRecipients
                .Where(r => r.UserId == userId && r.IsRead == false)
                .Include(r => r.Notification)
                .OrderByDescending(r => r.Notification.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountByUserAsync(int userId)
        {
            return await _dbContext.NotificationRecipients
                .CountAsync(r => r.UserId == userId && r.IsRead == false);
        }
    }
}
