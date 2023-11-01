using FluentValidation;

namespace hrOT.Application.EmployeeContracts.Commands.Delete;
public class Employee_DeleteContractCommandValidator : AbstractValidator<Employee_DeleteContractCommand>
{
    public Employee_DeleteContractCommandValidator()
    {
        // Validate EmployeeId
        RuleFor(v => v.EmployeeId)
            .NotEmpty().WithMessage("EmployeeId không được để trống.");

        // Validate ContractId
        RuleFor(v => v.ContractId)
            .NotEmpty().WithMessage("ContractId không được để trống.");
    }
}
