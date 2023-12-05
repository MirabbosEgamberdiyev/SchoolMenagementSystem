
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLayer.Models;

public class Scholarship : BaseModel
{

    [ForeignKey("ClassId")]
    public virtual Class Class { get; set; } = new Class();

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; } = new Student();


    [Required]
    public decimal Amount { get; set; }
}



