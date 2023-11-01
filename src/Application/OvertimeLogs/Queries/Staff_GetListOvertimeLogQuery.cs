using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Queries;

public record Staff_GetListOvertimeLogQuery : IRequest<List<OvertimeLogDto>>;

public class Staff_GetListOvertimeLogQueryHandler : IRequestHandler<Staff_GetListOvertimeLogQuery, List<OvertimeLogDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Staff_GetListOvertimeLogQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OvertimeLogDto>> Handle(Staff_GetListOvertimeLogQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.OvertimeLogs
                .Where(o => o.IsDeleted == false)
                .ProjectTo<OvertimeLogDto>(_mapper.ConfigurationProvider)
                .OrderBy(o => o.Status)
                .ToListAsync(cancellationToken);

        if (result.Count == 0)
        {
            throw new Exception($"Danh sách trống.");
        }
        return result;
    }
}