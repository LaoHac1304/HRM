using FluentValidation;

namespace hrOT.Application.Allowances.Command.Create
{
    public class CreateAllowanceCommandValidator : AbstractValidator<CreateAllowanceCommand>
    {
        public CreateAllowanceCommandValidator()
        {
            RuleFor(x => x.EmployeeContractId)
                .NotEmpty().WithMessage("ID hợp đồng của nhân viên đang bị để trống hoặc không đúng định dạng");

            RuleFor(x => x.AllowanceTypeId)
                .NotEmpty().WithMessage("ID loại trợ cấp đang bị để trống hoặc không đúng định dạng");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Phí trợ cấp không được để trống.")
                .GreaterThan(0).WithMessage("Phí trợ cấp phải lớn hơn 0");
        }
    }
}