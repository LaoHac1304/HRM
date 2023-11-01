using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace hrOT.Application.BankAccounts.Commands.Create;
public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateBankAccountCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.BankId)
            .NotEmpty().WithMessage("Bank ID không được bỏ trống.")
            .MustAsync(BankExistAsync).WithMessage("Ngân hàng không tồn tại.")
            .MustAsync(BankIsDeletedAsync).WithMessage("Ngân hàng đã bị xóa.");

        RuleFor(x => x.EmployeeId)
            .NotNull().WithMessage("Employee ID không được bỏ trống.")
            .MustAsync(EmployeeExistAsync).WithMessage("Nhân viên không tồn tại.")
            .MustAsync(EmployeeIsDeletedAsync).WithMessage("Nhân viên đã bị xóa.");

        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("Số tài khoản không được bỏ trống.")
            .Matches(@"^[0-9]*$").WithMessage("Định dạng số tài khoản không đúng.")
            .MaximumLength(14).WithMessage("Số tài khoản không được quá 14 ký tự.");

        RuleFor(x => x.BankAccountName)
            .NotEmpty().WithMessage("Tên tài khoản không được bỏ trống.")
            .Matches(@"^[A-Za-z]+$").WithMessage("Định dạng tên tài khoản không đúng.")
            .MaximumLength(100).WithMessage("Tên tài khoản không được quá 100 ký tự.");
    }
    
    private async Task<bool> BankExistAsync(Guid Id, CancellationToken cancellationToken)
    {
        var bank = await _context.Banks
                .Where(b => b.Id == Id).AnyAsync(b => b.Id == Id, cancellationToken);
        return bank;
    }
    private async Task<bool> BankIsDeletedAsync(Guid Id, CancellationToken cancellationToken)
    {
        var bank = _context.Banks.Where(b => b.Id == Id);
        Boolean check = true;
        if (bank.Count() > 0)
        {
            check = await bank.AnyAsync(e => e.Id == Id && e.IsDeleted == false, cancellationToken);
        }
        return check;
        
    }
    private async Task<bool> EmployeeExistAsync(Guid Id, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
                .Where(e => e.Id == Id).AnyAsync(e => e.Id == Id, cancellationToken);
        return employee;
    }
    private async Task<bool> EmployeeIsDeletedAsync(Guid Id, CancellationToken cancellationToken)
    {
        var employee = _context.Employees.Where(e => e.Id == Id);
        Boolean check = true;
        if (employee.Count() > 0) {
            check = await employee.AnyAsync(e => e.Id == Id && e.IsDeleted == false, cancellationToken);
        }
        return check;
    }
}
