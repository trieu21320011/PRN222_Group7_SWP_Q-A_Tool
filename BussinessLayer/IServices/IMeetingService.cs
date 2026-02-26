using BussinessLayer.ViewModels.MeetingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface IMeetingService
    {
        Task<IEnumerable<GetMeetingDTO>> GetAllMeetingsAsync();
        Task<GetMeetingDTO?> GetMeetingByIdAsync(int id);
        Task<MeetingDTO> CreateMeetingAsync(CreateMeetingDTO createMeetingDTO);
        Task<MeetingDTO?> UpdateMeetingAsync(UpdateMeetingDTO updateMeetingDTO);
        Task<bool> DeleteMeetingAsync(int id);
        Task<IEnumerable<GetMeetingDTO>> GetMeetingsByOrganizerAsync(int organizerId);
        Task<IEnumerable<GetMeetingDTO>> GetMeetingsByTeamAsync(int teamId);
        Task<IEnumerable<GetMeetingDTO>> GetUpcomingMeetingsAsync();
        Task<IEnumerable<GetMeetingDTO>> GetMeetingsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
