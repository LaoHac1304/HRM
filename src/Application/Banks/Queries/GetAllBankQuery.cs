﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Banks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Banks.Queries;
public record GetAllBankQuery : IRequest<List<BankDTO>>;
public class GetAllBankQueryHandler : IRequestHandler<GetAllBankQuery, List<BankDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllBankQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BankDTO>> Handle(GetAllBankQuery request, CancellationToken cancellationToken)
    {
      
            var list = await _context.Banks
                .Where(e => e.IsDeleted == false)
                .ProjectTo<BankDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            if (list == null || list.Count() == 0 )
            {
            //throw new NotFoundException($"Không có giá trị trả về!");
                throw new NotFoundException("Danh sách trống.");
            }
            return list;
        
    }
}
