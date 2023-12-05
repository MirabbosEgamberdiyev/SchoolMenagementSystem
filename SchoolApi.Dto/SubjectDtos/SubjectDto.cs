

using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Dto.SubjectDtos;

public class SubjectDto:BaseEntityDto
{


    public int ClassId { get; set; }

    public string SubjectName { get; set; } = string.Empty;
}
