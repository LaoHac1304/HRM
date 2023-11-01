using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Banks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.BankAccounts;
public class BankAccountDTO : IMapFrom<BankAccount>
{
    public Guid id { get; set; }
    [ForeignKey("Bank")]
    public Guid BankID { get; set; }
    [ForeignKey("Employee")]
    public Guid EmployeeID { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }
    public Boolean Selected { get; set; }
    // Relationship
    //public virtual Employee Employee { get; set; }
    public virtual BankDTO? Bank { get; set; }
}
