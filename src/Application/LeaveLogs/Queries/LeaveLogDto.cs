using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;

namespace hrOT.Application.LeaveLogs.Queries;
public class LeaveLogDto : IMapFrom<LeaveLog>
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double LeaveHours { get; set; }
    public string Reason { get; set; }
    public LeaveLogStatus Status { get; set; }
}
