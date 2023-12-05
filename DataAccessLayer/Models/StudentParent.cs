using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models;

public class StudentParent: BaseModel
{

    [Required]
    public int StudentId { get; set; }
    [Required]
    public int ParentId { get; set; }
    public string Relationship { get; set; } = string.Empty;

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }= new Student();

    [ForeignKey("ParentId")]
    public virtual Parent Parent { get; set; } = new Parent();
}

