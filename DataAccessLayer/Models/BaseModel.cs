using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Models;

public class BaseModel
{
    [Required, Key]
    public int Id { get; set; }
}
