using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Commands.Create;
public class CreateBankAccountCommand : IRequest<Guid>
{
    public Guid BankId { get; init; } = Guid.Empty;
    public Guid EmployeeId { get; init; } = Guid.Empty;
    //public BankAccountCommandDTO BankAccountDTO { get; init; }
    public string? BankAccountNumber { get; set; }
    public string? BankAccountName { get; set; }
    public Boolean Selected { get; set; }
    /*public CreateBankAccountCommand(Guid BankID, BankAccountCommandDTO bankAccountDTO)
    {
        BankId = BankID;
        BankAccountDTO = bankAccountDTO;
    }*/
}
public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateBankAccountCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var existBankAccount = await _context.BankAccounts
            .FirstOrDefaultAsync(ba => ba.BankId == request.BankId && ba.EmployeeId == request.EmployeeId);

        if (existBankAccount != null)
        {
            throw new NotFoundException("Tài khoản ngân hàng đã tồn tại");
        }

        // Đổi entity.Selected của các BankAccount khác về false theo EmployeeId
        if (request.Selected)
        {
            var otherBankAccounts = await _context.BankAccounts
                .Where(ba => ba.EmployeeId == request.EmployeeId && ba.Selected)
                .ToListAsync();

            foreach (var bankAccount in otherBankAccounts)
            {
                bankAccount.Selected = false;
            }
        }

        var entity = new BankAccount();
        entity.BankId = request.BankId;
        entity.BankAccountNumber = request.BankAccountNumber;
        entity.BankAccountName = request.BankAccountName;
        entity.EmployeeId = request.EmployeeId;
        entity.Selected = request.Selected;
        entity.CreatedBy = "test";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        _context.BankAccounts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
