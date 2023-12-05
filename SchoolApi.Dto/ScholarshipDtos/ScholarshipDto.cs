

using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Dto.ScholarshipDtos;

public class ScholarshipDto:BaseEntityDto
{
    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public decimal Amount { get; set; }
}
