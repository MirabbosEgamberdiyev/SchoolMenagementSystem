

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Expense: BaseModel
{ 
    [Required]
    public int ClassId { get; set; }
    [Required]
    public int SubjectId { get; set; }

    public int ChargeAmount { get; set; }

    // Navigation properties
    public virtual Class Class { get; set; }
    public virtual Subject Subject { get; set; }
}
