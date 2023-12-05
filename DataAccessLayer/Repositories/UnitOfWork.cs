

using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolDbContext _dbContext;

    public UnitOfWork(SchoolDbContext dbContext,
                      IClassRepository classRepository,
                      IExamRepository examRepository,
                      IExpenseRepository expenseRepository,
                      IFeesRepository feesRepository,
                      IParentRepository parentRepository,
                      IPaymentMethodRepository paymentMethodRepository,
                      IScholarshipRepository scholarshipRepository,
                      IStudentAttendanceRepository studentAttendanceRepository,
                      IStudentParentRepository studentParentRepository,
                      IStudentRepository studentRepository,
                      ISubjectRepository subjectRepository,
                      ITeacherAttendanceRepository teacherAttendanceRepository,
                      ITeacherRepository teacherRepository,
                      ITeacherSubjectRepository teacherSubjectRepository
                        )
    {
        _dbContext = dbContext;

        ClassRepository = classRepository;
        ExamRepository = examRepository;

        ExpenseRepository = expenseRepository;
        FeesRepository = feesRepository;

        ParentRepository = parentRepository;
        PaymentMethodRepository = paymentMethodRepository;

        StudentAttendanceRepository = studentAttendanceRepository;
        TeacherSubjectRepository = teacherSubjectRepository;

        TeacherRepository = teacherRepository;
        TeacherAttendanceRepository = teacherAttendanceRepository;

        SubjectRepository = subjectRepository;
        TeacherRepository = teacherRepository;

        StudentRepository = studentRepository;
        StudentParentRepository = studentParentRepository;


    }

    public IClassRepository ClassRepository { get; }
    public IExamRepository ExamRepository { get; }

    public IExpenseRepository ExpenseRepository { get; }
    public IFeesRepository FeesRepository { get; }

    public IParentRepository ParentRepository { get; }
    public IPaymentMethodRepository PaymentMethodRepository { get; }

    public IScholarshipRepository ScholarshipRepository { get; }
    public IStudentAttendanceRepository StudentAttendanceRepository { get; }

    public IStudentParentRepository StudentParentRepository { get; }
    public IStudentRepository StudentRepository { get; }

    public ISubjectRepository SubjectRepository { get; }
    public ITeacherAttendanceRepository TeacherAttendanceRepository { get; }

    public ITeacherRepository TeacherRepository { get; }
    public ITeacherSubjectRepository TeacherSubjectRepository { get; }

    public void Dispose()
              => GC.SuppressFinalize(this);

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
