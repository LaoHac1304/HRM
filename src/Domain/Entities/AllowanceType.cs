using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class AllowanceType : BaseAuditableEntity
{
    public string Name { get; set; }
    public Boolean IsPayTax { get; set; }
    public string Description { get; set; }
    // Tiêu chí đủ điều kiện nhận trợ cấp
    [Required]
    public string Eligibility_Criteria { get; set; }
    // Yêu cầu tài liệu phụ cấp
    [Required]
    public string Requirements { get; set; }
    public IList<Allowance> Allowances { get; private set; }
}
