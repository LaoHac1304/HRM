using FluentValidation;

namespace hrOT.Application.Families.Commands.Update
{
    public class UpdateFamilyCommandValidator : AbstractValidator<UpdateFamilyCommand>
    {
        public UpdateFamilyCommandValidator()
        {
            RuleFor(x => x.FamilyId)
            .NotEmpty().WithMessage("Không đúng định dạng xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");

            /*RuleFor(x => x.EmployeeId)
               .NotEmpty().WithMessage("Employee ID không được bỏ trống.");*/

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được bỏ trống.")
                .MaximumLength(100).WithMessage("Tên không được quá 100 ký tự.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh không được bỏ trống.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Ngày sinh không hợp lệ.")
                .Must((model, dateOfBirth) => ValidateBirthday(dateOfBirth)).WithMessage("Tuổi người phụ thuộc từ 0-18 hoặc 65-125.");

            RuleFor(x => x.Relationship)
                .IsInEnum().WithMessage("Loại quan hệ không hợp lệ.");

            RuleFor(x => x.CitizenIdentificationNumber)
                .NotEmpty().WithMessage("Số căng cước công dân không được bỏ trống.")
                .Length(12).WithMessage("Số căng cước công dân phải có tối thiểu 12 số");

            RuleFor(x => x.CreatedDateCI)
                .NotEmpty().WithMessage("Ngày cấp không được bỏ trống.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Ngày cấp phải nhỏ hơn hoặc bằng ngày hiện tại.");

            RuleFor(x => x.PlaceForCI)
                .NotEmpty().WithMessage("Nơi cấp không được bỏ trống.")
                .MaximumLength(200).WithMessage("Nơi cấp không được vượt quá 200 ký tự.");

            RuleFor(x => x.PhotoCIOnTheFront)
                .NotEmpty().WithMessage("Căn cước công dân mặt trước không được bỏ trống");

            RuleFor(x => x.PhotoCIOnTheBack)
                .NotEmpty().WithMessage("Căn cước công dân mặt sau không được bỏ trống.");
        }
        private bool ValidateBirthday(DateTime day)
        {
            if (day > DateTime.Now)
            {
                return false;
            }

            var ageInYears = DateTime.Today.Year - day.Year;

            if ((ageInYears > 18 && ageInYears < 65) || ageInYears > 125)
            {
                return false;
            }

            /*if (ageInYears > 65)
            {
                return false;
            }

            if (ageInYears < 125)
            {
                return false;
            }*/

            return true;
        }
    }
}

