using System;
using System.Collections.Generic;

namespace DataLayer.DataLayer;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Bio { get; set; }

    public string? StudentId { get; set; }

    public int RoleId { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsEmailVerified { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Core> Cores { get; set; } = new List<Core>();

    public virtual ICollection<NotificationRecipient> NotificationRecipients { get; set; } = new List<NotificationRecipient>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Question> QuestionAssignedInstructors { get; set; } = new List<Question>();

    public virtual ICollection<Question> QuestionAuthors { get; set; } = new List<Question>();

    public virtual ICollection<Question> QuestionClosedBies { get; set; } = new List<Question>();

    public virtual ICollection<QuestionFollower> QuestionFollowers { get; set; } = new List<QuestionFollower>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SystemInstructor> SystemInstructors { get; set; } = new List<SystemInstructor>();

    public virtual ICollection<Team> TeamLeaders { get; set; } = new List<Team>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public virtual ICollection<Team> TeamMentors { get; set; } = new List<Team>();
}
