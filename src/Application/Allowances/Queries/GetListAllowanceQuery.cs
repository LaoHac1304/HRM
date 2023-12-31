﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Security;
using Microsoft.EntityFrameworkCore;
using hrOT.Application.Common.Exceptions;
using hrOT.Domain.Entities;

namespace hrOT.Application.Allowances.Queries;

public record GetListAllowanceQuery : IRequest<List<AllowanceDto>>;

public class GetListAllowanceQueryHandler : IRequestHandler<GetListAllowanceQuery, List<AllowanceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListAllowanceQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AllowanceDto>> Handle(GetListAllowanceQuery request, CancellationToken cancellationToken)
    {
        
        var query = _context.Allowances.Where(o => o.IsDeleted == false).AsNoTracking();
        if (query == null || query.Count() == 0)
        {
            throw new NotFoundException("Danh sách trống.");
        }

        var map = query.ProjectTo<AllowanceDto>(_mapper.ConfigurationProvider);
        
        var list = map.ToListAsync(cancellationToken);
        
        return await list;

    }
}
