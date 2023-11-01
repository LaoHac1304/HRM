using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;

namespace hrOT.Application.OvertimeLogs.Queries;
public class OvertimeLogDto : IMapFrom<OvertimeLog>
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    // Tổng số giờ làm việc
    public double TotalHours { get; set; }

    // Trạng thái kiểm duyệt
    public OvertimeLogStatus Status { get; set; }

}
