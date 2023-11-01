using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.LeaveLogs.Queries;

public record Staff_GetListLeaveLogByDateQuery(DateTime StartDate, DateTime EndDate) : IRequest<List<LeaveLogDto>>;

public class Staff_GetListLeaveLogByDateQueryHandler : IRequestHandler<Staff_GetListLeaveLogByDateQuery, List<LeaveLogDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Staff_GetListLeaveLogByDateQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LeaveLogDto>> Handle(Staff_GetListLeaveLogByDateQuery request, CancellationToken cancellationToken)
    {

            var leaveLogs = await _context.LeaveLogs
                .AsNoTracking()
                .Where(log => log.StartDate >= request.StartDate && log.EndDate <= request.EndDate)
                .ProjectTo<LeaveLogDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Status)
                .ToListAsync(cancellationToken);

            return leaveLogs;
       
    }
}
