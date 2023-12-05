

namespace SchoolApi.Dto.ExpenseDtos;

public class ExpenseDto:BaseEntityDto
{

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public int ChargeAmount { get; set; }
}
