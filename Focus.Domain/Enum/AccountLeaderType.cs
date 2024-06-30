using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum AccountLeaderType
    {
        [Display(Name = "Customer")]
        Customer = 1,
        [Display(Name = "Supplier")]
        Supplier = 2,
        [Display(Name = "Product")]
        Product = 3,
        [Display(Name = "ClearanceAgent")]
        ClearanceAgent = 4,
        [Display(Name = "Employee")]
        Employee = 6,
        [Display(Name = "EmployeePayable")]
        EmployeePayable = 7,
        [Display(Name = "ExpenseType")]
        ExpenseType = 8,
    }
}
