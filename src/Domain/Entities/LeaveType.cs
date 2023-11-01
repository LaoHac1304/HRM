using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Domain.Common;

namespace hrOT.Domain.Entities;
public class LeaveType : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Benefit { get; set; } // Number of leave days
    public bool IsReward { get; set; }
    public IList<LeaveLog> LeaveLogs { get; private set; }
}
