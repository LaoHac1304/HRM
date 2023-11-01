using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Banks.Commands.Create;
public class CreateBankCommandValidator : AbstractValidator<CreateBankCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateBankCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x.BankName)
            .NotEmpty().WithMessage("Tên ngân hàng không được bỏ trống.")          
            .MaximumLength(100).WithMessage("Tên ngân hàng không được quá 100 ký tự.")
            .MustAsync(BeUniqueName).WithMessage("Tên ngân hàng đã tồn tại.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả ngân hàng không được bỏ trống.")
            .MaximumLength(1000).WithMessage("Mô tả ngân hàng không được quá 1000 ký tự.");
    }

    private async Task<bool> BeUniqueName(CreateBankCommand createBankCommand, string arg1, CancellationToken arg2)
    {
        return await _context.Banks
            .Where(s => s.BankName == createBankCommand.BankName && s.IsDeleted == false)
            .AllAsync(s => s.BankName != arg1, arg2);
    }
    /*private async Task<bool> ExistAsync(CreateBankCommand createBankCommand, CancellationToken cancellationToken)
    {
        var Exists = await _context.Banks.AnyAsync(e => e.BankName == createBankCommand.BankDTO.BankName, cancellationToken);
        return Exists;
    }*/
}
