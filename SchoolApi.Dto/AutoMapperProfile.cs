

using AutoMapper;
using DataAccessLayer.Models;
using SchoolApi.Dto.ClassDtos;
using SchoolApi.Dto.ExamDtos;
using SchoolApi.Dto.ExpenseDtos;
using SchoolApi.Dto.FeesDtos;
using SchoolApi.Dto.ParentDtos;
using SchoolApi.Dto.PaymentMethodDtos;
using SchoolApi.Dto.ScholarshipDtos;
using SchoolApi.Dto.StudentAttendanceDtos;
using SchoolApi.Dto.StudentDtos;
using SchoolApi.Dto.StudentParentDtos;
using SchoolApi.Dto.SubjectDtos;
using SchoolApi.Dto.TeacherAttendanceDtos;
using SchoolApi.Dto.TeacherDtos;
using SchoolApi.Dto.TeacherSubjectDtos;
using SchoolApi.Dto.UserDtos;

namespace SchoolApi.Dto;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, RegisterUserDto>()
            .ReverseMap();
        CreateMap<User, LoginUserDto>()
            .ReverseMap();
        CreateMap<Class , ClassDto>()
            .ReverseMap();
        CreateMap<Class , AddClassDto>() .
            ReverseMap();

        CreateMap<Exam , ExamDto>()
            .ReverseMap();
        CreateMap<Exam , AddExamDto>()
            .ReverseMap() ;

        CreateMap<Expense , ExpenseDto>()
            .ReverseMap();
        CreateMap<Expense, AddExpenseDto>() 
            .ReverseMap();

        CreateMap<Fees , FeesDto>()
            .ReverseMap();
        CreateMap<Fees, AddFeesDto>() 
            .ReverseMap();

        CreateMap<Parent , ParentDto>()
            .ReverseMap();
        CreateMap<Parent, AddParentDto>() 
            .ReverseMap();

        CreateMap<PaymentMethod , PaymentMethodDto>()
            .ReverseMap();
        CreateMap<PaymentMethod, AddPaymentMethodDto>()
          .ReverseMap();


        CreateMap<Scholarship , ScholarshipDto>()
            .ReverseMap();
        CreateMap<Scholarship, AddScholarshipDto>()
          .ReverseMap();

        CreateMap<Student , StudentDto>()
            .ReverseMap();
        CreateMap<Student, AddStudentDto>()
            .ReverseMap();

        CreateMap<StudentAttendance , StudentAttendanceDto>()
            .ReverseMap();
        CreateMap<StudentAttendance, AddStudentAttendanceDto>()
            .ReverseMap();

        CreateMap<StudentParent , StudentParentDto>()
            .ReverseMap();
        CreateMap<StudentParent, AddStudentParentDto>()
            .ReverseMap();

        CreateMap<Subject , SubjectDto>()
            .ReverseMap();
        CreateMap<Subject, AddSubjectDto>()
            .ReverseMap();

        CreateMap<Teacher  , TeacherDto>() 
            .ReverseMap();
        CreateMap<Teacher, AddTeacherDto>() .
            ReverseMap();

        CreateMap<TeacherAttendance , TeacherAttendanceDto>()
            .ReverseMap();
        CreateMap<TeacherAttendance , AddTeacherAttendanceDto>()
            .ReverseMap();

        CreateMap<TeacherSubject, TeacherSubjectDto>()
            .ReverseMap();
        CreateMap<TeacherSubject, AddTeacherSubjectDto>()
            .ReverseMap();
    }
}
