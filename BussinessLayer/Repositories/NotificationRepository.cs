using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(SWP391QAContext dbContext) : base(dbContext) { }

        public async Task<Notification?> GetNotificationWithDetailsAsync(int notificationId)
        {
            return await _dbContext.Notifications
                .Include(n => n.CreatedBy)
                .Include(n => n.NotificationRecipients)
                .FirstOrDefaultAsync(n => n.NotificationId == notificationId);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByCreatorAsync(int creatorId)
        {
            return await _dbContext.Notifications
                .Where(n => n.CreatedById == creatorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(string notificationType)
        {
            return await _dbContext.Notifications
                .Where(n => n.NotificationType == notificationType)
                .ToListAsync();
        }
    }
}
