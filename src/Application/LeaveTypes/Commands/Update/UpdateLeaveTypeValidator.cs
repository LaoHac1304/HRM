using FluentValidation;
using hrOT.Application.LeaveTypes.Commands.Create;
using hrOT.Application.LeaveTypes.Commands.Update;
using hrOT.Domain.Enums;

namespace hrOT.Application.Degrees.Commands.Create
{
    public class UpdateLeaveTypeValidator : AbstractValidator<Manager_UpdateLeaveTypeCommand>
    {
        public UpdateLeaveTypeValidator()
        {
            RuleFor(v => v.Name)
                 .MaximumLength(200).WithMessage("Tên không được quá 200 ký tự")
                 .NotEmpty().WithMessage("Tên không được để trống");

            RuleFor(v => v.Description)
                .MaximumLength(200).WithMessage("Mô tả không được quá 200 ký tự")
                .NotEmpty().WithMessage("Mô tả không được để trống");

            RuleFor(v => v.Benefit)
                .NotEmpty().WithMessage("Số ngày nghỉ không được để trống")
                .GreaterThan(0).WithMessage("Số ngày nghỉ phải lớn hơn 0");

            RuleFor(v => v.IsReward)
                .NotEmpty().WithMessage("Loại nghỉ phép không được để trống");
        }
    }
}
