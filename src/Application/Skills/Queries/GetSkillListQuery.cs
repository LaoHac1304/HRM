using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Queries;
public class GetSkillListQuery : IRequest<List<SkillDTO>>
{
}

public class GetSkillListQueryHandler : IRequestHandler<GetSkillListQuery, List<SkillDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetSkillListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SkillDTO>> Handle(GetSkillListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Skills.Where(s => s.IsDeleted == false).AsNoTracking();
        if (query == null || query.Count() == 0)
        {
            throw new NotFoundException(nameof(Skills));
        }

        var map = query.ProjectTo<SkillDTO>(_mapper.ConfigurationProvider);

        var list = map.ToListAsync(cancellationToken);

        return await list;
    }
}
