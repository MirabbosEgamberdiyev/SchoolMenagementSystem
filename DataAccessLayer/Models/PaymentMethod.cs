using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models;

public class PaymentMethod : BaseModel
{ 


    [MaxLength(100)]
    public string MethodName { get; set; } = string.Empty;

}


