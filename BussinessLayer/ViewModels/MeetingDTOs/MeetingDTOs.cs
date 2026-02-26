using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.MeetingDTOs
{
    public class MeetingDTO
    {
        public int MeetingId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string MeetingType { get; set; } = null!;
        public int OrganizerId { get; set; }
        public string? OrganizerName { get; set; }
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? MeetingUrl { get; set; }
        public string? Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? TimeZone { get; set; }
        public int? MaxParticipants { get; set; }
        public int? CurrentParticipants { get; set; }
        public string? Status { get; set; }
        public bool? IsRecurring { get; set; }
        public string? RecurrencePattern { get; set; }
        public string? Color { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateMeetingDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string MeetingType { get; set; } = null!;
        public int OrganizerId { get; set; }
        public int? TeamId { get; set; }
        public string? MeetingUrl { get; set; }
        public string? Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? TimeZone { get; set; }
        public int? MaxParticipants { get; set; }
        public bool? IsRecurring { get; set; }
        public string? RecurrencePattern { get; set; }
        public string? Color { get; set; }
    }

    public class UpdateMeetingDTO
    {
        public int MeetingId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string MeetingType { get; set; } = null!;
        public int? TeamId { get; set; }
        public string? MeetingUrl { get; set; }
        public string? Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? TimeZone { get; set; }
        public int? MaxParticipants { get; set; }
        public string? Status { get; set; }
        public bool? IsRecurring { get; set; }
        public string? RecurrencePattern { get; set; }
        public string? Color { get; set; }
    }

    public class GetMeetingDTO
    {
        public int MeetingId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string MeetingType { get; set; } = null!;
        public string? OrganizerName { get; set; }
        public string? TeamName { get; set; }
        public string? MeetingUrl { get; set; }
        public string? Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? CurrentParticipants { get; set; }
        public int? MaxParticipants { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
