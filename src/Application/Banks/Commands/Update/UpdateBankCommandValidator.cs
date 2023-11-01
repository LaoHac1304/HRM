using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Banks.Commands.Create;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Banks.Commands.Update;
public class UpdateBankCommandValidator : AbstractValidator<UpdateBankCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateBankCommandValidator()
    {
        RuleFor(x => x.BankName)
            .NotEmpty().WithMessage("Tên ngân hàng không được bỏ trống.")
            .MaximumLength(100).WithMessage("Tên ngân hàng không được quá 100 ký tự.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả ngân hàng không được bỏ trống.")
            .MaximumLength(1000).WithMessage("Mô tả ngân hàng không được quá 1000 ký tự.");
    }

    /*private async Task<bool> BeUniqueName(UpdateBankCommand updateBankCommand, string arg1, CancellationToken arg2)
    {
        return await _context.Banks
            .Where(s => s.BankName == updateBankCommand.BankDTO.BankName && s.IsDeleted == false)
            .AllAsync(s => s.BankName != arg1, arg2);
    }*/
}
