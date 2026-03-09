using DataLayer.DataLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SWP391QAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(BussinessLayer.Mappers.MapperConfigurationsProfile));

// Add Unit of Work
builder.Services.AddScoped<BussinessLayer.IUnitOfWork, BussinessLayer.UnitOfWork>();

// Add Repositories
builder.Services.AddScoped<BussinessLayer.IRepositories.IUserRepository, BussinessLayer.Repositories.UserRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.IRoleRepository, BussinessLayer.Repositories.RoleRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.IQuestionRepository, BussinessLayer.Repositories.QuestionRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.IAnswerRepository, BussinessLayer.Repositories.AnswerRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.ICommentRepository, BussinessLayer.Repositories.CommentRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.ITeamRepository, BussinessLayer.Repositories.TeamRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.ITeamMemberRepository, BussinessLayer.Repositories.TeamMemberRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.ICoreRepository, BussinessLayer.Repositories.CoreRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.ITopicRepository, BussinessLayer.Repositories.TopicRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.ISemesterRepository, BussinessLayer.Repositories.SemesterRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.ISystemInstructorRepository, BussinessLayer.Repositories.SystemInstructorRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.IChatRoomRepository, BussinessLayer.Repositories.ChatRoomRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.IChatMessageRepository, BussinessLayer.Repositories.ChatMessageRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.INotificationRepository, BussinessLayer.Repositories.NotificationRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.INotificationRecipientRepository, BussinessLayer.Repositories.NotificationRecipientRepository>();
builder.Services.AddScoped<BussinessLayer.IRepositories.IQuestionFollowerRepository, BussinessLayer.Repositories.QuestionFollowerRepository>();

// Add Services
builder.Services.AddScoped<BussinessLayer.IServices.IUserService, BussinessLayer.Service.UserService>();
builder.Services.AddScoped<BussinessLayer.IServices.IQuestionService, BussinessLayer.Service.QuestionService>();
builder.Services.AddScoped<BussinessLayer.IServices.ITeamService, BussinessLayer.Service.TeamService>();
builder.Services.AddScoped<BussinessLayer.IServices.ISemesterService, BussinessLayer.Service.SemesterService>();
builder.Services.AddScoped<BussinessLayer.IServices.ICoreService, BussinessLayer.Service.CoreService>();
builder.Services.AddScoped<BussinessLayer.IServices.ITopicService, BussinessLayer.Service.TopicService>();
builder.Services.AddScoped<BussinessLayer.IServices.IAnswerService, BussinessLayer.Service.AnswerService>();
builder.Services.AddScoped<BussinessLayer.IServices.ICommentService, BussinessLayer.Service.CommentService>();

// Configure Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
