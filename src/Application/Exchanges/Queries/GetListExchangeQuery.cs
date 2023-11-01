

using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Exchanges.Queries;
public record GetListExchangeQuery : IRequest<List<Exchange>>;
public class GetListExchangeQueryHandler : IRequestHandler<GetListExchangeQuery, List<Exchange>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListExchangeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Exchange>> Handle(GetListExchangeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Exchanges
                .AsNoTracking()
                .OrderBy(t => t.Muc_Quy_Doi)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Đã xảy ra lỗi khi lấy danh sách Exchange." + ex.Message);
        }
    }


}
