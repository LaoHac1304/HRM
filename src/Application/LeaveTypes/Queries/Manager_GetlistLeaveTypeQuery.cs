using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.LeaveLogs.Queries;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.LeaveTypes.Queries;
public record Manager_GetlistLeaveTypeQuery : IRequest<List<LeaveTypeDto>>;

public class Manager_GetlistLeaveTypeQueryHandler : IRequestHandler<Manager_GetlistLeaveTypeQuery, List<LeaveTypeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Manager_GetlistLeaveTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeDto>> Handle(Manager_GetlistLeaveTypeQuery request, CancellationToken cancellationToken)
    {
        var leaveTypes = await _context.LeaveTypes.AsNoTracking().ProjectTo<LeaveTypeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        if (leaveTypes.Count == 0)
        {
            throw new NotFoundException( "No Leave Type found");
        }
        return leaveTypes;

    }

}
