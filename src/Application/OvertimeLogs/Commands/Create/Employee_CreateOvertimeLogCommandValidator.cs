using FluentValidation;

namespace hrOT.Application.OvertimeLogs.Commands.Create;

public class Employee_CreateOvertimeLogCommandValidator : AbstractValidator<Employee_CreateOvertimeLogCommand>
{
    public Employee_CreateOvertimeLogCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("ID nhân viên không được để trống");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Ngày bắt đầu không được để trống.")
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("Ngày bắt đầu phải sớm hơn hoặc bằng ngày kết thúc.")
            .Must(NotInPast).WithMessage("Không được tạo vào ngày quá khứ");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Ngày kết thúc không được để trống.")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Ngày kết thúc phải sau hoặc bằng ngày bắt đầu.")
            .Must(NotInPast).WithMessage("Không được tạo vào ngày quá khứ");

        RuleFor(x => x.EndDate.Hour)
            .LessThanOrEqualTo(x => x.StartDate.Hour + 12).WithMessage("Chỉ được OT tối đa 12 tiếng/ngày");
    }

    private bool NotInPast(DateTime time)
    {
        return time.Date >= DateTime.UtcNow.Date;
    }
}