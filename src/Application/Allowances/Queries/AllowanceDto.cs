using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;

namespace hrOT.Application.Allowances.Queries;

public class AllowanceDto : IMapFrom<Allowance>
{
    public Guid Id { get; set; }
    public Guid EmployeeContractId { get; set; }
    public Guid AllowanceTypeId { get; set; }
    public double Amount { get; set; }
    public bool IsDeleted { get; set; }
    //public virtual AllowanceType AllowanceType { get; set; }
    //public virtual EmployeeContract EmployeeContract { get; set; }
}

