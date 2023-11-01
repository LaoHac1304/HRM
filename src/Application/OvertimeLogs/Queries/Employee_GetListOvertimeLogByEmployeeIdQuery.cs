using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Queries;

public record Employee_GetListOvertimeLogByEmployeeIdQuery(Guid Id) : IRequest<List<OvertimeLogDto>>;


public class Employee_GetListOvertimeLogByEmployeeIdQueryHandler : IRequestHandler<Employee_GetListOvertimeLogByEmployeeIdQuery, List<OvertimeLogDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_GetListOvertimeLogByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OvertimeLogDto>> Handle(Employee_GetListOvertimeLogByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.OvertimeLogs
                .Where(o => o.IsDeleted == false && o.EmployeeId == request.Id)
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