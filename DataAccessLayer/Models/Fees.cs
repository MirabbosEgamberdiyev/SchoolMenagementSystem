

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Fees: BaseModel
{

    [Required]
    public int ClassId { get; set; }
    public int FeesAmount { get; set; }

    // Navigation properties
    public virtual Class Class { get; set; } = new Class();
}
