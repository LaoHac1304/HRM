using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Positions.Queries.GetPosition;

public record GetListPositionQuery : IRequest<List<PositionDTO>>;

public class GetListPositionQueryHandler : IRequestHandler<GetListPositionQuery, List<PositionDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListPositionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PositionDTO>> Handle(GetListPositionQuery request, CancellationToken cancellationToken)
    {

        var position = await _context.Positions
           .Where(x => x.IsDeleted == false)
                       //.Include(l => l.Levels)
           .ProjectTo<PositionDTO>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);

        if (position == null)
        {
            throw new NotFoundException("Không tìm thấy danh sách vị trí");
        }

        if (position.Count == 0)
        {
            throw new NotFoundException("Không có vị trí nào");
        }

        return position;

    }
}