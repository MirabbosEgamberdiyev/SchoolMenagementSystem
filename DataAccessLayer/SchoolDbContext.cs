using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class SchoolDbContext : IdentityDbContext<User>
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {

    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<StudentParent> StudentParents { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<TeacherAttendance> TeacherAttendances { get; set; }
    public DbSet<StudentAttendance> StudentAttendances { get; set; }
    public DbSet<Fees> Fees { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Scholarship> Scholarships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureStudentParentRelationship(modelBuilder);
        ConfigureTeacherSubjectRelationship(modelBuilder);
        ConfigureStudentAttendanceRelationship(modelBuilder);
        ConfigureFeesRelationship(modelBuilder);
        ConfigureExamRelationship(modelBuilder);
        ConfigureExpenseRelationship(modelBuilder);
        ConfigureScholarshipRelationship(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureStudentParentRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentParent>()
            .HasKey(sp => new { sp.StudentId, sp.ParentId });

        modelBuilder.Entity<StudentParent>()
            .HasOne(sp => sp.Student)
            .WithMany(s => s.StudentParents)
            .HasForeignKey(sp => sp.StudentId);

        modelBuilder.Entity<StudentParent>()
            .HasOne(sp => sp.Parent)
            .WithMany(p => p.StudentParents)
            .HasForeignKey(sp => sp.ParentId);
    }

    private static void ConfigureTeacherSubjectRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeacherSubject>()
            .HasKey(ts => new { ts.ClassId, ts.SubjectId, ts.TeacherId });

        modelBuilder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Class);

        modelBuilder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Subject)
            .WithMany(s => s.TeacherSubjects)
            .HasForeignKey(ts => ts.SubjectId);
    }

    private static void ConfigureStudentAttendanceRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentAttendance>()
            .HasOne(sa => sa.Class)
            .WithMany(c => c.StudentAttendances)
            .HasForeignKey(sa => sa.ClassId);

        modelBuilder.Entity<StudentAttendance>()
            .HasOne(sa => sa.Student)
            .WithMany(s => s.StudentAttendances);

        modelBuilder.Entity<StudentAttendance>()
            .HasOne(sa => sa.Subject)
            .WithMany(s => s.StudentAttendances)
            .HasForeignKey(sa => sa.SubjectId);
    }

    private static void ConfigureFeesRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fees>()
            .HasOne(f => f.Class)
            .WithMany(c => c.Fees)
            .HasForeignKey(f => f.ClassId);
    }

    private static void ConfigureExamRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Class)
            .WithMany(c => c.Exams)
            .HasForeignKey(e => e.ClassId);

        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Subject)
            .WithMany(s => s.Exams)
            .HasForeignKey(e => e.SubjectId);
    }

    private static void ConfigureExpenseRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .HasOne(ex => ex.Class)
            .WithMany(c => c.Expenses)
            .HasForeignKey(ex => ex.ClassId);

        modelBuilder.Entity<Expense>()
            .HasOne(ex => ex.Subject)
            .WithMany(s => s.Expenses)
            .HasForeignKey(ex => ex.SubjectId);
    }


    private static void ConfigureScholarshipRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Scholarship>()
            .HasOne(s => s.Student)
            .WithMany()
            .HasForeignKey(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade); // Use CASCADE or another appropriate action for StudentId

        modelBuilder.Entity<Scholarship>()
            .HasOne(s => s.Class)
            .WithMany()
            .HasForeignKey(s => s.Id)
            .OnDelete(DeleteBehavior.NoAction); // Specify ON DELETE NO ACTION for ClassId
    }


}
