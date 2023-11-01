using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Asn1.Ocsp;

namespace hrOT.Application.Auth.Queries;
public class ChangePassWordValidator:AbstractValidator<ChangePassWord>
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public ChangePassWordValidator(IApplicationDbContext context)
    {
        _context = context;


        RuleFor(e => e.Username)
            .NotNull().WithMessage("Tài khoản không được để trống")
            .NotEmpty().WithMessage("Tài khoản không được để trống")
            .MaximumLength(50).WithMessage("Tên người dùng không được vượt quá 50 ký tự.");


        RuleFor(e => e.NewPassword)
            .NotNull().WithMessage("Mật khẩu không được để trống")
            .NotEmpty().WithMessage("Mật khẩu không được để trống")
            .MinimumLength(6).WithMessage("Mật khẩu mới phải có ít nhất 6 ký tự.");


        RuleFor(e => e.CurrentPassword)

            .NotEmpty().WithMessage("Mật khẩu không được để trống")
            .MinimumLength(6).WithMessage("Mật khẩu hiện tại phải có ít nhất 6 ký tự.");
    }

    public async Task<bool> BeUniqueUserName(string username, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AllAsync(e => e.ApplicationUser.UserName != username, cancellationToken);
    }



}
