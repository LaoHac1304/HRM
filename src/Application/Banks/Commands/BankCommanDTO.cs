using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Banks.Commands;
public class BankCommandDTO : IMapFrom<Bank>
{
    public Guid Id { get; set; }
    public string BankName { get; set; }

    public string Description { get; set; }
}
