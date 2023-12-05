using Microsoft.Extensions.Configuration;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Dto;
using Serilog.Events;
using Serilog;
using BusinessLogicLayer.Helpers;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Access the Configuration property
var configuration = builder.Configuration;

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SchoolDbContext>()
    .AddDefaultTokenProviders();

// Use the configuration to get the connection string
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("LocalDB")));

// Ensure that the "logs" folder exists
string logsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "logs");
if (!Directory.Exists(logsFolderPath))
{
    Directory.CreateDirectory(logsFolderPath);
}

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddTransient<IClassRepository, ClassRepository>();
builder.Services.AddTransient<IExamRepository, ExamRepository>();

builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IFeesRepository, FeesRepository>();

builder.Services.AddTransient<IParentRepository, ParentRepository>();
builder.Services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();

builder.Services.AddTransient<IScholarshipRepository, ScholarshipRepository>();
builder.Services.AddTransient<IStudentAttendanceRepository, StudentAttendanceRepository>();

builder.Services.AddTransient<IStudentParentRepository, StudentParentRepository>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();

builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<ITeacherAttendanceRepository, TeacherAttendanceRepository>();

builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<ITeacherSubjectRepository, TeacherSubjectRepository>();


builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IClassService, ClassService>();

builder.Services.AddTransient<IExamService, ExamService>();
builder.Services.AddTransient<IExpenseService, ExpenseService>();

builder.Services.AddTransient<IFeesService, FeesService>();
builder.Services.AddTransient<IParentService, ParentService>();

builder.Services.AddTransient<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddTransient<IScholarshipService, ScholarshipService>();

builder.Services.AddTransient<IStudentAttendanceService, StudentAttendanceService>();
builder.Services.AddTransient<IStudentParentService, StudentParentService>();

builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ISubjectService, SubjectService>();

builder.Services.AddTransient<ITeacherAttendanceService, TeacherAttendanceService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();

builder.Services.AddTransient<ITeacherSubjectService, TeacherSubjectService>();
builder.Services.AddSingleton<ILoggingService, LoggingService>();

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        Path.Combine(logsFolderPath, "diagnostics.txt"),
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        retainedFileCountLimit: 2,
        rollOnFileSizeLimit: true,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(1))
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
