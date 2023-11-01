using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Commands.Update;
public class UpdateBankAccountCommand : IRequest<string>
{
    /*public BankAccountCommandDTO _dto { get; init; }*/
    public Guid BankAccountId { get; set; }
    public string? BankAccountNumber { get; set; }
    public string? BankAccountName { get; set; }
    public Boolean Selected { get; set; }
    /*public UpdateBankAccountCommand(Guid BankID, BankAccountCommandDTO dto)
    {
        _dto = dto;
        BankId = BankID;
    }*/
}
public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UpdateBankAccountCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BankAccounts
            .FindAsync(new object[] { request.BankAccountId }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("Không tìm thấy tài khoản ngân hàng");
        }
        else if (entity.IsDeleted == true)
        {
            throw new NotFoundException("Tài khoản ngân hàng đã bị xóa!");
        }

        var employeeId = entity.EmployeeId;

        if (request.Selected == true)
        {
            // Set all BankAccounts of the EmployeeId to false
            var employeeBankAccounts = await _context.BankAccounts
                .Where(b => b.EmployeeId == employeeId)
                .ToListAsync(cancellationToken);

            foreach (var bankAccount in employeeBankAccounts)
            {
                if (bankAccount.Id != request.BankAccountId)
                {
                    bankAccount.Selected = false;
                }
            }
        }

        entity.BankAccountNumber = request.BankAccountNumber ?? "0";
        entity.BankAccountName = request.BankAccountName ?? "";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";
        entity.Selected = request.Selected;
        await _context.SaveChangesAsync(cancellationToken);

        return "Cập nhật thành công";
    }
}
