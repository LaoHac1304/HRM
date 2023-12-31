﻿using FluentValidation;

namespace hrOT.Application.OvertimeLogs.Commands.Update
{
    public class Staff_UpdateOvertimeLogCommandValidator : AbstractValidator<Staff_UpdateOvertimeLogCommand>
    {
        public Staff_UpdateOvertimeLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID của OT Log không được để trống");
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status không hợp lệ");
        }
    }
}
