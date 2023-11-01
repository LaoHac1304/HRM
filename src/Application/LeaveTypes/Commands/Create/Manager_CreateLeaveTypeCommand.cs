using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.LeaveTypes.Commands.Create;
public record Manager_CreateLeaveTypeCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Benefit { get; set; } // Number of leave days
    public bool IsReward { get; set; }
}

public class Manager_CreateLeaveTypeCommandHandler : IRequestHandler<Manager_CreateLeaveTypeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public Manager_CreateLeaveTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(Manager_CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {

        var entity = new LeaveType
        {
            Name = request.Name,
            Description = request.Description,
            Benefit = request.Benefit,
            IsReward = request.IsReward
        };

        var e = _context.LeaveTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return e.Entity.Id;
    }
}
