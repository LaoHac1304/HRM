using FluentValidation;

namespace hrOT.Application.Allowances.Command.Update
{
    public class UpdateAllowanceCommandValidator : AbstractValidator<UpdateAllowanceCommand>
    {
        public UpdateAllowanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID phụ cấp đang bị để trống hoặc chưa đúng định dạng.");

            RuleFor(x => x.EmployeeContractId)
                 .NotEmpty().WithMessage("ID hợp đồng của nhân viên đang bị để trống hoặc chưa đúng định dạng.");

            RuleFor(x => x.AllowanceTypeId)
                .NotEmpty().WithMessage("ID loại trợ cấp đang bị để trống hoặc chưa đúng định dạng.");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Phí trợ cấp không được để trống.")
                .GreaterThan(0).WithMessage("Phí trợ cấp phải lớn hơn 0");
        }
    }
}