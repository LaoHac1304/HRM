using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.AllowanceTypes.Command.Create;

namespace hrOT.Application.AllowanceTypes.Command.Update;
public class UpdateAllowanceTypeCommandValidator : AbstractValidator<UpdateAllowanceTypeCommand>
{
    public UpdateAllowanceTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id không được để trống");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên không được để trống")
            .MinimumLength(5).WithMessage("Tên phải nhiều hơn 5 chữ")
            .MaximumLength(500).WithMessage("Tên phải nhỏ hơn 500 chữ");

        RuleFor(x => x.IsPayTax)
            .NotNull().WithMessage("IsPayTax không được để trống");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Giới thiệu không được để trống.")
            .MinimumLength(5).WithMessage("Giới thiệu phải nhiều hơn 5 chữ")
            .MaximumLength(1000).WithMessage("Giới thiệu phải nhỏ hơn 1000 chữ");

        RuleFor(x => x.Eligibility_Criteria)
            .NotEmpty().WithMessage("Eligibility_Criteria không được để trống.")
            .MinimumLength(5).WithMessage("Eligibility_Criteria phải nhiều hơn 5 chữ")
            .MaximumLength(1000).WithMessage("Eligibility_Criteria phải nhỏ hơn 1000 chữ");

        RuleFor(x => x.Requirements)
            .NotEmpty().WithMessage("Yêu cầu không được để trống.")
            .MinimumLength(5).WithMessage("Yêu cầu phải nhiều hơn 5 chữ")
            .MaximumLength(1000).WithMessage("Yêu cầu phải nhỏ hơn 1000 chữ");
    }
}
