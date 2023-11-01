using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.AllowanceTypes;
public class AllowanceTypesDTO : IMapFrom<AllowanceType>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Boolean IsPayTax { get; set; }
    public string Description { get; set; }
    public string Eligibility_Criteria { get; set; }
    public string Requirements { get; set; }
    //public IList<Allowance> Allowances { get; private set; }
}
