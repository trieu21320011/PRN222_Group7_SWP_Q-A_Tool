using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface INotificationRecipientRepository : IGenericRepository<NotificationRecipient>
    {
        Task<IEnumerable<NotificationRecipient>> GetRecipientsByNotificationAsync(int notificationId);
        Task<IEnumerable<NotificationRecipient>> GetNotificationsByUserAsync(int userId);
        Task<IEnumerable<NotificationRecipient>> GetUnreadNotificationsByUserAsync(int userId);
        Task<int> GetUnreadCountByUserAsync(int userId);
    }
}
