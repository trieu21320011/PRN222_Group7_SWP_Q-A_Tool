using System;
using System.Collections.Generic;
using System.Reflection;
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

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Core> Cores { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationRecipient> NotificationRecipients { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionFollower> QuestionFollowers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<SystemInstructor> SystemInstructors { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=SWP391_QA;Trusted_Connection=True;TrustServerCertificate=True;");

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
            entity.Property(e => e.IsAccepted).HasDefaultValue(false);
            entity.Property(e => e.IsMentorAnswer).HasDefaultValue(false);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Author).WithMany(p => p.Answers)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_Author");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Answers_Question");
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

        modelBuilder.Entity<Core>(entity =>
        {
            entity.HasKey(e => e.CoreId).HasName("PK__Cores__DF0AAB83C7EA60CD");

            entity.HasIndex(e => e.InstructorId, "IX_Cores_InstructorId");

            entity.HasIndex(e => e.SemesterId, "IX_Cores_SemesterId");

            entity.HasIndex(e => new { e.CoreCode, e.SemesterId }, "UQ_Cores_Code_Semester").IsUnique();

            entity.Property(e => e.CoreCode).HasMaxLength(20);
            entity.Property(e => e.CoreName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CurrentTeams).HasDefaultValue(0);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaxTeams).HasDefaultValue(10);
            entity.Property(e => e.Room).HasMaxLength(50);
            entity.Property(e => e.Schedule).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Cores)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cores_Instructor");

            entity.HasOne(d => d.Semester).WithMany(p => p.Cores)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cores_Semester");
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

            entity.HasIndex(e => e.AssignedInstructorId, "IX_Questions_AssignedInstructorId");

            entity.HasIndex(e => e.AuthorId, "IX_Questions_AuthorId");

            entity.HasIndex(e => e.Category, "IX_Questions_Category");

            entity.HasIndex(e => e.CoreId, "IX_Questions_CoreId");

            entity.HasIndex(e => e.CreatedAt, "IX_Questions_CreatedAt").IsDescending();

            entity.HasIndex(e => e.LastActivityAt, "IX_Questions_LastActivityAt").IsDescending();

            entity.HasIndex(e => e.Status, "IX_Questions_Status");

            entity.HasIndex(e => e.TopicId, "IX_Questions_TopicId");

            entity.HasIndex(e => e.ViewCount, "IX_Questions_ViewCount").IsDescending();

            entity.Property(e => e.AnswerCount).HasDefaultValue(0);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.ClosedReason).HasMaxLength(255);
            entity.Property(e => e.CommentCount).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Difficulty).HasMaxLength(20);
            entity.Property(e => e.Excerpt).HasMaxLength(500);
            entity.Property(e => e.IsClosed).HasDefaultValue(false);
            entity.Property(e => e.IsPinned).HasDefaultValue(false);
            entity.Property(e => e.IsPrivate).HasDefaultValue(false);
            entity.Property(e => e.LastActivityAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Open");
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ViewCount).HasDefaultValue(0);

            entity.HasOne(d => d.AssignedInstructor).WithMany(p => p.QuestionAssignedInstructors)
                .HasForeignKey(d => d.AssignedInstructorId)
                .HasConstraintName("FK_Questions_AssignedInstructor");

            entity.HasOne(d => d.Author).WithMany(p => p.QuestionAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Author");

            entity.HasOne(d => d.ClosedBy).WithMany(p => p.QuestionClosedBies)
                .HasForeignKey(d => d.ClosedById)
                .HasConstraintName("FK_Questions_ClosedBy");

            entity.HasOne(d => d.Core).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CoreId)
                .HasConstraintName("FK_Questions_Core");

            entity.HasOne(d => d.Team).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Questions_Team");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_Questions_Topic");
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

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A0D73B196");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616025DF98CD").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.SemesterId).HasName("PK__Semester__043301DD6AC7D7B8");

            entity.HasIndex(e => e.IsActive, "IX_Semesters_IsActive");

            entity.HasIndex(e => e.IsCurrent, "IX_Semesters_IsCurrent");

            entity.HasIndex(e => e.SemesterCode, "UQ__Semester__513151C944B3EE65").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsCurrent).HasDefaultValue(false);
            entity.Property(e => e.SemesterCode).HasMaxLength(20);
            entity.Property(e => e.SemesterName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<SystemInstructor>(entity =>
        {
            entity.HasKey(e => e.SystemInstructorId).HasName("PK__SystemIn__0013902052D896BD");

            entity.HasIndex(e => new { e.UserId, e.SemesterId }, "UQ_SystemInstructors").IsUnique();

            entity.Property(e => e.CanManageCores).HasDefaultValue(true);
            entity.Property(e => e.CanManageInstructors).HasDefaultValue(false);
            entity.Property(e => e.CanManageTopics).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsHead).HasDefaultValue(false);

            entity.HasOne(d => d.Semester).WithMany(p => p.SystemInstructors)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SystemInstructors_Semester");

            entity.HasOne(d => d.User).WithMany(p => p.SystemInstructors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SystemInstructors_User");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__123AE799A8E293F0");

            entity.HasIndex(e => e.CoreId, "IX_Teams_CoreId");

            entity.HasIndex(e => e.SemesterId, "IX_Teams_SemesterId");

            entity.HasIndex(e => e.TopicId, "IX_Teams_TopicId");

            entity.HasIndex(e => e.TeamCode, "UQ__Teams__5501350846AFA1AB").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectName).HasMaxLength(200);
            entity.Property(e => e.Semester).HasMaxLength(20);
            entity.Property(e => e.TeamCode).HasMaxLength(20);
            entity.Property(e => e.TeamName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Core).WithMany(p => p.Teams)
                .HasForeignKey(d => d.CoreId)
                .HasConstraintName("FK_Teams_Core");

            entity.HasOne(d => d.Leader).WithMany(p => p.TeamLeaders)
                .HasForeignKey(d => d.LeaderId)
                .HasConstraintName("FK_Teams_Leader");

            entity.HasOne(d => d.Mentor).WithMany(p => p.TeamMentors)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK_Teams_Mentor");

            entity.HasOne(d => d.SemesterNavigation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.SemesterId)
                .HasConstraintName("FK_Teams_Semester");

            entity.HasOne(d => d.Topic).WithMany(p => p.Teams)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_Teams_Topic");
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

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__Topics__022E0F5DD3409851");

            entity.HasIndex(e => e.IsActive, "IX_Topics_IsActive");

            entity.HasIndex(e => e.SemesterId, "IX_Topics_SemesterId");

            entity.HasIndex(e => new { e.TopicCode, e.SemesterId }, "UQ_Topics_Code_Semester").IsUnique();

            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CurrentTeams).HasDefaultValue(0);
            entity.Property(e => e.Difficulty).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TopicCode).HasMaxLength(20);
            entity.Property(e => e.TopicName).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Semester).WithMany(p => p.Topics)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Topics_Semester");
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
            entity.Property(e => e.StudentId).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        // Apply all configurations from FluentApis folder
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
