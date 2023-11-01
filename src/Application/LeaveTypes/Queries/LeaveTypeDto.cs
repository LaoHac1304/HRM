using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.LeaveTypes.Queries;
public class LeaveTypeDto : IMapFrom<LeaveType>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Benefit { get; set; } // Number of leave days
    public bool IsReward { get; set; }
}
