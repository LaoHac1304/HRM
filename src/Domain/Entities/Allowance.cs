using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class Allowance : BaseAuditableEntity
{
    [ForeignKey("EmployeeContract")]
    public Guid EmployeeContractId { get; set; }
    [ForeignKey("AllowanceType")]
    public Guid AllowanceTypeId { get; set; }

    //public string Name { get; set; }
    //public AllowanceType Type { get; set; }
    public double Amount { get; set; }

    // Relationship
    public virtual EmployeeContract EmployeeContract { get; set; }
    public virtual AllowanceType AllowanceType { get; set; }
}