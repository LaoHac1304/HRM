using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.PaySlips.Queries;
public record GetTotalCostOfInsurance(DateTime FromDate, DateTime ToDate) : IRequest<double?>;

public class GetTotalCostOfInsuranceHandler : IRequestHandler<GetTotalCostOfInsurance, double?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTotalCostOfInsuranceHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<double?> Handle(GetTotalCostOfInsurance request, CancellationToken cancellationToken)
    {
        try
        {
            double? totalCostOfInsurance = await _context.PaySlips
                .Where(x => !x.IsDeleted && x.Paid_date >= request.FromDate && x.Paid_date <= request.ToDate)
                .SumAsync(x => x.BHXH_Comp + x.BHTN_Comp + x.BHYT_Comp, cancellationToken);
            if (totalCostOfInsurance == null || totalCostOfInsurance == 0)
            {
                throw new NotFoundException("Không tìm thấy phiếu lương trong khoảng thời gian này.");
            }
            return totalCostOfInsurance;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}


