-- Docker-compatible database init script
USE [master]
GO

-- Drop database if exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'SWP391_QA')
BEGIN
    ALTER DATABASE [SWP391_QA] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [SWP391_QA];
END
GO

-- Create database (simple, no file paths)
CREATE DATABASE [SWP391_QA]
GO
USE [SWP391_QA]
GO
/****** Object:  User [trieu]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE USER [trieu] FOR LOGIN [trieu] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[AnswerId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[IsAccepted] [bit] NULL,
	[IsMentorAnswer] [bit] NULL,
	[CommentCount] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatMessages]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatMessages](
	[ChatMessageId] [int] IDENTITY(1,1) NOT NULL,
	[ChatRoomId] [int] NOT NULL,
	[SenderId] [int] NOT NULL,
	[MessageText] [nvarchar](max) NOT NULL,
	[MessageType] [nvarchar](20) NULL,
	[AttachmentUrl] [nvarchar](500) NULL,
	[IsEdited] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[ReplyToMessageId] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ChatMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatRooms]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatRooms](
	[ChatRoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [nvarchar](100) NULL,
	[RoomType] [nvarchar](20) NOT NULL,
	[TeamId] [int] NULL,
	[CreatedById] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ChatRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](1000) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[QuestionId] [int] NULL,
	[AnswerId] [int] NULL,
	[ParentCommentId] [int] NULL,
	[IsEdited] [bit] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoreManagers]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoreManagers](
	[CoreManagerId] [int] IDENTITY(1,1) NOT NULL,
	[CoreId] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
	[ManagerType] [nvarchar](20) NULL,
	[AssignedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[CoreManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cores]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cores](
	[CoreId] [int] IDENTITY(1,1) NOT NULL,
	[CoreCode] [nvarchar](20) NOT NULL,
	[CoreName] [nvarchar](100) NOT NULL,
	[SemesterId] [int] NOT NULL,
	[InstructorId] [int] NOT NULL,
	[MaxTeams] [int] NULL,
	[CurrentTeams] [int] NULL,
	[Schedule] [nvarchar](200) NULL,
	[Room] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[CoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationRecipients]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationRecipients](
	[NotificationRecipientId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsRead] [bit] NULL,
	[ReadAt] [datetime2](7) NULL,
	[IsEmailSent] [bit] NULL,
	[EmailSentAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificationRecipientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[NotificationType] [nvarchar](50) NOT NULL,
	[ReferenceType] [nvarchar](50) NULL,
	[ReferenceId] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionFollowers]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionFollowers](
	[QuestionFollowerId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[NotifyOnAnswer] [bit] NULL,
	[NotifyOnComment] [bit] NULL,
	[FollowedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionFollowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[Excerpt] [nvarchar](500) NULL,
	[AuthorId] [int] NOT NULL,
	[TeamId] [int] NULL,
	[Status] [nvarchar](20) NULL,
	[Category] [nvarchar](50) NULL,
	[Difficulty] [nvarchar](20) NULL,
	[ViewCount] [int] NULL,
	[AnswerCount] [int] NULL,
	[CommentCount] [int] NULL,
	[AcceptedAnswerId] [int] NULL,
	[IsPinned] [bit] NULL,
	[IsClosed] [bit] NULL,
	[ClosedReason] [nvarchar](255) NULL,
	[ClosedById] [int] NULL,
	[ClosedAt] [datetime2](7) NULL,
	[LastActivityAt] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[CoreId] [int] NULL,
	[AssignedInstructorId] [int] NULL,
	[IsPrivate] [bit] NULL,
	[TopicId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Semesters]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Semesters](
	[SemesterId] [int] IDENTITY(1,1) NOT NULL,
	[SemesterCode] [nvarchar](20) NOT NULL,
	[SemesterName] [nvarchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[IsActive] [bit] NULL,
	[IsCurrent] [bit] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeamMembers]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamMembers](
	[TeamMemberId] [int] IDENTITY(1,1) NOT NULL,
	[TeamId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Role] [nvarchar](50) NULL,
	[JoinedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[TeamMemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[TeamId] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [nvarchar](100) NOT NULL,
	[TeamCode] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ProjectName] [nvarchar](200) NULL,
	[LeaderId] [int] NULL,
	[MentorId] [int] NULL,
	[Semester] [nvarchar](20) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[CoreId] [int] NULL,
	[TopicId] [int] NULL,
	[SemesterId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[TopicId] [int] IDENTITY(1,1) NOT NULL,
	[TopicCode] [nvarchar](20) NOT NULL,
	[TopicName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[SemesterId] [int] NOT NULL,
	[Category] [nvarchar](100) NULL,
	[Difficulty] [nvarchar](20) NULL,
	[MaxTeams] [int] NULL,
	[CurrentTeams] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[TopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](50) NULL,
	[AvatarUrl] [nvarchar](500) NULL,
	[Bio] [nvarchar](500) NULL,
	[StudentId] [nvarchar](20) NULL,
	[RoleId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[IsEmailVerified] [bit] NULL,
	[LastLoginAt] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Answers] ON 

INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (1, 1, 3, N'Chào bạn, mình đã từng tích hợp VNPay cho dự án trước. Đây là các bước cơ bản:

**1. Cài đặt NuGet packages cần thiết:**
```
VNPAY.NET
```

**2. Tạo VNPayService:**
```csharp
public class VNPayService
{
    public string CreatePaymentUrl(PaymentRequest request, HttpContext context)
    {
        var vnpay = new VnPayLibrary();
        vnpay.AddRequestData("vnp_Version", "2.1.0");
        vnpay.AddRequestData("vnp_Command", "pay");
        // ... thêm các params khác
        return vnpay.CreateRequestUrl(baseUrl, hashSecret);
    }
}
```

**3. Xử lý callback:**
Tạo endpoint để VNPay redirect về sau khi thanh toán.

Bạn cần tài liệu chi tiết hơn không?', 0, 1, 0, CAST(N'2026-02-26T19:00:00.0000000' AS DateTime2), CAST(N'2026-02-26T19:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (2, 1, 8, N'Bổ sung thêm cho bạn: Nhớ test kỹ trên môi trường sandbox trước nhé. VNPay có cấp tài khoản test.

Link đăng ký sandbox: https://sandbox.vnpayment.vn

Có gì không hiểu thì ping mình!', 0, 0, 0, CAST(N'2026-02-27T10:30:00.0000000' AS DateTime2), CAST(N'2026-02-27T10:30:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (3, 2, 4, N'Đây là cách implement JWT trong .NET 8:

**1. Install packages:**
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

**2. Configure trong Program.cs:**
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
```

**3. Generate token:**
```csharp
public string GenerateToken(User user)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.RoleName)
    };
    
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(2),
        signingCredentials: creds);
        
    return new JwtSecurityTokenHandler().WriteToken(token);
}
```', 0, 1, 1, CAST(N'2026-02-26T20:00:00.0000000' AS DateTime2), CAST(N'2026-02-26T20:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (4, 2, 10, N'Bổ sung thêm về Refresh Token:

Nên lưu refresh token vào database và set expiry dài hơn (7-30 ngày). Khi access token hết hạn, client gửi refresh token để lấy access token mới.

```csharp
public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public int UserId { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
}
```', 0, 0, 0, CAST(N'2026-02-27T08:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T08:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (5, 2, 2, N'Các bạn implement tốt rồi. Mình bổ sung thêm về security best practices:

1. **Không lưu sensitive data trong token** - JWT có thể decode được
2. **Sử dụng HTTPS** bắt buộc
3. **Access token nên có lifetime ngắn** (15-30 phút)
4. **Implement token revocation** cho logout
5. **Sử dụng secure cookie** để lưu token thay vì localStorage

Đây là bài viết hay về JWT security: https://auth0.com/blog/jwt-security-best-practices/', 0, 0, 1, CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (6, 3, 3, N'Code của bạn đúng rồi! Đó là cách traditional để implement many-to-many trong EF Core.

Tuy nhiên, từ **EF Core 5.0+**, bạn có thể dùng cách đơn giản hơn mà không cần entity trung gian:

```csharp
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public ICollection<Category> Categories { get; set; }
}

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}
```

EF Core sẽ tự tạo join table. Bạn chỉ cần configure trong OnModelCreating:

```csharp
modelBuilder.Entity<Product>()
    .HasMany(p => p.Categories)
    .WithMany(c => c.Products);
```

**Khi nào dùng cách nào?**
- Dùng entity trung gian khi cần thêm properties cho relationship (VD: Quantity, AddedDate)
- Dùng cách mới khi relationship đơn giản', 1, 1, 0, CAST(N'2026-02-26T21:00:00.0000000' AS DateTime2), CAST(N'2026-02-26T21:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (7, 3, 14, N'Cảm ơn mentor! Mình cũng vừa tìm hiểu thêm và thấy cách mới clean hơn nhiều.', 0, 0, 0, CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (8, 4, 5, N'Đây là các bước deploy lên Azure với Free tier:

**1. Tạo Azure Account**
- Đăng ký tại portal.azure.com
- Sinh viên có thể dùng Azure for Students (free $100 credit)

**2. Tạo resources:**
- **App Service** (Free F1 tier)
- **Azure SQL Database** (Basic tier ~$5/month hoặc dùng SQL Server Express local)

**3. Deploy từ Visual Studio:**
- Right-click project > Publish
- Chọn Azure > Azure App Service
- Login và chọn subscription
- Create new hoặc select existing App Service
- Publish

**4. Configure Connection String:**
- Vào App Service > Configuration
- Add connection string cho database

**Tips tiết kiệm:**
- Dùng Azure SQL serverless để save cost
- Set auto-shutdown cho dev environments', 0, 1, 0, CAST(N'2026-02-27T11:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (9, 5, 4, N'Đây là cách implement SignalR notification:

**1. Install package:**
```bash
dotnet add package Microsoft.AspNetCore.SignalR
```

**2. Tạo Hub:**
```csharp
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
        await base.OnConnectedAsync();
    }
}
```

**3. Register trong Program.cs:**
```csharp
builder.Services.AddSignalR();
app.MapHub<NotificationHub>("/notificationHub");
```

**4. Send notification từ service:**
```csharp
public class NotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    
    public async Task SendToUser(int userId, string message)
    {
        await _hubContext.Clients.Group($"user_{userId}")
            .SendAsync("ReceiveNotification", message);
    }
}
```

**5. Client-side (JavaScript):**
```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("ReceiveNotification", (message) => {
    // Show popup notification
    toastr.info(message);
});

connection.start();
```', 0, 1, 0, CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (10, 6, 3, N'Đây là một câu hỏi hay và có nhiều quan điểm khác nhau!

**Khi NÊN dùng Repository Pattern:**
1. Dự án lớn, phức tạp
2. Cần abstraction để dễ test (mock repository)
3. Team có thể switch ORM/database trong tương lai
4. Business logic phức tạp cần encapsulate

**Khi KHÔNG cần Repository Pattern:**
1. Dự án nhỏ, đơn giản
2. Team nhỏ, ít kinh nghiệm
3. Chắc chắn sẽ dùng EF Core mãi
4. CRUD đơn giản

**Với dự án SWP391:**
Mình khuyên dùng Repository Pattern vì:
- Giúp các bạn học về design pattern
- Dễ viết unit test (yêu cầu của môn học)
- Code clean và maintainable hơn', 0, 1, 1, CAST(N'2026-02-27T11:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (11, 6, 8, N'Mình đồng ý với mentor. Team mình đang dùng Repository + Unit of Work và thấy code clean hơn nhiều.', 0, 0, 0, CAST(N'2026-02-27T12:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T12:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (12, 6, 2, N'Thêm một góc nhìn: Các bạn có thể dùng **CQRS pattern** - tách riêng Read và Write operations. Với Read, có thể query trực tiếp DbContext. Với Write, dùng Repository.

Đây là hybrid approach phổ biến trong enterprise applications.', 0, 0, 1, CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (13, 6, 16, N'Cảm ơn mọi người! Team mình quyết định sẽ dùng Repository Pattern để học và có code structure tốt hơn.', 0, 0, 0, CAST(N'2026-02-27T16:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (14, 7, 4, N'Với dự án student, mình recommend dùng **wwwroot** vì đơn giản nhất:

```csharp
[HttpPost]
public async Task<IActionResult> UploadAvatar(IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest();
    
    // Validate file type
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
    var extension = Path.GetExtension(file.FileName).ToLower();
    if (!allowedExtensions.Contains(extension))
        return BadRequest("Invalid file type");
    
    // Generate unique filename
    var fileName = $"{Guid.NewGuid()}{extension}";
    var filePath = Path.Combine(_env.WebRootPath, "uploads", "avatars", fileName);
    
    // Ensure directory exists
    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
    
    // Save file
    using var stream = new FileStream(filePath, FileMode.Create);
    await file.CopyToAsync(stream);
    
    // Return URL
    return Ok(new { url = $"/uploads/avatars/{fileName}" });
}
```

**Lưu ý:**
- Validate file size (max 2MB)
- Validate file type (chỉ cho phép image)
- Generate unique filename để tránh conflict', 0, 1, 0, CAST(N'2026-02-27T12:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T12:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (15, 7, 10, N'Nếu sau này muốn scale lên, có thể dùng Azure Blob Storage. Sinh viên có thể dùng free tier với 5GB storage.', 0, 0, 0, CAST(N'2026-02-27T17:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T17:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (16, 8, 5, N'Đây là cách viết unit test cho Controller:

**1. Sample Controller:**
```csharp
public class ProductController : Controller
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
}
```

**2. Unit Test với Moq:**
```csharp
public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;
    
    public ProductControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductController(_mockService.Object);
    }
    
    [Fact]
    public async Task GetById_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(product);
        
        // Act
        var result = await _controller.GetById(1);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(1, returnedProduct.Id);
    }
    
    [Fact]
    public async Task GetById_ReturnsNotFound_WhenProductNotExists()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Product?)null);
        
        // Act
        var result = await _controller.GetById(999);
        
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
```', 0, 1, 0, CAST(N'2026-02-27T18:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T18:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (17, 9, 3, N'Với dự án CRUD là chính, mình recommend dùng **Razor Pages**:

**Ưu điểm Razor Pages:**
1. **Đơn giản hơn** - page-focused, không cần routing phức tạp
2. **Code organization tốt hơn** - .cshtml và .cshtml.cs cùng folder
3. **Less boilerplate** - không cần tạo nhiều controller
4. **Dễ học** - phù hợp với team mới

**Ưu điểm MVC:**
1. **Flexible routing** - phù hợp với API phức tạp
2. **Tái sử dụng views** - dễ share partial views
3. **Phù hợp cho SPA backend**

**Với quản lý ký túc xá:**
Razor Pages là lựa chọn tốt vì:
- Chức năng chủ yếu là form-based
- Ít cần custom routing
- Dễ maintain

Các bạn có thể xem thêm: https://docs.microsoft.com/aspnet/core/razor-pages/', 1, 1, 0, CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (18, 9, 6, N'Team mình cũng dùng Razor Pages và thấy develop nhanh hơn MVC. Recommend!', 0, 0, 0, CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (19, 9, 2, N'Bổ sung: Razor Pages và MVC có thể dùng chung trong một project. Bạn có thể dùng Razor Pages cho admin CRUD và MVC cho API/public pages.', 0, 0, 1, CAST(N'2026-02-27T19:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T19:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (20, 10, 4, N'Bạn có thể dùng **Global Query Filters** trong EF Core:

**1. Tạo base entity:**
```csharp
public abstract class BaseEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}
```

**2. Configure trong DbContext:**
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Apply to all entities that inherit BaseEntity
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
        if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
        {
            modelBuilder.Entity(entityType.ClrType)
                .HasQueryFilter(
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(
                                Expression.Parameter(entityType.ClrType, "e"), 
                                "IsDeleted"),
                            Expression.Constant(false)),
                        Expression.Parameter(entityType.ClrType, "e")));
        }
    }
}
```

**3. Hoặc đơn giản hơn với từng entity:**
```csharp
modelBuilder.Entity<Product>()
    .HasQueryFilter(p => !p.IsDeleted);
```

**4. Override SoftDelete trong repository:**
```csharp
public async Task SoftDeleteAsync(int id)
{
    var entity = await _dbSet.FindAsync(id);
    if (entity != null)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}
```

**Lưu ý:** Nếu cần query cả deleted records, dùng `.IgnoreQueryFilters()`', 0, 1, 0, CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (21, 10, 14, N'Cảm ơn mentor! Cách dùng Global Query Filters rất hay, không cần thêm where clause vào mỗi query nữa.', 0, 0, 0, CAST(N'2026-02-27T20:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T20:00:00.0000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (22, 8, 2, N'trả lời', 0, 1, 0, CAST(N'2026-03-09T04:31:54.9658633' AS DateTime2), CAST(N'2026-03-09T11:31:55.0300000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (23, 3, 2, N'ji', 0, 1, 0, CAST(N'2026-03-09T04:32:15.6755962' AS DateTime2), CAST(N'2026-03-09T11:32:15.6833333' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (24, 3, 2, N'ji', 0, 1, 0, CAST(N'2026-03-09T04:32:21.1814055' AS DateTime2), CAST(N'2026-03-09T11:32:21.1833333' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (25, 8, 2, N'1', 0, 1, 0, CAST(N'2026-03-09T04:33:26.2938982' AS DateTime2), CAST(N'2026-03-09T11:33:26.3000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (26, 8, 2, N'123', 0, 1, 0, CAST(N'2026-03-09T04:47:39.0453607' AS DateTime2), CAST(N'2026-03-09T11:47:39.1000000' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (27, 11, 2, N'32144', 0, 1, 0, CAST(N'2026-03-10T03:57:02.8426468' AS DateTime2), CAST(N'2026-03-10T10:57:02.8866667' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (28, 12, 2, N'cung oke', 0, 1, 2, CAST(N'2026-03-10T04:02:42.0949321' AS DateTime2), CAST(N'2026-03-10T11:02:42.1233333' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (29, 12, 2, N'Dừng câu hỏi tại đây, đã được giải quyết', 0, 1, 0, CAST(N'2026-03-10T04:13:38.6720254' AS DateTime2), CAST(N'2026-03-10T11:13:38.7033333' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (30, 13, 13, N'oke ds em', 0, 1, 0, CAST(N'2026-03-16T03:59:20.7672039' AS DateTime2), CAST(N'2026-03-16T10:59:20.7966667' AS DateTime2))
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AuthorId], [Body], [IsAccepted], [IsMentorAnswer], [CommentCount], [CreatedAt], [UpdatedAt]) VALUES (31, 14, 5, N'Thiet ke 123', 0, 1, 1, CAST(N'2026-03-16T13:21:59.7916285' AS DateTime2), CAST(N'2026-03-16T20:21:59.8233333' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Answers] OFF
GO
SET IDENTITY_INSERT [dbo].[ChatMessages] ON 

INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (1, 1, 6, N'Chào cả team! Hôm nay mình bắt đầu sprint 2 nhé', N'Text', NULL, 0, 0, NULL, CAST(N'2026-02-27T08:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T08:00:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (2, 1, 7, N'Ok anh, em đã push code login rồi', N'Text', NULL, 0, 0, NULL, CAST(N'2026-02-27T08:05:00.0000000' AS DateTime2), CAST(N'2026-02-27T08:05:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (3, 1, 8, N'Mình review code và merge nhé', N'Text', NULL, 0, 0, 2, CAST(N'2026-02-27T08:10:00.0000000' AS DateTime2), CAST(N'2026-02-27T08:10:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (4, 1, 9, N'Em đang làm phần product, có issue về pagination', N'Text', NULL, 0, 0, NULL, CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (5, 1, 6, N'@Dung share code em đang gặp issue đi', N'Text', NULL, 0, 0, 4, CAST(N'2026-02-27T09:05:00.0000000' AS DateTime2), CAST(N'2026-02-27T09:05:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (6, 5, 2, N'Chào các bạn, tuần này nhớ submit SRS draft nhé!', N'Text', NULL, 0, 0, NULL, CAST(N'2026-02-27T07:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T07:00:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (7, 5, 6, N'Dạ thầy, team em đã hoàn thành 80% rồi ạ', N'Text', NULL, 0, 0, 6, CAST(N'2026-02-27T07:30:00.0000000' AS DateTime2), CAST(N'2026-02-27T07:30:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (8, 5, 10, N'Team em cũng gần xong rồi thầy', N'Text', NULL, 0, 0, 6, CAST(N'2026-02-27T07:35:00.0000000' AS DateTime2), CAST(N'2026-02-27T07:35:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (9, 2, 10, N'Team ơi, ai handle phần booking schedule?', N'Text', NULL, 0, 0, NULL, CAST(N'2026-02-27T10:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T10:00:00.0000000' AS DateTime2))
INSERT [dbo].[ChatMessages] ([ChatMessageId], [ChatRoomId], [SenderId], [MessageText], [MessageType], [AttachmentUrl], [IsEdited], [IsDeleted], [ReplyToMessageId], [CreatedAt], [UpdatedAt]) VALUES (10, 2, 11, N'Em đang làm phần đó anh', N'Text', NULL, 0, 0, 9, CAST(N'2026-02-27T10:05:00.0000000' AS DateTime2), CAST(N'2026-02-27T10:05:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[ChatMessages] OFF
GO
SET IDENTITY_INSERT [dbo].[ChatRooms] ON 

INSERT [dbo].[ChatRooms] ([ChatRoomId], [RoomName], [RoomType], [TeamId], [CreatedById], [IsActive], [CreatedAt]) VALUES (1, N'Team Alpha Chat', N'Team', 1, 6, 1, CAST(N'2026-02-26T17:30:00.0000000' AS DateTime2))
INSERT [dbo].[ChatRooms] ([ChatRoomId], [RoomName], [RoomType], [TeamId], [CreatedById], [IsActive], [CreatedAt]) VALUES (2, N'Team Beta Chat', N'Team', 2, 10, 1, CAST(N'2026-02-26T17:30:00.0000000' AS DateTime2))
INSERT [dbo].[ChatRooms] ([ChatRoomId], [RoomName], [RoomType], [TeamId], [CreatedById], [IsActive], [CreatedAt]) VALUES (3, N'Team Gamma Chat', N'Team', 3, 14, 1, CAST(N'2026-02-26T17:30:00.0000000' AS DateTime2))
INSERT [dbo].[ChatRooms] ([ChatRoomId], [RoomName], [RoomType], [TeamId], [CreatedById], [IsActive], [CreatedAt]) VALUES (4, N'Team Delta Chat', N'Team', 4, 18, 1, CAST(N'2026-02-26T17:30:00.0000000' AS DateTime2))
INSERT [dbo].[ChatRooms] ([ChatRoomId], [RoomName], [RoomType], [TeamId], [CreatedById], [IsActive], [CreatedAt]) VALUES (5, N'SE1801 General', N'Class', NULL, 2, 1, CAST(N'2026-02-26T17:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[ChatRooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (1, N'Cảm ơn bạn đã hỏi, mình cũng đang cần tích hợp VNPay!', 7, 1, NULL, NULL, 0, CAST(N'2026-02-26T18:30:00.0000000' AS DateTime2), CAST(N'2026-02-26T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (2, N'Phần refresh token rất hữu ích, thanks!', 9, NULL, 3, NULL, 0, CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (3, N'Mình có một repo sample về JWT, để mình share sau nhé', 10, NULL, 5, NULL, 0, CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (4, N'Team mình đang dùng cách này, rất ổn!', 15, NULL, 10, NULL, 0, CAST(N'2026-02-27T12:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T12:00:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (5, N'CQRS nghe hay ho, có tutorial nào recommend không anh?', 17, NULL, 12, NULL, 0, CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (6, N'Cảm ơn mentor đã giải đáp thắc mắc!', 18, 9, NULL, NULL, 0, CAST(N'2026-02-27T14:30:00.0000000' AS DateTime2), CAST(N'2026-02-27T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (7, N'Có thể combine cả 2 cách trong 1 project được không?', 19, NULL, 17, NULL, 0, CAST(N'2026-02-27T19:30:00.0000000' AS DateTime2), CAST(N'2026-02-27T19:30:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (8, N'Câu hỏi hay, mình cũng từng thắc mắc vấn đề này', 11, 6, NULL, NULL, 0, CAST(N'2026-02-27T10:30:00.0000000' AS DateTime2), CAST(N'2026-02-27T10:30:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (9, N'Bài viết về JWT security rất hay!', 12, NULL, 5, NULL, 0, CAST(N'2026-02-27T16:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (10, N'Follow để đọc thêm các câu trả lời', 13, 5, NULL, NULL, 0, CAST(N'2026-02-27T10:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T10:00:00.0000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (13, N'vậy mình thêm được ko thầy', 3, NULL, 28, NULL, 0, CAST(N'2026-03-10T04:08:29.3054356' AS DateTime2), CAST(N'2026-03-10T11:08:29.4000000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (14, N'OKe vậy em sẽ hỏi thêm nha', 3, 12, NULL, NULL, 0, CAST(N'2026-03-10T04:08:48.4567952' AS DateTime2), CAST(N'2026-03-10T11:08:48.4566667' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (15, N'oke', 2, NULL, 28, NULL, 0, CAST(N'2026-03-10T04:13:11.1358944' AS DateTime2), CAST(N'2026-03-10T11:13:11.2066667' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (16, N'được em oiiw', 2, 12, NULL, NULL, 0, CAST(N'2026-03-10T04:13:21.5485935' AS DateTime2), CAST(N'2026-03-10T11:13:21.5500000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (17, N'hoi them', 21, 14, NULL, NULL, 0, CAST(N'2026-03-16T13:21:17.7233884' AS DateTime2), CAST(N'2026-03-16T20:21:17.7400000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (18, N'thay thay nhu vay ok roi', 5, 14, NULL, NULL, 0, CAST(N'2026-03-16T13:21:50.5615685' AS DateTime2), CAST(N'2026-03-16T20:21:50.5600000' AS DateTime2))
INSERT [dbo].[Comments] ([CommentId], [Body], [AuthorId], [QuestionId], [AnswerId], [ParentCommentId], [IsEdited], [CreatedAt], [UpdatedAt]) VALUES (19, N'hoi them', 21, NULL, 31, NULL, 0, CAST(N'2026-03-16T13:22:19.3064130' AS DateTime2), CAST(N'2026-03-16T20:22:19.3100000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[CoreManagers] ON 

INSERT [dbo].[CoreManagers] ([CoreManagerId], [CoreId], [ManagerId], [ManagerType], [AssignedAt]) VALUES (1, 1, 3, N'Mentor', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[CoreManagers] ([CoreManagerId], [CoreId], [ManagerId], [ManagerType], [AssignedAt]) VALUES (2, 2, 4, N'Mentor', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[CoreManagers] ([CoreManagerId], [CoreId], [ManagerId], [ManagerType], [AssignedAt]) VALUES (3, 3, 5, N'Mentor', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[CoreManagers] ([CoreManagerId], [CoreId], [ManagerId], [ManagerType], [AssignedAt]) VALUES (4, 1, 2, N'Instructor', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[CoreManagers] ([CoreManagerId], [CoreId], [ManagerId], [ManagerType], [AssignedAt]) VALUES (5, 2, 2, N'Instructor', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[CoreManagers] ([CoreManagerId], [CoreId], [ManagerId], [ManagerType], [AssignedAt]) VALUES (6, 3, 3, N'Instructor', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[CoreManagers] OFF
GO
SET IDENTITY_INSERT [dbo].[Cores] ON 

INSERT [dbo].[Cores] ([CoreId], [CoreCode], [CoreName], [SemesterId], [InstructorId], [MaxTeams], [CurrentTeams], [Schedule], [Room], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'SE1801', N'Software Engineering 1801', 4, 7, 10, 2, N'Thứ 2, 7:30-9:00', N'DE-C201', 1, CAST(N'2026-02-26T17:17:51.0366667' AS DateTime2), CAST(N'2026-03-10T02:45:41.8612125' AS DateTime2))
INSERT [dbo].[Cores] ([CoreId], [CoreCode], [CoreName], [SemesterId], [InstructorId], [MaxTeams], [CurrentTeams], [Schedule], [Room], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'SE1802', N'Software Engineering 1802', 4, 2, 10, 2, N'Thứ 3, 7:30-9:00', N'DE-C202', 1, CAST(N'2026-02-26T17:17:51.0366667' AS DateTime2), CAST(N'2026-03-09T07:51:01.5625377' AS DateTime2))
INSERT [dbo].[Cores] ([CoreId], [CoreCode], [CoreName], [SemesterId], [InstructorId], [MaxTeams], [CurrentTeams], [Schedule], [Room], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'SE1803', N'Software Engineering 1803', 4, 2, 10, 0, N'Thứ 4, 7:30-9:00', N'DE-C203', 1, CAST(N'2026-02-26T17:17:51.0366667' AS DateTime2), CAST(N'2026-03-09T07:51:03.2818646' AS DateTime2))
INSERT [dbo].[Cores] ([CoreId], [CoreCode], [CoreName], [SemesterId], [InstructorId], [MaxTeams], [CurrentTeams], [Schedule], [Room], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'SE1804', N'Software Engineering 1804', 4, 16, 10, 0, N'Thứ 5, 7:30-9:00', N'DE-C204', 1, CAST(N'2026-02-26T17:17:51.0366667' AS DateTime2), CAST(N'2026-03-10T02:46:06.3376995' AS DateTime2))
INSERT [dbo].[Cores] ([CoreId], [CoreCode], [CoreName], [SemesterId], [InstructorId], [MaxTeams], [CurrentTeams], [Schedule], [Room], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'PRN223', N'PRN223-FU2026', 4, 6, 4, 0, N'Thu 2', N'A26', 0, CAST(N'2026-03-10T02:43:15.9166631' AS DateTime2), CAST(N'2026-03-16T04:00:41.4813736' AS DateTime2))
INSERT [dbo].[Cores] ([CoreId], [CoreCode], [CoreName], [SemesterId], [InstructorId], [MaxTeams], [CurrentTeams], [Schedule], [Room], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'PRN223-23131', N'PRN223-FU2026', 5, 2, 10, 0, N'Thu 2', N'A2687', 1, CAST(N'2026-03-10T03:20:08.2602111' AS DateTime2), CAST(N'2026-03-10T10:20:08.2700000' AS DateTime2))
INSERT [dbo].[Cores] ([CoreId], [CoreCode], [CoreName], [SemesterId], [InstructorId], [MaxTeams], [CurrentTeams], [Schedule], [Room], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, N'SWP231-2026', N'SWP231-2026', 5, 5, 10, 0, N'11', N'12312', 1, CAST(N'2026-03-16T13:13:02.1395713' AS DateTime2), CAST(N'2026-03-16T13:13:38.7816506' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Cores] OFF
GO
SET IDENTITY_INSERT [dbo].[NotificationRecipients] ON 

INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (1, 1, 3, 1, CAST(N'2026-02-26T18:30:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (2, 1, 2, 1, CAST(N'2026-02-26T19:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (3, 2, 6, 1, CAST(N'2026-02-26T19:30:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (4, 3, 3, 1, CAST(N'2026-02-27T09:30:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (5, 4, 6, 0, NULL, 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (6, 4, 7, 0, NULL, 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (7, 4, 8, 1, CAST(N'2026-02-27T08:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (8, 5, 6, 0, NULL, 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (9, 5, 7, 0, NULL, 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (10, 5, 8, 0, NULL, 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (11, 5, 9, 1, CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[NotificationRecipients] ([NotificationRecipientId], [NotificationId], [UserId], [IsRead], [ReadAt], [IsEmailSent], [EmailSentAt]) VALUES (12, 5, 10, 0, NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[NotificationRecipients] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 

INSERT [dbo].[Notifications] ([NotificationId], [Title], [Message], [NotificationType], [ReferenceType], [ReferenceId], [CreatedById], [CreatedAt]) VALUES (1, N'Câu hỏi mới trong lớp', N'Có câu hỏi mới về tích hợp VNPay trong lớp SE1801', N'NewQuestion', N'Question', 1, 6, CAST(N'2026-02-26T18:00:00.0000000' AS DateTime2))
INSERT [dbo].[Notifications] ([NotificationId], [Title], [Message], [NotificationType], [ReferenceType], [ReferenceId], [CreatedById], [CreatedAt]) VALUES (2, N'Câu trả lời mới', N'Mentor đã trả lời câu hỏi của bạn về VNPay', N'NewAnswer', N'Answer', 1, 3, CAST(N'2026-02-26T19:00:00.0000000' AS DateTime2))
INSERT [dbo].[Notifications] ([NotificationId], [Title], [Message], [NotificationType], [ReferenceType], [ReferenceId], [CreatedById], [CreatedAt]) VALUES (3, N'Câu trả lời được chấp nhận', N'Câu trả lời của bạn về EF Core Many-to-Many đã được chấp nhận', N'AnswerAccepted', N'Answer', 6, 7, CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2))
INSERT [dbo].[Notifications] ([NotificationId], [Title], [Message], [NotificationType], [ReferenceType], [ReferenceId], [CreatedById], [CreatedAt]) VALUES (4, N'Thông báo hệ thống', N'Chào mừng bạn đến với SWP QA Tool!', N'System', NULL, NULL, 1, CAST(N'2026-02-26T10:00:00.0000000' AS DateTime2))
INSERT [dbo].[Notifications] ([NotificationId], [Title], [Message], [NotificationType], [ReferenceType], [ReferenceId], [CreatedById], [CreatedAt]) VALUES (5, N'Nhắc nhở', N'Deadline nộp SRS là 01/03/2026', N'Reminder', NULL, NULL, 2, CAST(N'2026-02-27T08:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionFollowers] ON 

INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (1, 1, 6, 1, 1, CAST(N'2026-02-26T18:00:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (2, 1, 7, 1, 0, CAST(N'2026-02-26T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (3, 2, 8, 1, 1, CAST(N'2026-02-26T19:00:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (4, 2, 9, 1, 1, CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (5, 5, 9, 1, 1, CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (6, 5, 13, 1, 0, CAST(N'2026-02-27T10:00:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (7, 6, 14, 1, 1, CAST(N'2026-02-27T10:00:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (8, 6, 11, 1, 0, CAST(N'2026-02-27T10:30:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (9, 9, 18, 1, 1, CAST(N'2026-02-27T13:00:00.0000000' AS DateTime2))
INSERT [dbo].[QuestionFollowers] ([QuestionFollowerId], [QuestionId], [UserId], [NotifyOnAnswer], [NotifyOnComment], [FollowedAt]) VALUES (10, 9, 19, 1, 1, CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[QuestionFollowers] OFF
GO
SET IDENTITY_INSERT [dbo].[Questions] ON 

INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (1, N'Làm sao để tích hợp VNPay vào ASP.NET Core?', N'Chào mọi người, mình đang làm dự án bán hàng online và cần tích hợp thanh toán VNPay. Mình đã đọc tài liệu nhưng vẫn chưa hiểu cách implement trong ASP.NET Core. Ai có kinh nghiệm cho mình xin hướng dẫn chi tiết được không?

Cụ thể mình cần:
1. Cách tạo URL thanh toán
2. Xử lý callback từ VNPay
3. Verify chữ ký

Cảm ơn mọi người!', N'Chào mọi người, mình đang làm dự án bán hàng online và cần tích hợp thanh toán VNPay...', 6, 1, N'Open', N'Payment', N'Medium', 45, 2, 1, NULL, 0, 0, NULL, NULL, NULL, CAST(N'2026-02-27T10:30:00.0000000' AS DateTime2), CAST(N'2026-02-26T18:00:00.0000000' AS DateTime2), CAST(N'2026-02-26T18:00:00.0000000' AS DateTime2), 1, 2, 0, 1)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (2, N'Cách xử lý authentication với JWT trong .NET 8?', N'Mình đang build API cho project và muốn sử dụng JWT để authenticate. Ai có thể chia sẻ best practice khi implement JWT trong .NET 8 không?

Mình đang dùng:
- .NET 8
- Entity Framework Core
- SQL Server

Mình muốn biết:
- Cách generate token
- Cách validate token
- Refresh token flow', N'Mình đang build API cho project và muốn sử dụng JWT để authenticate...', 8, 1, N'Answered', N'Authentication', N'Hard', 78, 3, 2, NULL, 1, 0, NULL, NULL, NULL, CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2), CAST(N'2026-02-26T19:00:00.0000000' AS DateTime2), CAST(N'2026-02-26T19:00:00.0000000' AS DateTime2), 1, 2, 0, 1)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (3, N'Entity Framework Core - Cách xử lý relationship nhiều-nhiều?', N'Mình có 2 entity Product và Category, mỗi Product có thể thuộc nhiều Category và mỗi Category có thể chứa nhiều Product. 

Mình implement như thế này có đúng không?

```csharp
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
}

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
}

public class ProductCategory
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
```', N'Mình có 2 entity Product và Category, mỗi Product có thể thuộc nhiều Category...', 7, 1, N'Resolved', N'Database', N'Easy', 120, 4, 0, 6, 0, 1, NULL, 2, CAST(N'2026-03-09T11:32:21.1883265' AS DateTime2), CAST(N'2026-03-09T04:32:21.1845307' AS DateTime2), CAST(N'2026-02-26T20:00:00.0000000' AS DateTime2), CAST(N'2026-03-09T11:32:21.1883580' AS DateTime2), 1, NULL, 0, 1)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (4, N'Làm sao để deploy ứng dụng ASP.NET Core lên Azure?', N'Team mình cần deploy dự án lên Azure để demo cho mentor. Mình chưa có kinh nghiệm với Azure, ai có thể hướng dẫn các bước cơ bản không?

- Dự án: ASP.NET Core Web App + SQL Server
- Budget: Free tier
- Yêu cầu: Custom domain nếu có thể', N'Team mình cần deploy dự án lên Azure để demo cho mentor...', 10, 2, N'Open', N'Deployment', N'Medium', 35, 1, 0, NULL, 0, 0, NULL, NULL, NULL, CAST(N'2026-02-27T11:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T08:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T08:00:00.0000000' AS DateTime2), 1, NULL, 0, 2)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (5, N'SignalR - Cách implement real-time notification?', N'Dự án của mình cần có tính năng thông báo real-time khi có đơn hàng mới. Mình đã tìm hiểu về SignalR nhưng chưa biết cách implement. 

Ai có thể share sample code hoặc tutorial không?

Yêu cầu:
- Send notification từ server đến specific user
- Notification hiển thị popup trên browser
- Lưu notification vào database', N'Dự án của mình cần có tính năng thông báo real-time khi có đơn hàng mới...', 9, 1, N'Open', N'Real-time', N'Hard', 55, 1, 1, NULL, 0, 0, NULL, NULL, NULL, CAST(N'2026-02-27T15:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T09:00:00.0000000' AS DateTime2), 1, 2, 0, 1)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (6, N'Repository Pattern có cần thiết không trong .NET 8?', N'Mình thấy nhiều tutorial cũ khuyên dùng Repository Pattern với EF Core, nhưng một số bài viết mới lại nói là không cần vì DbContext đã là Unit of Work rồi.

Team mình đang tranh luận về vấn đề này. Theo các bạn, khi nào nên dùng Repository Pattern?', N'Mình thấy nhiều tutorial cũ khuyên dùng Repository Pattern với EF Core...', 14, 3, N'Answered', N'Architecture', N'Medium', 88, 4, 3, NULL, 0, 0, NULL, NULL, NULL, CAST(N'2026-02-27T16:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T10:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T10:00:00.0000000' AS DateTime2), 2, NULL, 0, 3)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (7, N'Cách upload và lưu trữ hình ảnh trong ASP.NET Core?', N'Mình cần implement tính năng upload avatar cho user. Có mấy cách mình đang cân nhắc:

1. Lưu file trong wwwroot
2. Lưu vào database dạng byte[]
3. Dùng cloud storage (Azure Blob, AWS S3)

Với dự án student, cách nào phù hợp nhất?', N'Mình cần implement tính năng upload avatar cho user...', 11, 2, N'Open', N'File Upload', N'Easy', 42, 2, 0, NULL, 0, 0, NULL, NULL, NULL, CAST(N'2026-02-27T17:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T11:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T11:00:00.0000000' AS DateTime2), 1, NULL, 0, 2)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (8, N'Làm sao để viết Unit Test cho Controller trong ASP.NET Core?', N'Mentor yêu cầu team phải có unit test coverage ít nhất 60%. Mình chưa biết cách viết test cho Controller với dependency injection.

Mình dùng:
- xUnit
- Moq

Ai có thể cho ví dụ cách mock service và test controller action không?', N'Mentor yêu cầu team phải có unit test coverage ít nhất 60%...', 15, 3, N'Open', N'Testing', N'Hard', 65, 4, 0, NULL, 0, 0, NULL, NULL, NULL, CAST(N'2026-03-09T04:47:39.0766698' AS DateTime2), CAST(N'2026-02-27T12:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T12:00:00.0000000' AS DateTime2), 2, 3, 0, 3)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (9, N'Razor Pages vs MVC - Nên chọn cái nào?', N'Team mình đang bắt đầu dự án mới và đang phân vân giữa Razor Pages và MVC. 

Dự án là web app quản lý ký túc xá với các chức năng CRUD là chính.

Các bạn có recommendation nào không?', N'Team mình đang bắt đầu dự án mới và đang phân vân giữa Razor Pages và MVC...', 18, 4, N'Accepted', N'Architecture', N'Easy', 95, 3, 1, 17, 0, 0, NULL, NULL, NULL, CAST(N'2026-02-27T19:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T13:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T13:00:00.0000000' AS DateTime2), 2, NULL, 0, 4)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (10, N'Cách implement Soft Delete trong EF Core?', N'Thay vì xóa dữ liệu thật, mình muốn chỉ đánh dấu IsDeleted = true. 

Có cách nào để tự động filter out các record đã xóa trong tất cả query không? Mình không muốn phải thêm .Where(x => !x.IsDeleted) vào mỗi query.', N'Thay vì xóa dữ liệu thật, mình muốn chỉ đánh dấu IsDeleted = true...', 16, 3, N'Answered', N'Database', N'Medium', 72, 2, 0, NULL, 0, 0, NULL, NULL, NULL, CAST(N'2026-02-27T20:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2), CAST(N'2026-02-27T14:00:00.0000000' AS DateTime2), 2, NULL, 0, 3)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (11, N'câu hỏi', N'123,321', NULL, 3, 5, N'Resolved', N'Design', N'Easy', 0, 1, 0, NULL, 0, 1, NULL, 3, CAST(N'2026-03-10T10:58:20.7161768' AS DateTime2), CAST(N'2026-03-10T03:57:02.8670563' AS DateTime2), CAST(N'2026-03-10T03:56:22.6480584' AS DateTime2), CAST(N'2026-03-10T10:58:20.7162289' AS DateTime2), 6, NULL, 0, NULL)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (12, N'câu hỏi hom nay', N'1233123123214', NULL, 3, 5, N'Resolved', N'Requirements', N'Easy', 0, 2, 2, NULL, 0, 1, NULL, 2, CAST(N'2026-03-10T11:13:38.7172011' AS DateTime2), CAST(N'2026-03-10T04:13:38.6852544' AS DateTime2), CAST(N'2026-03-10T04:02:20.8447980' AS DateTime2), CAST(N'2026-03-10T11:13:38.7172305' AS DateTime2), 6, NULL, 0, NULL)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (13, N'câu hỏi', N'ẻwerwerwe', NULL, 3, 5, N'Resolved', N'Requirements', N'Easy', 0, 1, 0, NULL, 0, 1, NULL, 3, CAST(N'2026-03-16T10:59:47.1987929' AS DateTime2), CAST(N'2026-03-16T03:59:20.7819566' AS DateTime2), CAST(N'2026-03-16T03:56:26.2163765' AS DateTime2), CAST(N'2026-03-16T10:59:47.1988749' AS DateTime2), 5, NULL, 0, NULL)
INSERT [dbo].[Questions] ([QuestionId], [Title], [Body], [Excerpt], [AuthorId], [TeamId], [Status], [Category], [Difficulty], [ViewCount], [AnswerCount], [CommentCount], [AcceptedAnswerId], [IsPinned], [IsClosed], [ClosedReason], [ClosedById], [ClosedAt], [LastActivityAt], [CreatedAt], [UpdatedAt], [CoreId], [AssignedInstructorId], [IsPrivate], [TopicId]) VALUES (14, N'cau hoi 1', N'sql desing', NULL, 21, 8, N'Resolved', N'Database', N'Easy', 0, 1, 2, NULL, 0, 1, NULL, 21, CAST(N'2026-03-16T20:23:14.3293010' AS DateTime2), CAST(N'2026-03-16T13:21:59.8070051' AS DateTime2), CAST(N'2026-03-16T13:21:00.6677807' AS DateTime2), CAST(N'2026-03-16T20:23:14.3293636' AS DateTime2), 9, NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[Questions] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [CreatedAt]) VALUES (1, N'Admin', N'System administrator with full access', CAST(N'2026-02-26T10:40:30.9433333' AS DateTime2))
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [CreatedAt]) VALUES (2, N'Instructor', N'Course instructor/lecturer', CAST(N'2026-02-26T10:40:30.9433333' AS DateTime2))
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [CreatedAt]) VALUES (3, N'Student', N'SWP391 course student', CAST(N'2026-02-26T10:40:30.9433333' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Semesters] ON 

INSERT [dbo].[Semesters] ([SemesterId], [SemesterCode], [SemesterName], [StartDate], [EndDate], [IsActive], [IsCurrent], [CreatedAt], [UpdatedAt]) VALUES (1, N'FA24', N'Fall 2024', CAST(N'2024-09-01' AS Date), CAST(N'2024-12-31' AS Date), 0, 0, CAST(N'2026-02-26T17:17:51.0000000' AS DateTime2), CAST(N'2026-02-26T17:17:51.0000000' AS DateTime2))
INSERT [dbo].[Semesters] ([SemesterId], [SemesterCode], [SemesterName], [StartDate], [EndDate], [IsActive], [IsCurrent], [CreatedAt], [UpdatedAt]) VALUES (2, N'SP25', N'Spring 2025', CAST(N'2025-01-06' AS Date), CAST(N'2025-04-30' AS Date), 1, 0, CAST(N'2026-02-26T17:17:51.0000000' AS DateTime2), CAST(N'2026-02-26T17:17:51.0000000' AS DateTime2))
INSERT [dbo].[Semesters] ([SemesterId], [SemesterCode], [SemesterName], [StartDate], [EndDate], [IsActive], [IsCurrent], [CreatedAt], [UpdatedAt]) VALUES (3, N'SU25', N'Summer 2025', CAST(N'2025-05-05' AS Date), CAST(N'2025-08-31' AS Date), 1, 0, CAST(N'2026-02-26T17:17:51.0000000' AS DateTime2), CAST(N'2026-02-26T17:17:51.0000000' AS DateTime2))
INSERT [dbo].[Semesters] ([SemesterId], [SemesterCode], [SemesterName], [StartDate], [EndDate], [IsActive], [IsCurrent], [CreatedAt], [UpdatedAt]) VALUES (4, N'FA25', N'Fall 2025', CAST(N'2025-09-01' AS Date), CAST(N'2025-12-31' AS Date), NULL, 0, CAST(N'2026-02-26T17:17:51.0000000' AS DateTime2), CAST(N'2026-03-16T03:48:27.6866903' AS DateTime2))
INSERT [dbo].[Semesters] ([SemesterId], [SemesterCode], [SemesterName], [StartDate], [EndDate], [IsActive], [IsCurrent], [CreatedAt], [UpdatedAt]) VALUES (5, N'Sp2026', N'Spring2026', CAST(N'2026-01-01' AS Date), CAST(N'2026-05-30' AS Date), NULL, 1, CAST(N'2026-03-10T03:19:44.4788459' AS DateTime2), CAST(N'2026-03-16T03:48:32.8995157' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Semesters] OFF
GO
SET IDENTITY_INSERT [dbo].[TeamMembers] ON 

INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (1, 1, 6, N'Leader', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (2, 1, 7, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (3, 1, 8, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (4, 1, 9, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (5, 2, 10, N'Leader', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (6, 2, 11, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (7, 2, 12, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (8, 2, 13, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (9, 3, 14, N'Leader', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (10, 3, 15, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (11, 3, 16, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (12, 3, 17, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (13, 4, 18, N'Leader', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (14, 4, 19, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (15, 4, 20, N'Member', CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (16, 5, 3, N'Leader', CAST(N'2026-03-10T03:36:33.8984185' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (17, 5, 4, N'Member', CAST(N'2026-03-10T03:37:02.3718502' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (18, 5, 5, N'Member', CAST(N'2026-03-10T03:37:09.0256999' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (19, 6, 3, N'Leader', CAST(N'2026-03-16T03:03:45.1546241' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (20, 7, 3, N'Leader', CAST(N'2026-03-16T03:04:19.0312576' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (21, 8, 21, N'Leader', CAST(N'2026-03-16T13:17:30.6981249' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (22, 8, 12, N'Member', CAST(N'2026-03-16T13:17:47.9244266' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (23, 8, 6, N'Member', CAST(N'2026-03-16T13:17:57.9311172' AS DateTime2))
INSERT [dbo].[TeamMembers] ([TeamMemberId], [TeamId], [UserId], [Role], [JoinedAt]) VALUES (24, 8, 18, N'Member', CAST(N'2026-03-16T13:18:05.4049292' AS DateTime2))
SET IDENTITY_INSERT [dbo].[TeamMembers] OFF
GO
SET IDENTITY_INSERT [dbo].[Teams] ON 

INSERT [dbo].[Teams] ([TeamId], [TeamName], [TeamCode], [Description], [ProjectName], [LeaderId], [MentorId], [Semester], [IsActive], [CreatedAt], [UpdatedAt], [CoreId], [TopicId], [SemesterId]) VALUES (1, N'Team Alpha', N'SE1801-T01', N'Nhóm phát triển hệ thống bán hàng online', N'Shop Online Pro', 6, 3, N'FA25', 1, CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2), CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2), 1, 1, 4)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [TeamCode], [Description], [ProjectName], [LeaderId], [MentorId], [Semester], [IsActive], [CreatedAt], [UpdatedAt], [CoreId], [TopicId], [SemesterId]) VALUES (2, N'Team Beta', N'SE1801-T02', N'Nhóm phát triển hệ thống đặt lịch khám', N'HealthCare Booking', 10, 3, N'FA25', 1, CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2), CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2), 1, 2, 4)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [TeamCode], [Description], [ProjectName], [LeaderId], [MentorId], [Semester], [IsActive], [CreatedAt], [UpdatedAt], [CoreId], [TopicId], [SemesterId]) VALUES (3, N'Team Gamma', N'SE1802-T01', N'Nhóm phát triển nền tảng học trực tuyến', N'EduLearn Platform', 14, 4, N'FA25', 1, CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2), CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2), 2, 3, 4)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [TeamCode], [Description], [ProjectName], [LeaderId], [MentorId], [Semester], [IsActive], [CreatedAt], [UpdatedAt], [CoreId], [TopicId], [SemesterId]) VALUES (4, N'Team Delta', N'SE1802-T02', N'Nhóm phát triển hệ thống quản lý ký túc xá', N'Dormitory Manager', 18, 4, N'FA25', 1, CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2), CAST(N'2026-02-26T17:20:00.0000000' AS DateTime2), 2, 4, 4)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [TeamCode], [Description], [ProjectName], [LeaderId], [MentorId], [Semester], [IsActive], [CreatedAt], [UpdatedAt], [CoreId], [TopicId], [SemesterId]) VALUES (5, N'Nhom cua Trong NgHIA', N'TEAM-B6D621DF', N'fgdf', N'Online for Student', 3, NULL, NULL, 1, CAST(N'2026-03-10T03:36:33.7480566' AS DateTime2), CAST(N'2026-03-10T10:36:33.8600000' AS DateTime2), 6, 1, NULL)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [TeamCode], [Description], [ProjectName], [LeaderId], [MentorId], [Semester], [IsActive], [CreatedAt], [UpdatedAt], [CoreId], [TopicId], [SemesterId]) VALUES (6, N'Nhom cua Trong NgHIA 2', N'TEAM-13842085', NULL, N'Online for Student', 3, NULL, NULL, 1, CAST(N'2026-03-16T03:03:45.0559527' AS DateTime2), CAST(N'2026-03-16T10:03:45.1066667' AS DateTime2), 6, 3, NULL)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [TeamCode], [Description], [ProjectName], [LeaderId], [MentorId], [Semester], [IsActive], [CreatedAt], [UpdatedAt], [CoreId], [TopicId], [SemesterId]) VALUES (7, N'Nhom cua Trong NgHIA 2', N'TEAM-3D83F1A7', N'xzzcxz', N'Online for Student', 3, NULL, NULL, 1, CAST(N'2026-03-16T03:04:19.0199310' AS DateTime2), CAST(N'2026-03-16T10:04:19.0233333' AS DateTime2), 5, 3, NULL)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [TeamCode], [Description], [ProjectName], [LeaderId], [MentorId], [Semester], [IsActive], [CreatedAt], [UpdatedAt], [CoreId], [TopicId], [SemesterId]) VALUES (8, N'Nhom cua trieu', N'TEAM-E0A3CEFD', N'Online course', N'Online course', 21, NULL, NULL, 1, CAST(N'2026-03-16T13:17:30.6500136' AS DateTime2), CAST(N'2026-03-16T20:17:30.6733333' AS DateTime2), 9, 3, NULL)
SET IDENTITY_INSERT [dbo].[Teams] OFF
GO
SET IDENTITY_INSERT [dbo].[Topics] ON 

INSERT [dbo].[Topics] ([TopicId], [TopicCode], [TopicName], [Description], [SemesterId], [Category], [Difficulty], [MaxTeams], [CurrentTeams], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'SWP-01', N'Hệ thống quản lý bán hàng online', N'Xây dựng website thương mại điện tử với các tính năng quản lý sản phẩm, đơn hàng, thanh toán VNPay/Momo', 4, N'Web Application', N'Medium', 5, 1, 1, CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2), CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2))
INSERT [dbo].[Topics] ([TopicId], [TopicCode], [TopicName], [Description], [SemesterId], [Category], [Difficulty], [MaxTeams], [CurrentTeams], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'SWP-02', N'Hệ thống đặt lịch khám bệnh', N'Website đặt lịch khám bệnh online cho phòng khám/bệnh viện', 4, N'Web Application', N'Medium', 5, 1, 1, CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2), CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2))
INSERT [dbo].[Topics] ([TopicId], [TopicCode], [TopicName], [Description], [SemesterId], [Category], [Difficulty], [MaxTeams], [CurrentTeams], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'SWP-03', N'Nền tảng học trực tuyến', N'Hệ thống E-learning với video courses, quiz, certificate', 4, N'Web Application', N'Hard', 4, 1, 1, CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2), CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2))
INSERT [dbo].[Topics] ([TopicId], [TopicCode], [TopicName], [Description], [SemesterId], [Category], [Difficulty], [MaxTeams], [CurrentTeams], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'SWP-04', N'Hệ thống quản lý ký túc xá', N'Quản lý phòng, sinh viên, hóa đơn điện nước, báo cáo sự cố', 4, N'Web Application', N'Easy', 6, 1, 1, CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2), CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2))
INSERT [dbo].[Topics] ([TopicId], [TopicCode], [TopicName], [Description], [SemesterId], [Category], [Difficulty], [MaxTeams], [CurrentTeams], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'SWP-05', N'Ứng dụng đặt sân thể thao', N'Đặt sân bóng đá, cầu lông, tennis với thanh toán online', 4, N'Web Application', N'Medium', 5, 0, 1, CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2), CAST(N'2026-03-16T02:54:44.0654869' AS DateTime2))
INSERT [dbo].[Topics] ([TopicId], [TopicCode], [TopicName], [Description], [SemesterId], [Category], [Difficulty], [MaxTeams], [CurrentTeams], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'SWP-06', N'Hệ thống Q&A cho sinh viên', N'Nền tảng hỏi đáp kiến thức giữa sinh viên và giảng viên', 4, N'Web Application', N'Medium', 4, 0, 1, CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2), CAST(N'2026-02-26T17:17:51.0266667' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Topics] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (1, N'admin@swp391.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'System Administrator', N'Admin', NULL, NULL, NULL, 1, 1, NULL, CAST(N'2026-03-16T20:29:37.8400450' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-03-16T13:25:54.0926934' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (2, N'instructor@swp391.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Nguyen Van Instructor', N'Instructor', NULL, NULL, NULL, 2, 1, 1, CAST(N'2026-03-16T20:28:30.8940903' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (3, N'Student1@swp391.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Tran Thi Mentor', N'Student', NULL, NULL, NULL, 3, 1, 1, CAST(N'2026-03-16T20:41:35.4973016' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (4, N'mentor2@swp391.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Le Van Mentor', N'Mentor Le', NULL, N'5 năm kinh nghiệm phát triển web', NULL, 2, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (5, N'mentor3@swp391.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Pham Thi Mentor', N'Mentor Pham', NULL, N'Chuyên gia .NET và Azure', NULL, 2, 1, 1, CAST(N'2026-03-16T20:15:05.6107302' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (6, N'se171001@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Nguyen Van An', N'An Nguyen', NULL, N'Sinh viên SE K17', N'SE171001', 3, 1, 1, CAST(N'2026-03-10T10:21:06.0263932' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (7, N'se171002@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Tran Thi Binh', N'Binh Tran', NULL, N'Sinh viên SE K17', N'SE171002', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (8, N'se171003@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Le Van Cuong', N'Cuong Le', NULL, N'Sinh viên SE K17, yêu thích backend', N'SE171003', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (9, N'se171004@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Pham Thi Dung', N'Dung Pham', NULL, N'Sinh viên SE K17', N'SE171004', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (10, N'se171005@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Hoang Van Em', N'Em Hoang', NULL, N'Sinh viên SE K17, frontend developer', N'SE171005', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (11, N'se171006@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Vo Thi Phuong', N'Phuong Vo', NULL, N'Sinh viên SE K17', N'SE171006', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (12, N'se171007@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Dao Van Giang', N'Giang Dao', NULL, N'Sinh viên SE K17', N'SE171007', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (13, N'se171008@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Nguyen Thi Huong', N'Huong Nguyen', NULL, N'Sinh viên SE K17', N'SE171008', 3, 1, 1, CAST(N'2026-03-16T10:59:03.4451941' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (14, N'se171009@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Tran Van Ich', N'Ich Tran', NULL, N'Sinh viên SE K17', N'SE171009', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (15, N'se171010@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Le Thi Kim', N'Kim Le', NULL, N'Sinh viên SE K17', N'SE171010', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (16, N'se171011@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Pham Van Long', N'Long Pham', NULL, N'Sinh viên SE K17', N'SE171011', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (17, N'se171012@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Hoang Thi Mai', N'Mai Hoang', NULL, N'Sinh viên SE K17', N'SE171012', 3, 1, 1, CAST(N'2026-03-16T20:16:07.5090515' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (18, N'se171013@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Vo Van Nam', N'Nam Vo', NULL, N'Sinh viên SE K17', N'SE171013', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (19, N'se171014@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Dao Thi Oanh', N'Oanh Dao', NULL, N'Sinh viên SE K17', N'SE171014', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (20, N'se171015@fpt.edu.vn', N'AQAAAAEAACcQAAAAEL8k5...', N'Nguyen Van Phuc', N'Phuc Nguyen', NULL, N'Sinh viên SE K17', N'SE171015', 3, 1, 1, NULL, CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2), CAST(N'2026-02-26T10:40:30.9666667' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Email], [PasswordHash], [FullName], [DisplayName], [AvatarUrl], [Bio], [StudentId], [RoleId], [IsActive], [IsEmailVerified], [LastLoginAt], [CreatedAt], [UpdatedAt]) VALUES (21, N'trieuse1710071@fpt.edu.vn', N'123456', N'trieu', N'test', NULL, NULL, N'e171007', 3, 1, 0, CAST(N'2026-03-16T20:16:36.7864822' AS DateTime2), CAST(N'2026-03-16T03:40:08.1492818' AS DateTime2), CAST(N'2026-03-16T10:40:08.1733333' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Answers_AuthorId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Answers_AuthorId] ON [dbo].[Answers]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Answers_IsAccepted]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Answers_IsAccepted] ON [dbo].[Answers]
(
	[IsAccepted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Answers_QuestionId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Answers_QuestionId] ON [dbo].[Answers]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChatMessages_ChatRoomId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChatMessages_ChatRoomId] ON [dbo].[ChatMessages]
(
	[ChatRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChatMessages_CreatedAt]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChatMessages_CreatedAt] ON [dbo].[ChatMessages]
(
	[CreatedAt] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_AnswerId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_AnswerId] ON [dbo].[Comments]
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_AuthorId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_AuthorId] ON [dbo].[Comments]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_QuestionId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_QuestionId] ON [dbo].[Comments]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_CoreManagers]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[CoreManagers] ADD  CONSTRAINT [UQ_CoreManagers] UNIQUE NONCLUSTERED 
(
	[CoreId] ASC,
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Cores_Code_Semester]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[Cores] ADD  CONSTRAINT [UQ_Cores_Code_Semester] UNIQUE NONCLUSTERED 
(
	[CoreCode] ASC,
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cores_InstructorId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cores_InstructorId] ON [dbo].[Cores]
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cores_SemesterId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cores_SemesterId] ON [dbo].[Cores]
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NotificationRecipients_IsRead]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_NotificationRecipients_IsRead] ON [dbo].[NotificationRecipients]
(
	[IsRead] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NotificationRecipients_UserId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_NotificationRecipients_UserId] ON [dbo].[NotificationRecipients]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_QuestionFollowers]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[QuestionFollowers] ADD  CONSTRAINT [UQ_QuestionFollowers] UNIQUE NONCLUSTERED 
(
	[QuestionId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_AssignedInstructorId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_AssignedInstructorId] ON [dbo].[Questions]
(
	[AssignedInstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_AuthorId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_AuthorId] ON [dbo].[Questions]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Questions_Category]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_Category] ON [dbo].[Questions]
(
	[Category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_CoreId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_CoreId] ON [dbo].[Questions]
(
	[CoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_CreatedAt]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_CreatedAt] ON [dbo].[Questions]
(
	[CreatedAt] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_LastActivityAt]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_LastActivityAt] ON [dbo].[Questions]
(
	[LastActivityAt] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Questions_Status]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_Status] ON [dbo].[Questions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_TopicId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_TopicId] ON [dbo].[Questions]
(
	[TopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_ViewCount]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_ViewCount] ON [dbo].[Questions]
(
	[ViewCount] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__8A2B616025DF98CD]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Semester__513151C944B3EE65]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[Semesters] ADD UNIQUE NONCLUSTERED 
(
	[SemesterCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Semesters_IsActive]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Semesters_IsActive] ON [dbo].[Semesters]
(
	[IsActive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Semesters_IsCurrent]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Semesters_IsCurrent] ON [dbo].[Semesters]
(
	[IsCurrent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_TeamMembers]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[TeamMembers] ADD  CONSTRAINT [UQ_TeamMembers] UNIQUE NONCLUSTERED 
(
	[TeamId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Teams__5501350846AFA1AB]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[Teams] ADD UNIQUE NONCLUSTERED 
(
	[TeamCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Teams_CoreId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Teams_CoreId] ON [dbo].[Teams]
(
	[CoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Teams_SemesterId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Teams_SemesterId] ON [dbo].[Teams]
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Teams_TopicId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Teams_TopicId] ON [dbo].[Teams]
(
	[TopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Topics_Code_Semester]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[Topics] ADD  CONSTRAINT [UQ_Topics_Code_Semester] UNIQUE NONCLUSTERED 
(
	[TopicCode] ASC,
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Topics_IsActive]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Topics_IsActive] ON [dbo].[Topics]
(
	[IsActive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Topics_SemesterId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Topics_SemesterId] ON [dbo].[Topics]
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D1053484BB3A3F]    Script Date: 3/18/2026 2:08:51 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_IsActive]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_IsActive] ON [dbo].[Users]
(
	[IsActive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RoleId]    Script Date: 3/18/2026 2:08:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_RoleId] ON [dbo].[Users]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answers] ADD  DEFAULT ((0)) FOR [IsAccepted]
GO
ALTER TABLE [dbo].[Answers] ADD  DEFAULT ((0)) FOR [IsMentorAnswer]
GO
ALTER TABLE [dbo].[Answers] ADD  DEFAULT ((0)) FOR [CommentCount]
GO
ALTER TABLE [dbo].[Answers] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Answers] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[ChatMessages] ADD  DEFAULT ('Text') FOR [MessageType]
GO
ALTER TABLE [dbo].[ChatMessages] ADD  DEFAULT ((0)) FOR [IsEdited]
GO
ALTER TABLE [dbo].[ChatMessages] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ChatMessages] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ChatMessages] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[ChatRooms] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ChatRooms] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT ((0)) FOR [IsEdited]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[CoreManagers] ADD  DEFAULT ('Manager') FOR [ManagerType]
GO
ALTER TABLE [dbo].[CoreManagers] ADD  DEFAULT (getdate()) FOR [AssignedAt]
GO
ALTER TABLE [dbo].[Cores] ADD  DEFAULT ((10)) FOR [MaxTeams]
GO
ALTER TABLE [dbo].[Cores] ADD  DEFAULT ((0)) FOR [CurrentTeams]
GO
ALTER TABLE [dbo].[Cores] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Cores] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Cores] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[NotificationRecipients] ADD  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[NotificationRecipients] ADD  DEFAULT ((0)) FOR [IsEmailSent]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[QuestionFollowers] ADD  DEFAULT ((1)) FOR [NotifyOnAnswer]
GO
ALTER TABLE [dbo].[QuestionFollowers] ADD  DEFAULT ((1)) FOR [NotifyOnComment]
GO
ALTER TABLE [dbo].[QuestionFollowers] ADD  DEFAULT (getdate()) FOR [FollowedAt]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ('Open') FOR [Status]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [ViewCount]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [AnswerCount]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [CommentCount]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [IsPinned]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [IsClosed]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT (getdate()) FOR [LastActivityAt]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [IsPrivate]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Semesters] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Semesters] ADD  DEFAULT ((0)) FOR [IsCurrent]
GO
ALTER TABLE [dbo].[Semesters] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Semesters] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[TeamMembers] ADD  DEFAULT ('Member') FOR [Role]
GO
ALTER TABLE [dbo].[TeamMembers] ADD  DEFAULT (getdate()) FOR [JoinedAt]
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Topics] ADD  DEFAULT ((0)) FOR [CurrentTeams]
GO
ALTER TABLE [dbo].[Topics] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Topics] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Topics] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsEmailVerified]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Author]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([QuestionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Question]
GO
ALTER TABLE [dbo].[ChatMessages]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessages_Reply] FOREIGN KEY([ReplyToMessageId])
REFERENCES [dbo].[ChatMessages] ([ChatMessageId])
GO
ALTER TABLE [dbo].[ChatMessages] CHECK CONSTRAINT [FK_ChatMessages_Reply]
GO
ALTER TABLE [dbo].[ChatMessages]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessages_Room] FOREIGN KEY([ChatRoomId])
REFERENCES [dbo].[ChatRooms] ([ChatRoomId])
GO
ALTER TABLE [dbo].[ChatMessages] CHECK CONSTRAINT [FK_ChatMessages_Room]
GO
ALTER TABLE [dbo].[ChatMessages]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessages_Sender] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ChatMessages] CHECK CONSTRAINT [FK_ChatMessages_Sender]
GO
ALTER TABLE [dbo].[ChatRooms]  WITH CHECK ADD  CONSTRAINT [FK_ChatRooms_Creator] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ChatRooms] CHECK CONSTRAINT [FK_ChatRooms_Creator]
GO
ALTER TABLE [dbo].[ChatRooms]  WITH CHECK ADD  CONSTRAINT [FK_ChatRooms_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([TeamId])
GO
ALTER TABLE [dbo].[ChatRooms] CHECK CONSTRAINT [FK_ChatRooms_Team]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Answer] FOREIGN KEY([AnswerId])
REFERENCES [dbo].[Answers] ([AnswerId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Answer]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Author]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Parent] FOREIGN KEY([ParentCommentId])
REFERENCES [dbo].[Comments] ([CommentId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Parent]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([QuestionId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Question]
GO
ALTER TABLE [dbo].[CoreManagers]  WITH CHECK ADD  CONSTRAINT [FK_CoreManagers_Core] FOREIGN KEY([CoreId])
REFERENCES [dbo].[Cores] ([CoreId])
GO
ALTER TABLE [dbo].[CoreManagers] CHECK CONSTRAINT [FK_CoreManagers_Core]
GO
ALTER TABLE [dbo].[CoreManagers]  WITH CHECK ADD  CONSTRAINT [FK_CoreManagers_Manager] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[CoreManagers] CHECK CONSTRAINT [FK_CoreManagers_Manager]
GO
ALTER TABLE [dbo].[Cores]  WITH CHECK ADD  CONSTRAINT [FK_Cores_Instructor] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Cores] CHECK CONSTRAINT [FK_Cores_Instructor]
GO
ALTER TABLE [dbo].[Cores]  WITH CHECK ADD  CONSTRAINT [FK_Cores_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semesters] ([SemesterId])
GO
ALTER TABLE [dbo].[Cores] CHECK CONSTRAINT [FK_Cores_Semester]
GO
ALTER TABLE [dbo].[NotificationRecipients]  WITH CHECK ADD  CONSTRAINT [FK_NotificationRecipients_Notification] FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notifications] ([NotificationId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NotificationRecipients] CHECK CONSTRAINT [FK_NotificationRecipients_Notification]
GO
ALTER TABLE [dbo].[NotificationRecipients]  WITH CHECK ADD  CONSTRAINT [FK_NotificationRecipients_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[NotificationRecipients] CHECK CONSTRAINT [FK_NotificationRecipients_User]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Creator] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Creator]
GO
ALTER TABLE [dbo].[QuestionFollowers]  WITH CHECK ADD  CONSTRAINT [FK_QuestionFollowers_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([QuestionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuestionFollowers] CHECK CONSTRAINT [FK_QuestionFollowers_Question]
GO
ALTER TABLE [dbo].[QuestionFollowers]  WITH CHECK ADD  CONSTRAINT [FK_QuestionFollowers_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[QuestionFollowers] CHECK CONSTRAINT [FK_QuestionFollowers_User]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_AssignedInstructor] FOREIGN KEY([AssignedInstructorId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_AssignedInstructor]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Author]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_ClosedBy] FOREIGN KEY([ClosedById])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_ClosedBy]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Core] FOREIGN KEY([CoreId])
REFERENCES [dbo].[Cores] ([CoreId])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Core]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([TeamId])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Team]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Topic] FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topics] ([TopicId])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Topic]
GO
ALTER TABLE [dbo].[TeamMembers]  WITH CHECK ADD  CONSTRAINT [FK_TeamMembers_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([TeamId])
GO
ALTER TABLE [dbo].[TeamMembers] CHECK CONSTRAINT [FK_TeamMembers_Team]
GO
ALTER TABLE [dbo].[TeamMembers]  WITH CHECK ADD  CONSTRAINT [FK_TeamMembers_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[TeamMembers] CHECK CONSTRAINT [FK_TeamMembers_User]
GO
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Core] FOREIGN KEY([CoreId])
REFERENCES [dbo].[Cores] ([CoreId])
GO
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Core]
GO
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Leader] FOREIGN KEY([LeaderId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Leader]
GO
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Mentor] FOREIGN KEY([MentorId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Mentor]
GO
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semesters] ([SemesterId])
GO
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Semester]
GO
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Topic] FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topics] ([TopicId])
GO
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Topic]
GO
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD  CONSTRAINT [FK_Topics_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semesters] ([SemesterId])
GO
ALTER TABLE [dbo].[Topics] CHECK CONSTRAINT [FK_Topics_Semester]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [CK_Comments_Target] CHECK  (([QuestionId] IS NOT NULL AND [AnswerId] IS NULL OR [QuestionId] IS NULL AND [AnswerId] IS NOT NULL))
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [CK_Comments_Target]
GO
/****** Object:  StoredProcedure [dbo].[sp_AcceptAnswer]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Accept answer
CREATE   PROCEDURE [dbo].[sp_AcceptAnswer]
    @AnswerId INT,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @QuestionId INT;
    DECLARE @QuestionAuthorId INT;
    DECLARE @AnswerAuthorId INT;
    
    SELECT @QuestionId = QuestionId, @AnswerAuthorId = AuthorId FROM Answers WHERE AnswerId = @AnswerId;
    SELECT @QuestionAuthorId = AuthorId FROM Questions WHERE QuestionId = @QuestionId;
    
    -- Only question author can accept
    IF @QuestionAuthorId != @UserId
    BEGIN
        RAISERROR('Only the question author can accept an answer', 16, 1);
        RETURN;
    END
    
    -- Remove previous accepted answer
    UPDATE Answers SET IsAccepted = 0 WHERE QuestionId = @QuestionId;
    
    -- Accept this answer
    UPDATE Answers SET IsAccepted = 1 WHERE AnswerId = @AnswerId;
    
    -- Update question
    UPDATE Questions 
    SET Status = 'Accepted', AcceptedAnswerId = @AnswerId, LastActivityAt = GETDATE()
    WHERE QuestionId = @QuestionId;
    
    -- Award reputation to answer author
    EXEC sp_UpdateUserReputation @AnswerAuthorId, 25;
    
    -- Notify answer author
    IF @AnswerAuthorId != @UserId
    BEGIN
        DECLARE @NotificationId INT;
        INSERT INTO Notifications (Title, Message, NotificationType, ReferenceType, ReferenceId, CreatedById)
        VALUES ('Answer Accepted', 'Your answer has been accepted!', 'AnswerAccepted', 'Answer', @AnswerId, @UserId);
        SET @NotificationId = SCOPE_IDENTITY();
        
        INSERT INTO NotificationRecipients (NotificationId, UserId)
        VALUES (@NotificationId, @AnswerAuthorId);
    END
    
    SELECT 'Answer accepted successfully' AS Message;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateAnswer]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- ANSWER PROCEDURES
-- =============================================

-- Create new answer
CREATE   PROCEDURE [dbo].[sp_CreateAnswer]
    @QuestionId INT,
    @AuthorId INT,
    @Body NVARCHAR(MAX),
    @IsMentorAnswer BIT = 0,
    @IsAIGenerated BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @AnswerId INT;
    
    INSERT INTO Answers (QuestionId, AuthorId, Body, IsMentorAnswer, IsAIGenerated)
    VALUES (@QuestionId, @AuthorId, @Body, @IsMentorAnswer, @IsAIGenerated);
    
    SET @AnswerId = SCOPE_IDENTITY();
    
    -- Update question
    UPDATE Questions 
    SET AnswerCount = AnswerCount + 1,
        Status = CASE WHEN Status = 'Open' THEN 'Answered' ELSE Status END,
        LastActivityAt = GETDATE()
    WHERE QuestionId = @QuestionId;
    
    -- Award reputation
    IF @IsAIGenerated = 0
        EXEC sp_UpdateUserReputation @AuthorId, 10;
    
    -- Create notification for question author
    DECLARE @QuestionAuthorId INT;
    DECLARE @QuestionTitle NVARCHAR(500);
    SELECT @QuestionAuthorId = AuthorId, @QuestionTitle = Title FROM Questions WHERE QuestionId = @QuestionId;
    
    IF @QuestionAuthorId != @AuthorId
    BEGIN
        DECLARE @NotificationId INT;
        INSERT INTO Notifications (Title, Message, NotificationType, ReferenceType, ReferenceId, CreatedById)
        VALUES ('New Answer', 'Your question "' + LEFT(@QuestionTitle, 50) + '..." received a new answer', 
                'NewAnswer', 'Question', @QuestionId, @AuthorId);
        SET @NotificationId = SCOPE_IDENTITY();
        
        INSERT INTO NotificationRecipients (NotificationId, UserId)
        VALUES (@NotificationId, @QuestionAuthorId);
    END
    
    SELECT @AnswerId AS AnswerId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateComment]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- COMMENT PROCEDURES
-- =============================================

-- Create comment
CREATE   PROCEDURE [dbo].[sp_CreateComment]
    @Body NVARCHAR(1000),
    @AuthorId INT,
    @QuestionId INT = NULL,
    @AnswerId INT = NULL,
    @ParentCommentId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @QuestionId IS NULL AND @AnswerId IS NULL
    BEGIN
        RAISERROR('Comment must be on a question or answer', 16, 1);
        RETURN;
    END
    
    DECLARE @CommentId INT;
    
    INSERT INTO Comments (Body, AuthorId, QuestionId, AnswerId, ParentCommentId)
    VALUES (@Body, @AuthorId, @QuestionId, @AnswerId, @ParentCommentId);
    
    SET @CommentId = SCOPE_IDENTITY();
    
    -- Update counts
    IF @QuestionId IS NOT NULL
    BEGIN
        UPDATE Questions SET CommentCount = CommentCount + 1, LastActivityAt = GETDATE() 
        WHERE QuestionId = @QuestionId;
    END
    ELSE
    BEGIN
        UPDATE Answers SET CommentCount = CommentCount + 1 WHERE AnswerId = @AnswerId;
        UPDATE Questions SET LastActivityAt = GETDATE() 
        WHERE QuestionId = (SELECT QuestionId FROM Answers WHERE AnswerId = @AnswerId);
    END
    
    SELECT @CommentId AS CommentId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateMeeting]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- MEETING PROCEDURES
-- =============================================

-- Create meeting
CREATE   PROCEDURE [dbo].[sp_CreateMeeting]
    @Title NVARCHAR(200),
    @Description NVARCHAR(1000) = NULL,
    @MeetingType NVARCHAR(50),
    @OrganizerId INT,
    @StartTime DATETIME2,
    @EndTime DATETIME2,
    @MeetingUrl NVARCHAR(500) = NULL,
    @Location NVARCHAR(200) = NULL,
    @MaxParticipants INT = NULL,
    @TeamId INT = NULL,
    @Color NVARCHAR(20) = '#0078d4'
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @MeetingId INT;
    
    INSERT INTO Meetings (Title, Description, MeetingType, OrganizerId, StartTime, EndTime, 
                          MeetingUrl, Location, MaxParticipants, TeamId, Color)
    VALUES (@Title, @Description, @MeetingType, @OrganizerId, @StartTime, @EndTime,
            @MeetingUrl, @Location, @MaxParticipants, @TeamId, @Color);
    
    SET @MeetingId = SCOPE_IDENTITY();
    
    -- Auto-register organizer
    INSERT INTO MeetingParticipants (MeetingId, UserId) VALUES (@MeetingId, @OrganizerId);
    UPDATE Meetings SET CurrentParticipants = 1 WHERE MeetingId = @MeetingId;
    
    SELECT @MeetingId AS MeetingId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateQuestion]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- QUESTION PROCEDURES
-- =============================================

-- Create new question
CREATE   PROCEDURE [dbo].[sp_CreateQuestion]
    @Title NVARCHAR(500),
    @Body NVARCHAR(MAX),
    @AuthorId INT,
    @Category NVARCHAR(50) = NULL,
    @Difficulty NVARCHAR(20) = NULL,
    @TeamId INT = NULL,
    @TagIds NVARCHAR(MAX) = NULL -- Comma-separated tag IDs
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    DECLARE @QuestionId INT;
    DECLARE @Excerpt NVARCHAR(500) = LEFT(@Body, 500);
    
    -- Insert question
    INSERT INTO Questions (Title, Body, Excerpt, AuthorId, Category, Difficulty, TeamId)
    VALUES (@Title, @Body, @Excerpt, @AuthorId, @Category, @Difficulty, @TeamId);
    
    SET @QuestionId = SCOPE_IDENTITY();
    
    -- Insert tags
    IF @TagIds IS NOT NULL AND LEN(@TagIds) > 0
    BEGIN
        INSERT INTO QuestionTags (QuestionId, TagId)
        SELECT @QuestionId, value
        FROM STRING_SPLIT(@TagIds, ',')
        WHERE value IN (SELECT TagId FROM Tags);
        
        -- Update tag question counts
        UPDATE Tags
        SET QuestionCount = QuestionCount + 1
        WHERE TagId IN (SELECT value FROM STRING_SPLIT(@TagIds, ','));
    END
    
    -- Auto-follow the question for the author
    INSERT INTO QuestionFollowers (QuestionId, UserId)
    VALUES (@QuestionId, @AuthorId);
    
    -- Award reputation points for asking
    EXEC sp_UpdateUserReputation @AuthorId, 2;
    
    COMMIT TRANSACTION;
    
    SELECT @QuestionId AS QuestionId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateUser]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- USER PROCEDURES
-- =============================================

-- Create new user
CREATE   PROCEDURE [dbo].[sp_CreateUser]
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(255),
    @FullName NVARCHAR(100),
    @DisplayName NVARCHAR(50) = NULL,
    @StudentId NVARCHAR(20) = NULL,
    @RoleId INT = 4 -- Default: Student
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        RAISERROR('Email already exists', 16, 1);
        RETURN;
    END
    
    INSERT INTO Users (Email, PasswordHash, FullName, DisplayName, StudentId, RoleId)
    VALUES (@Email, @PasswordHash, @FullName, ISNULL(@DisplayName, @FullName), @StudentId, @RoleId);
    
    SELECT SCOPE_IDENTITY() AS UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAnswersByQuestionId]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get answers for a question
CREATE   PROCEDURE [dbo].[sp_GetAnswersByQuestionId]
    @QuestionId INT,
    @ViewerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        a.*,
        u.DisplayName AS AuthorName,
        u.AvatarUrl AS AuthorAvatar,
        u.ReputationPoints AS AuthorReputation,
        r.RoleName AS AuthorRole,
        CASE WHEN @ViewerId IS NOT NULL AND EXISTS(
            SELECT 1 FROM AnswerVotes WHERE AnswerId = a.AnswerId AND UserId = @ViewerId
        ) THEN (SELECT VoteType FROM AnswerVotes WHERE AnswerId = a.AnswerId AND UserId = @ViewerId) ELSE 0 END AS UserVote
    FROM Answers a
    INNER JOIN Users u ON a.AuthorId = u.UserId
    INNER JOIN Roles r ON u.RoleId = r.RoleId
    WHERE a.QuestionId = @QuestionId
    ORDER BY a.IsAccepted DESC, a.IsMentorAnswer DESC, a.UpvoteCount DESC, a.CreatedAt ASC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetComments]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get comments
CREATE   PROCEDURE [dbo].[sp_GetComments]
    @QuestionId INT = NULL,
    @AnswerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.*,
        u.DisplayName AS AuthorName,
        u.AvatarUrl AS AuthorAvatar,
        u.ReputationPoints AS AuthorReputation
    FROM Comments c
    INNER JOIN Users u ON c.AuthorId = u.UserId
    WHERE (@QuestionId IS NOT NULL AND c.QuestionId = @QuestionId)
       OR (@AnswerId IS NOT NULL AND c.AnswerId = @AnswerId)
    ORDER BY c.CreatedAt ASC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDashboardStats]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- DASHBOARD / STATISTICS PROCEDURES
-- =============================================

-- Get dashboard statistics
CREATE   PROCEDURE [dbo].[sp_GetDashboardStats]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        (SELECT COUNT(*) FROM Questions WHERE IsClosed = 0) AS TotalQuestions,
        (SELECT COUNT(*) FROM Questions WHERE AnswerCount = 0 AND IsClosed = 0) AS UnansweredQuestions,
        (SELECT COUNT(*) FROM Questions WHERE Status = 'Accepted') AS AcceptedQuestions,
        (SELECT COUNT(*) FROM Answers) AS TotalAnswers,
        (SELECT COUNT(*) FROM Users WHERE IsActive = 1) AS TotalUsers,
        (SELECT COUNT(*) FROM Tags WHERE IsActive = 1) AS TotalTags,
        (SELECT COUNT(*) FROM Meetings WHERE StartTime >= GETDATE()) AS UpcomingMeetings;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetMeetings]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get meetings by date range
CREATE   PROCEDURE [dbo].[sp_GetMeetings]
    @StartDate DATETIME2,
    @EndDate DATETIME2,
    @UserId INT = NULL,
    @TeamId INT = NULL,
    @MeetingType NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        m.*,
        u.DisplayName AS OrganizerName,
        u.AvatarUrl AS OrganizerAvatar,
        t.TeamName,
        CASE WHEN @UserId IS NOT NULL AND EXISTS(
            SELECT 1 FROM MeetingParticipants WHERE MeetingId = m.MeetingId AND UserId = @UserId
        ) THEN 1 ELSE 0 END AS IsRegistered
    FROM Meetings m
    INNER JOIN Users u ON m.OrganizerId = u.UserId
    LEFT JOIN Teams t ON m.TeamId = t.TeamId
    WHERE m.StartTime >= @StartDate AND m.StartTime <= @EndDate
        AND m.Status != 'Cancelled'
        AND (@TeamId IS NULL OR m.TeamId = @TeamId)
        AND (@MeetingType IS NULL OR m.MeetingType = @MeetingType)
    ORDER BY m.StartTime ASC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetQuestionById]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get single question with full details
CREATE   PROCEDURE [dbo].[sp_GetQuestionById]
    @QuestionId INT,
    @ViewerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Increment view count
    UPDATE Questions SET ViewCount = ViewCount + 1 WHERE QuestionId = @QuestionId;
    
    -- Get question details
    SELECT 
        q.*,
        u.DisplayName AS AuthorName,
        u.AvatarUrl AS AuthorAvatar,
        u.ReputationPoints AS AuthorReputation,
        u.Bio AS AuthorBio,
        t.TeamName,
        (SELECT COUNT(*) FROM QuestionFollowers WHERE QuestionId = @QuestionId) AS FollowerCount,
        CASE WHEN @ViewerId IS NOT NULL AND EXISTS(
            SELECT 1 FROM QuestionFollowers WHERE QuestionId = @QuestionId AND UserId = @ViewerId
        ) THEN 1 ELSE 0 END AS IsFollowing,
        CASE WHEN @ViewerId IS NOT NULL AND EXISTS(
            SELECT 1 FROM QuestionVotes WHERE QuestionId = @QuestionId AND UserId = @ViewerId
        ) THEN (SELECT VoteType FROM QuestionVotes WHERE QuestionId = @QuestionId AND UserId = @ViewerId) ELSE 0 END AS UserVote
    FROM Questions q
    INNER JOIN Users u ON q.AuthorId = u.UserId
    LEFT JOIN Teams t ON q.TeamId = t.TeamId
    WHERE q.QuestionId = @QuestionId;
    
    -- Get tags
    SELECT t.TagId, t.TagName, t.Slug
    FROM QuestionTags qt
    INNER JOIN Tags t ON qt.TagId = t.TagId
    WHERE qt.QuestionId = @QuestionId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetQuestions]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get questions with filtering and pagination
CREATE   PROCEDURE [dbo].[sp_GetQuestions]
    @Page INT = 1,
    @PageSize INT = 20,
    @Category NVARCHAR(50) = NULL,
    @Status NVARCHAR(20) = NULL,
    @TagId INT = NULL,
    @AuthorId INT = NULL,
    @SearchQuery NVARCHAR(200) = NULL,
    @SortBy NVARCHAR(20) = 'updated' -- newest, updated, votes, answers, unanswered
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@Page - 1) * @PageSize;
    
    WITH QuestionsCTE AS (
        SELECT 
            q.QuestionId,
            q.Title,
            q.Excerpt,
            q.Status,
            q.Category,
            q.Difficulty,
            q.ViewCount,
            q.AnswerCount,
            q.UpvoteCount,
            q.DownvoteCount,
            q.CreatedAt,
            q.LastActivityAt,
            u.UserId AS AuthorId,
            u.DisplayName AS AuthorName,
            u.AvatarUrl AS AuthorAvatar,
            u.ReputationPoints AS AuthorReputation,
            ROW_NUMBER() OVER (
                ORDER BY 
                    CASE WHEN @SortBy = 'newest' THEN q.CreatedAt END DESC,
                    CASE WHEN @SortBy = 'updated' THEN q.LastActivityAt END DESC,
                    CASE WHEN @SortBy = 'votes' THEN q.UpvoteCount END DESC,
                    CASE WHEN @SortBy = 'answers' THEN q.AnswerCount END DESC,
                    CASE WHEN @SortBy = 'unanswered' AND q.AnswerCount = 0 THEN q.CreatedAt END DESC,
                    q.LastActivityAt DESC
            ) AS RowNum
        FROM Questions q
        INNER JOIN Users u ON q.AuthorId = u.UserId
        LEFT JOIN QuestionTags qt ON q.QuestionId = qt.QuestionId
        WHERE q.IsClosed = 0
            AND (@Category IS NULL OR q.Category = @Category)
            AND (@Status IS NULL OR q.Status = @Status)
            AND (@TagId IS NULL OR qt.TagId = @TagId)
            AND (@AuthorId IS NULL OR q.AuthorId = @AuthorId)
            AND (@SearchQuery IS NULL OR q.Title LIKE '%' + @SearchQuery + '%' OR q.Body LIKE '%' + @SearchQuery + '%')
            AND (@SortBy != 'unanswered' OR q.AnswerCount = 0)
        GROUP BY q.QuestionId, q.Title, q.Excerpt, q.Status, q.Category, q.Difficulty,
                 q.ViewCount, q.AnswerCount, q.UpvoteCount, q.DownvoteCount, q.CreatedAt,
                 q.LastActivityAt, u.UserId, u.DisplayName, u.AvatarUrl, u.ReputationPoints
    )
    SELECT 
        qc.*,
        (SELECT STRING_AGG(t.TagName, ',') FROM QuestionTags qt 
         INNER JOIN Tags t ON qt.TagId = t.TagId 
         WHERE qt.QuestionId = qc.QuestionId) AS Tags,
        (SELECT COUNT(*) FROM QuestionsCTE) AS TotalCount
    FROM QuestionsCTE qc
    WHERE RowNum > @Offset AND RowNum <= @Offset + @PageSize
    ORDER BY RowNum;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTags]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- TAG PROCEDURES
-- =============================================

-- Get all tags with counts
CREATE   PROCEDURE [dbo].[sp_GetTags]
    @Category NVARCHAR(50) = NULL,
    @SearchQuery NVARCHAR(100) = NULL,
    @Page INT = 1,
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@Page - 1) * @PageSize;
    
    SELECT 
        t.*,
        (SELECT COUNT(*) FROM Tags 
         WHERE IsActive = 1 
         AND (@Category IS NULL OR Category = @Category)
         AND (@SearchQuery IS NULL OR TagName LIKE '%' + @SearchQuery + '%')) AS TotalCount
    FROM Tags t
    WHERE t.IsActive = 1
        AND (@Category IS NULL OR t.Category = @Category)
        AND (@SearchQuery IS NULL OR t.TagName LIKE '%' + @SearchQuery + '%')
    ORDER BY t.QuestionCount DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserByEmail]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get user by email (for login)
CREATE   PROCEDURE [dbo].[sp_GetUserByEmail]
    @Email NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT u.*, r.RoleName
    FROM Users u
    INNER JOIN Roles r ON u.RoleId = r.RoleId
    WHERE u.Email = @Email AND u.IsActive = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserNotifications]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- NOTIFICATION PROCEDURES
-- =============================================

-- Get user notifications
CREATE   PROCEDURE [dbo].[sp_GetUserNotifications]
    @UserId INT,
    @UnreadOnly BIT = 0,
    @Page INT = 1,
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@Page - 1) * @PageSize;
    
    SELECT 
        n.*,
        nr.IsRead,
        nr.ReadAt,
        u.DisplayName AS CreatedByName,
        u.AvatarUrl AS CreatedByAvatar,
        (SELECT COUNT(*) FROM NotificationRecipients 
         WHERE UserId = @UserId AND (@UnreadOnly = 0 OR IsRead = 0)) AS TotalCount
    FROM Notifications n
    INNER JOIN NotificationRecipients nr ON n.NotificationId = nr.NotificationId
    LEFT JOIN Users u ON n.CreatedById = u.UserId
    WHERE nr.UserId = @UserId
        AND (@UnreadOnly = 0 OR nr.IsRead = 0)
    ORDER BY n.CreatedAt DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserStats]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get user statistics
CREATE   PROCEDURE [dbo].[sp_GetUserStats]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.UserId,
        u.DisplayName,
        u.ReputationPoints,
        (SELECT COUNT(*) FROM Questions WHERE AuthorId = @UserId) AS QuestionsAsked,
        (SELECT COUNT(*) FROM Answers WHERE AuthorId = @UserId) AS AnswersGiven,
        (SELECT COUNT(*) FROM Answers WHERE AuthorId = @UserId AND IsAccepted = 1) AS AcceptedAnswers,
        (SELECT COUNT(*) FROM Comments WHERE AuthorId = @UserId) AS CommentsPosted,
        (SELECT COUNT(*) FROM TagFollowers WHERE UserId = @UserId) AS TagsFollowing,
        (SELECT COUNT(*) FROM QuestionFollowers WHERE UserId = @UserId) AS QuestionsFollowing
    FROM Users u
    WHERE u.UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_MarkAllNotificationsRead]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Mark all notifications as read
CREATE   PROCEDURE [dbo].[sp_MarkAllNotificationsRead]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE NotificationRecipients 
    SET IsRead = 1, ReadAt = GETDATE()
    WHERE UserId = @UserId AND IsRead = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_MarkNotificationRead]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Mark notification as read
CREATE   PROCEDURE [dbo].[sp_MarkNotificationRead]
    @NotificationId INT,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE NotificationRecipients 
    SET IsRead = 1, ReadAt = GETDATE()
    WHERE NotificationId = @NotificationId AND UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegisterForMeeting]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Register for meeting
CREATE   PROCEDURE [dbo].[sp_RegisterForMeeting]
    @MeetingId INT,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @MaxParticipants INT;
    DECLARE @CurrentParticipants INT;
    
    SELECT @MaxParticipants = MaxParticipants, @CurrentParticipants = CurrentParticipants
    FROM Meetings WHERE MeetingId = @MeetingId;
    
    IF @MaxParticipants IS NOT NULL AND @CurrentParticipants >= @MaxParticipants
    BEGIN
        RAISERROR('Meeting is full', 16, 1);
        RETURN;
    END
    
    IF EXISTS (SELECT 1 FROM MeetingParticipants WHERE MeetingId = @MeetingId AND UserId = @UserId)
    BEGIN
        RAISERROR('Already registered for this meeting', 16, 1);
        RETURN;
    END
    
    INSERT INTO MeetingParticipants (MeetingId, UserId) VALUES (@MeetingId, @UserId);
    UPDATE Meetings SET CurrentParticipants = CurrentParticipants + 1 WHERE MeetingId = @MeetingId;
    
    SELECT 'Registration successful' AS Message;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ToggleTagFollow]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Follow/unfollow tag
CREATE   PROCEDURE [dbo].[sp_ToggleTagFollow]
    @TagId INT,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM TagFollowers WHERE TagId = @TagId AND UserId = @UserId)
    BEGIN
        DELETE FROM TagFollowers WHERE TagId = @TagId AND UserId = @UserId;
        UPDATE Tags SET FollowerCount = FollowerCount - 1 WHERE TagId = @TagId;
        SELECT 0 AS IsFollowing;
    END
    ELSE
    BEGIN
        INSERT INTO TagFollowers (TagId, UserId) VALUES (@TagId, @UserId);
        UPDATE Tags SET FollowerCount = FollowerCount + 1 WHERE TagId = @TagId;
        SELECT 1 AS IsFollowing;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUserProfile]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Update user profile
CREATE   PROCEDURE [dbo].[sp_UpdateUserProfile]
    @UserId INT,
    @FullName NVARCHAR(100) = NULL,
    @DisplayName NVARCHAR(50) = NULL,
    @Bio NVARCHAR(500) = NULL,
    @AvatarUrl NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Users
    SET FullName = ISNULL(@FullName, FullName),
        DisplayName = ISNULL(@DisplayName, DisplayName),
        Bio = ISNULL(@Bio, Bio),
        AvatarUrl = ISNULL(@AvatarUrl, AvatarUrl),
        UpdatedAt = GETDATE()
    WHERE UserId = @UserId;
    
    SELECT * FROM Users WHERE UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUserReputation]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Update user reputation
CREATE   PROCEDURE [dbo].[sp_UpdateUserReputation]
    @UserId INT,
    @Points INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Users
    SET ReputationPoints = ReputationPoints + @Points
    WHERE UserId = @UserId;
    
    SELECT ReputationPoints FROM Users WHERE UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_VoteQuestion]    Script Date: 3/18/2026 2:08:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Vote on question
CREATE   PROCEDURE [dbo].[sp_VoteQuestion]
    @QuestionId INT,
    @UserId INT,
    @VoteType TINYINT -- 1 = upvote, -1 = downvote
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ExistingVote TINYINT;
    DECLARE @AuthorId INT;
    DECLARE @ReputationChange INT;
    
    SELECT @AuthorId = AuthorId FROM Questions WHERE QuestionId = @QuestionId;
    
    -- Check if user is trying to vote on their own question
    IF @AuthorId = @UserId
    BEGIN
        RAISERROR('Cannot vote on your own question', 16, 1);
        RETURN;
    END
    
    -- Check for existing vote
    SELECT @ExistingVote = VoteType FROM QuestionVotes 
    WHERE QuestionId = @QuestionId AND UserId = @UserId;
    
    IF @ExistingVote IS NOT NULL
    BEGIN
        IF @ExistingVote = @VoteType
        BEGIN
            -- Remove vote (toggle off)
            DELETE FROM QuestionVotes WHERE QuestionId = @QuestionId AND UserId = @UserId;
            
            IF @VoteType = 1
                UPDATE Questions SET UpvoteCount = UpvoteCount - 1 WHERE QuestionId = @QuestionId;
            ELSE
                UPDATE Questions SET DownvoteCount = DownvoteCount - 1 WHERE QuestionId = @QuestionId;
            
            -- Remove reputation
            SET @ReputationChange = -1 * CAST(@VoteType AS INT) * 10;
            EXEC sp_UpdateUserReputation @AuthorId, @ReputationChange;
        END
        ELSE
        BEGIN
            -- Change vote
            UPDATE QuestionVotes SET VoteType = @VoteType WHERE QuestionId = @QuestionId AND UserId = @UserId;
            
            IF @VoteType = 1
            BEGIN
                UPDATE Questions SET UpvoteCount = UpvoteCount + 1, DownvoteCount = DownvoteCount - 1 WHERE QuestionId = @QuestionId;
                EXEC sp_UpdateUserReputation @AuthorId, 20;
            END
            ELSE
            BEGIN
                UPDATE Questions SET UpvoteCount = UpvoteCount - 1, DownvoteCount = DownvoteCount + 1 WHERE QuestionId = @QuestionId;
                EXEC sp_UpdateUserReputation @AuthorId, -20;
            END
        END
    END
    ELSE
    BEGIN
        -- New vote
        INSERT INTO QuestionVotes (QuestionId, UserId, VoteType) VALUES (@QuestionId, @UserId, @VoteType);
        
        IF @VoteType = 1
            UPDATE Questions SET UpvoteCount = UpvoteCount + 1 WHERE QuestionId = @QuestionId;
        ELSE
            UPDATE Questions SET DownvoteCount = DownvoteCount + 1 WHERE QuestionId = @QuestionId;
        
        -- Award/deduct reputation
        SET @ReputationChange = CAST(@VoteType AS INT) * 10;
        EXEC sp_UpdateUserReputation @AuthorId, @ReputationChange;
    END
    
    SELECT UpvoteCount, DownvoteCount FROM Questions WHERE QuestionId = @QuestionId;
END
GO
USE [master]
GO
ALTER DATABASE [SWP391_QA] SET  READ_WRITE 
GO
