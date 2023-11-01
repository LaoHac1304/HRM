using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.LeaveLogs.Queries;

public record Staff_GetListLeaveLogQuery : IRequest<List<LeaveLogDto>>;

public class Staff_GetListLeaveLogQueryHandler : IRequestHandler<Staff_GetListLeaveLogQuery, List<LeaveLogDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Staff_GetListLeaveLogQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LeaveLogDto>> Handle(Staff_GetListLeaveLogQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var leaveLogs = await _context.LeaveLogs
                .AsNoTracking()
                .ProjectTo<LeaveLogDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Status)
                .ToListAsync(cancellationToken);

            if (leaveLogs.Count == 0 || leaveLogs is null)
            {
                throw new NotFoundException("Danh sách Leave Log trống.");
            }

            return leaveLogs;
        }
        catch (Exception ex)
        {
            if (ex is NotFoundException) throw new NotFoundException(ex.Message);
            throw new Exception("Đã xảy ra lỗi khi lấy danh sách Leave Log");
        }
    }
}
