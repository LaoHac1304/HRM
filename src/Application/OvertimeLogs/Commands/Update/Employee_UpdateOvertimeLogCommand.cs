using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Commands.Update;

public record Employee_UpdateOvertimeLogCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    //public Guid EmployeeId { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }
    //public double TotalHours { get; init; }
}

public class Employee_UpdateOvertimeLogCommandHandler : IRequestHandler<Employee_UpdateOvertimeLogCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public Employee_UpdateOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(Employee_UpdateOvertimeLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLogs
            .FirstOrDefaultAsync(e => e.Id == request.Id)
            ?? throw new NotFoundException($"Không tìm thấy OvertimeLog ID: {request.Id}");

        entity.Status = OvertimeLogStatus.Pending;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.TotalHours = (request.EndDate - request.StartDate).TotalHours;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}