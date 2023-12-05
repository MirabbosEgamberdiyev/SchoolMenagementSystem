

namespace DataAccessLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IClassRepository ClassRepository { get; }
    IExamRepository ExamRepository { get; }

    IExpenseRepository ExpenseRepository { get; }
    IFeesRepository FeesRepository { get; }

    IParentRepository ParentRepository { get; }
    IPaymentMethodRepository PaymentMethodRepository {  get; }

    IScholarshipRepository ScholarshipRepository { get; }
    IStudentAttendanceRepository StudentAttendanceRepository { get; }

    IStudentParentRepository StudentParentRepository { get; }
    IStudentRepository StudentRepository { get; }
    
    ISubjectRepository SubjectRepository { get; }
    ITeacherAttendanceRepository TeacherAttendanceRepository { get; }

    ITeacherRepository TeacherRepository { get; }
    ITeacherSubjectRepository TeacherSubjectRepository { get; }

    Task SaveAsync();
}
