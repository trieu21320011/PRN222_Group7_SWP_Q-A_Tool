using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface IMeetingRepository : IGenericRepository<Meeting>
    {
        Task<Meeting?> GetMeetingWithDetailsAsync(int meetingId);
        Task<IEnumerable<Meeting>> GetMeetingsByOrganizerAsync(int organizerId);
        Task<IEnumerable<Meeting>> GetMeetingsByTeamAsync(int teamId);
        Task<IEnumerable<Meeting>> GetUpcomingMeetingsAsync();
        Task<IEnumerable<Meeting>> GetMeetingsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
