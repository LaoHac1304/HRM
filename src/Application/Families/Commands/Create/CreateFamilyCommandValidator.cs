using FluentValidation;

namespace hrOT.Application.Families.Commands.Create
{
    public class CreateFamilyCommandValidator : AbstractValidator<CreateFamilyCommand>
    {
        public CreateFamilyCommandValidator()
        {

            RuleFor(x => x.EmployeeId)
           .NotEmpty().WithMessage("Employee ID không được bỏ trống.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được bỏ trống.")
                .MaximumLength(100).WithMessage("Tên không quá 100 ký tự.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh không được bỏ trống.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Ngày sinh không hợp lệ.");

            RuleFor(x => x.Relationship)
                .IsInEnum().WithMessage("Loại quan hệ không hợp lệ.");

            RuleFor(x => x.CitizenIdentificationNumber)
                .NotEmpty().WithMessage("Số căng cước công dân không được bỏ trống.")
                .Length(12).WithMessage("Số căng cước công dân phải có 12 số");

            RuleFor(x => x.CreatedDateCI)
                .NotEmpty().WithMessage("Ngày cấp không được bỏ trống.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Ngày cấp phải nhỏ hơn hoặc bằng ngày hiện tại.");

            RuleFor(x => x.PlaceForCI)
                .NotEmpty().WithMessage("Nơi cấp không được bỏ trống.")
                .MaximumLength(200).WithMessage("Nơi cấp không được vượt quá 200 ký tự.");

            RuleFor(x => x.PhotoCIOnTheFront)
                .NotEmpty().WithMessage("Căng cước công dân mặt trước không được bỏ trống");

            RuleFor(x => x.PhotoCIOnTheBack)
                .NotEmpty().WithMessage("Căng cước công dân mặt sau không được bỏ trống.");
        }
    }
}
