﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace hrOT.Application.BankAccounts.Commands.Update;
public class UpdateBankAccountCommandValidator : AbstractValidator<UpdateBankAccountCommand>
{
    public UpdateBankAccountCommandValidator()
    {
        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("Số tài khoản không được bỏ trống.")
            .Matches(@"^[0-9]*$").WithMessage("Định dạng số tài khoản không đúng.")
            .MaximumLength(14).WithMessage("Số tài khoản không được quá 14 ký tự.");

        RuleFor(x => x.BankAccountName)
            .NotEmpty().WithMessage("Tên tài khoản không được bỏ trống.")
            .Matches(@"^[A-Za-z]+$").WithMessage("Định dạng tên tài khoản không đúng.")
            .MaximumLength(100).WithMessage("Tên tài khoản không được quá 100 ký tự.");
    }
}
