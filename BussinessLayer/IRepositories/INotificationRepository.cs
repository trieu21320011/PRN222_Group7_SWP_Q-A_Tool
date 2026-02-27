using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<Notification?> GetNotificationWithDetailsAsync(int notificationId);
        Task<IEnumerable<Notification>> GetNotificationsByCreatorAsync(int creatorId);
        Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(string notificationType);
    }
}
