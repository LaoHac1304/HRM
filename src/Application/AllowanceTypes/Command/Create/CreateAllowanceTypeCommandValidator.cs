using FluentValidation;

namespace hrOT.Application.AllowanceTypes.Command.Create;

public class CreateAllowanceTypeCommandValidator : AbstractValidator<CreateAllowanceTypeCommand>
{
    public CreateAllowanceTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên không được để trống")
            .MinimumLength(5).WithMessage("Tên phải nhiều hơn 5 chữ")
            .MaximumLength(500).WithMessage("Tên phải nhỏ hơn 500 chữ");
            //.MustAsync(BeUniqueName).WithMessage("Tên đã tồn tại.");

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