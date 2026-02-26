using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DataLayer;

public partial class SWP391QAContext : DbContext
{
    public SWP391QAContext()
    {
    }

    public SWP391QAContext(DbContextOptions<SWP391QAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<AnswerVote> AnswerVotes { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentVote> CommentVotes { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<MeetingParticipant> MeetingParticipants { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationRecipient> NotificationRecipients { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionFollower> QuestionFollowers { get; set; }

    public virtual DbSet<QuestionTag> QuestionTags { get; set; }

    public virtual DbSet<QuestionVote> QuestionVotes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagFollower> TagFollowers { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8O97R1M;Database=SWP391_QA;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answers__D48250042C1AA718");

            entity.HasIndex(e => e.AuthorId, "IX_Answers_AuthorId");

            entity.HasIndex(e => e.IsAccepted, "IX_Answers_IsAccepted");

            entity.HasIndex(e => e.QuestionId, "IX_Answers_QuestionId");

            entity.Property(e => e.CommentCount).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DownvoteCount).HasDefaultValue(0);
            entity.Property(e => e.IsAccepted).HasDefaultValue(false);
            entity.Property(e => e.IsAigenerated)
                .HasDefaultValue(false)
                .HasColumnName("IsAIGenerated");
            entity.Property(e => e.IsMentorAnswer).HasDefaultValue(false);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpvoteCount).HasDefaultValue(0);

            entity.HasOne(d => d.Author).WithMany(p => p.Answers)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_Author");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Answers_Question");
        });

        modelBuilder.Entity<AnswerVote>(entity =>
        {
            entity.HasKey(e => e.AnswerVoteId).HasName("PK__AnswerVo__DC313A2F5CA0CAC6");

            entity.HasIndex(e => new { e.AnswerId, e.UserId }, "UQ_AnswerVotes").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Answer).WithMany(p => p.AnswerVotes)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_AnswerVotes_Answer");

            entity.HasOne(d => d.User).WithMany(p => p.AnswerVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnswerVotes_User");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.ChatMessageId).HasName("PK__ChatMess__9AB61035786BA3FD");

            entity.HasIndex(e => e.ChatRoomId, "IX_ChatMessages_ChatRoomId");

            entity.HasIndex(e => e.CreatedAt, "IX_ChatMessages_CreatedAt").IsDescending();

            entity.Property(e => e.AttachmentUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsEdited).HasDefaultValue(false);
            entity.Property(e => e.MessageType)
                .HasMaxLength(20)
                .HasDefaultValue("Text");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ChatRoom).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.ChatRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatMessages_Room");

            entity.HasOne(d => d.ReplyToMessage).WithMany(p => p.InverseReplyToMessage)
                .HasForeignKey(d => d.ReplyToMessageId)
                .HasConstraintName("FK_ChatMessages_Reply");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatMessages_Sender");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.ChatRoomId).HasName("PK__ChatRoom__69733CF7EE02C3E5");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoomName).HasMaxLength(100);
            entity.Property(e => e.RoomType).HasMaxLength(20);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.ChatRooms)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatRooms_Creator");

            entity.HasOne(d => d.Team).WithMany(p => p.ChatRooms)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_ChatRooms_Team");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCA43984B44");

            entity.HasIndex(e => e.AnswerId, "IX_Comments_AnswerId");

            entity.HasIndex(e => e.AuthorId, "IX_Comments_AuthorId");

            entity.HasIndex(e => e.QuestionId, "IX_Comments_QuestionId");

