using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models;

public class Parent: BaseModel
{
    [Required, MinLength(3), MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    [MaxLength(13)]
    public string? ContactNumber { get; set; }
    [MinLength(5), MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    [MinLength(5), MaxLength(1000)]
    public string Address { get; set; } = string.Empty;

    public virtual ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
}
