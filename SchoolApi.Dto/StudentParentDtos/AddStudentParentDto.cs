﻿

using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Dto.StudentParentDtos;

public class AddStudentParentDto
{
    public int StudentId { get; set; }

    public int ParentId { get; set; }

    public string Relationship { get; set; } = string.Empty;

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; } = new Student();

    [ForeignKey("ParentId")]
    public virtual Parent Parent { get; set; } = new Parent();
}