            entity.Property(e => e.Body).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsEdited).HasDefaultValue(false);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpvoteCount).HasDefaultValue(0);

            entity.HasOne(d => d.Answer).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_Comments_Answer");

            entity.HasOne(d => d.Author).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Author");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK_Comments_Parent");

            entity.HasOne(d => d.Question).WithMany(p => p.Comments)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Comments_Question");
        });

        modelBuilder.Entity<CommentVote>(entity =>
        {
            entity.HasKey(e => e.CommentVoteId).HasName("PK__CommentV__B8446E76D1709949");

            entity.HasIndex(e => new { e.CommentId, e.UserId }, "UQ_CommentVotes").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.VoteType).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentVotes)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK_CommentVotes_Comment");

            entity.HasOne(d => d.User).WithMany(p => p.CommentVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommentVotes_User");
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => e.MeetingId).HasName("PK__Meetings__E9F9E94C1A02EE61");

            entity.HasIndex(e => e.OrganizerId, "IX_Meetings_OrganizerId");

            entity.HasIndex(e => e.StartTime, "IX_Meetings_StartTime");

            entity.HasIndex(e => e.Status, "IX_Meetings_Status");

            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasDefaultValue("#0078d4");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CurrentParticipants).HasDefaultValue(0);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsRecurring).HasDefaultValue(false);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.MeetingType).HasMaxLength(50);
            entity.Property(e => e.MeetingUrl).HasMaxLength(500);
            entity.Property(e => e.RecurrencePattern).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Scheduled");
            entity.Property(e => e.TimeZone)
                .HasMaxLength(50)
                .HasDefaultValue("Asia/Ho_Chi_Minh");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Meetings)
                .HasForeignKey(d => d.OrganizerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meetings_Organizer");

            entity.HasOne(d => d.Team).WithMany(p => p.Meetings)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Meetings_Team");
        });

        modelBuilder.Entity<MeetingParticipant>(entity =>
        {
            entity.HasKey(e => e.MeetingParticipantId).HasName("PK__MeetingP__3D3422E6BD983E56");

            entity.HasIndex(e => new { e.MeetingId, e.UserId }, "UQ_MeetingParticipants").IsUnique();

            entity.Property(e => e.RegisteredAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Registered");

            entity.HasOne(d => d.Meeting).WithMany(p => p.MeetingParticipants)
                .HasForeignKey(d => d.MeetingId)
                .HasConstraintName("FK_MeetingParticipants_Meeting");

            entity.HasOne(d => d.User).WithMany(p => p.MeetingParticipants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeetingParticipants_User");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12F98A19D9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.NotificationType).HasMaxLength(50);
            entity.Property(e => e.ReferenceType).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_Notifications_Creator");
        });

        modelBuilder.Entity<NotificationRecipient>(entity =>
        {
            entity.HasKey(e => e.NotificationRecipientId).HasName("PK__Notifica__F6659EE494940E02");

            entity.HasIndex(e => e.IsRead, "IX_NotificationRecipients_IsRead");

            entity.HasIndex(e => e.UserId, "IX_NotificationRecipients_UserId");

            entity.Property(e => e.IsEmailSent).HasDefaultValue(false);
            entity.Property(e => e.IsRead).HasDefaultValue(false);

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationRecipients)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK_NotificationRecipients_Notification");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationRecipients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationRecipients_User");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FACA1D76904");

            entity.HasIndex(e => e.AuthorId, "IX_Questions_AuthorId");

            entity.HasIndex(e => e.Category, "IX_Questions_Category");

            entity.HasIndex(e => e.CreatedAt, "IX_Questions_CreatedAt").IsDescending();

            entity.HasIndex(e => e.LastActivityAt, "IX_Questions_LastActivityAt").IsDescending();

            entity.HasIndex(e => e.Status, "IX_Questions_Status");

            entity.HasIndex(e => e.ViewCount, "IX_Questions_ViewCount").IsDescending();

            entity.Property(e => e.AnswerCount).HasDefaultValue(0);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.ClosedReason).HasMaxLength(255);
            entity.Property(e => e.CommentCount).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Difficulty).HasMaxLength(20);
            entity.Property(e => e.DownvoteCount).HasDefaultValue(0);
            entity.Property(e => e.Excerpt).HasMaxLength(500);
            entity.Property(e => e.IsClosed).HasDefaultValue(false);
            entity.Property(e => e.IsPinned).HasDefaultValue(false);
            entity.Property(e => e.LastActivityAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Open");
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpvoteCount).HasDefaultValue(0);
            entity.Property(e => e.ViewCount).HasDefaultValue(0);

            entity.HasOne(d => d.Author).WithMany(p => p.QuestionAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Author");

            entity.HasOne(d => d.ClosedBy).WithMany(p => p.QuestionClosedBies)
                .HasForeignKey(d => d.ClosedById)
                .HasConstraintName("FK_Questions_ClosedBy");

            entity.HasOne(d => d.Team).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Questions_Team");
        });

        modelBuilder.Entity<QuestionFollower>(entity =>
        {
            entity.HasKey(e => e.QuestionFollowerId).HasName("PK__Question__820CA1E7A4636695");

            entity.HasIndex(e => new { e.QuestionId, e.UserId }, "UQ_QuestionFollowers").IsUnique();

            entity.Property(e => e.FollowedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NotifyOnAnswer).HasDefaultValue(true);
            entity.Property(e => e.NotifyOnComment).HasDefaultValue(true);

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionFollowers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_QuestionFollowers_Question");

            entity.HasOne(d => d.User).WithMany(p => p.QuestionFollowers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionFollowers_User");
        });

        modelBuilder.Entity<QuestionTag>(entity =>
        {
            entity.HasKey(e => e.QuestionTagId).HasName("PK__Question__9E06EFA7BB29A0C0");

            entity.HasIndex(e => new { e.QuestionId, e.TagId }, "UQ_QuestionTags").IsUnique();

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionTags)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_QuestionTags_Question");

            entity.HasOne(d => d.Tag).WithMany(p => p.QuestionTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionTags_Tag");
        });

        modelBuilder.Entity<QuestionVote>(entity =>
        {
            entity.HasKey(e => e.QuestionVoteId).HasName("PK__Question__874C1A3AC7E4DB6E");

            entity.HasIndex(e => new { e.QuestionId, e.UserId }, "UQ_QuestionVotes").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionVotes)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_QuestionVotes_Question");

            entity.HasOne(d => d.User).WithMany(p => p.QuestionVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionVotes_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A0D73B196");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616025DF98CD").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CF9AC87290BEA");

            entity.HasIndex(e => e.Category, "IX_Tags_Category");

            entity.HasIndex(e => e.Slug, "IX_Tags_Slug");

            entity.HasIndex(e => e.Slug, "UQ__Tags__BC7B5FB63877B106").IsUnique();

            entity.HasIndex(e => e.TagName, "UQ__Tags__BDE0FD1D522B16F2").IsUnique();

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FollowerCount).HasDefaultValue(0);
            entity.Property(e => e.IconUrl).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.QuestionCount).HasDefaultValue(0);
            entity.Property(e => e.Slug).HasMaxLength(100);
            entity.Property(e => e.TagName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TagFollower>(entity =>
        {
            entity.HasKey(e => e.TagFollowerId).HasName("PK__TagFollo__FD122139D30EF721");

            entity.HasIndex(e => new { e.TagId, e.UserId }, "UQ_TagFollowers").IsUnique();

            entity.Property(e => e.FollowedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagFollowers)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagFollowers_Tag");

            entity.HasOne(d => d.User).WithMany(p => p.TagFollowers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagFollowers_User");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__123AE799A8E293F0");

            entity.HasIndex(e => e.TeamCode, "UQ__Teams__5501350846AFA1AB").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectName).HasMaxLength(200);
            entity.Property(e => e.Semester).HasMaxLength(20);
            entity.Property(e => e.TeamCode).HasMaxLength(20);
            entity.Property(e => e.TeamName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Leader).WithMany(p => p.TeamLeaders)
                .HasForeignKey(d => d.LeaderId)
                .HasConstraintName("FK_Teams_Leader");

            entity.HasOne(d => d.Mentor).WithMany(p => p.TeamMentors)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK_Teams_Mentor");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.TeamMemberId).HasName("PK__TeamMemb__C7C092E54CCEA74C");

            entity.HasIndex(e => new { e.TeamId, e.UserId }, "UQ_TeamMembers").IsUnique();

            entity.Property(e => e.JoinedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("Member");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamMembers_Team");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamMembers_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C01DABA39");

            entity.HasIndex(e => e.Email, "IX_Users_Email");

            entity.HasIndex(e => e.IsActive, "IX_Users_IsActive");

            entity.HasIndex(e => e.RoleId, "IX_Users_RoleId");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053484BB3A3F").IsUnique();

            entity.Property(e => e.AvatarUrl).HasMaxLength(500);
            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsEmailVerified).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.ReputationPoints).HasDefaultValue(0);
            entity.Property(e => e.StudentId).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
