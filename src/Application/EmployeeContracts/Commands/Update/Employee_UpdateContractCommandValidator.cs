using System.Diagnostics.Metrics;
using FluentValidation;
using hrOT.Application.Common.Interfaces;

namespace hrOT.Application.EmployeeContracts.Commands.Update;

public class Employee_UpdateContractCommandValidator : AbstractValidator<Employee_UpdateContractCommand>
{
    private readonly IApplicationDbContext _context;

    public Employee_UpdateContractCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        // Validate EmployeeId
        RuleFor(v => v.EmployeeId)
            .NotEmpty().WithMessage("EmployeeId không được để trống.");

        // Validate ContractId
        RuleFor(v => v.ContractId)
            .NotEmpty().WithMessage("ContractId không được để trống.");

        // Validate File
        RuleFor(v => v.EmployeeContract.File)
            .NotEmpty().WithMessage("File không được để trống.");
        //.MaximumLength(200).WithMessage("File cannot be more than 200 characters long.");

        // Validate StartDate
        RuleFor(v => v.EmployeeContract.StartDate)
            .NotNull().WithMessage("Ngày bắt đầu không được để trống.")
            .LessThan(v => v.EmployeeContract.EndDate).WithMessage("Ngày bắt đầu phải sớm hơn ngày kết thúc.");

        // Validate EndDate
        RuleFor(v => v.EmployeeContract.EndDate)
            .NotNull().WithMessage("Ngày kết thúc không được để trống.")
            .GreaterThan(d => d.EmployeeContract.StartDate).WithMessage("Ngày kết thúc phải trễ hơn ngày bắt đầu");

        // Validate Job
        RuleFor(v => v.EmployeeContract.Job)
            .NotEmpty().WithMessage("Công việc không được để trống");

        // Validate Salary
        RuleFor(v => v.EmployeeContract.Salary)
            .NotNull().WithMessage("Lương không được để trống")
            .GreaterThan(0).WithMessage("Lương không được âm.");

        // Validate CustomSalary
        RuleFor(v => v.EmployeeContract.CustomSalary)
            .NotNull().WithMessage("Lương đóng bảo hiểm khác không được để trống.")
            .GreaterThan(0).WithMessage("Lương đóng bảo hiểm khác không được âm.")
            .GreaterThan(2000000).WithMessage("Lương được điều chỉnh phải lớn hơn hai triệu đồng.")
            .LessThanOrEqualTo(m => m.EmployeeContract.Salary).WithMessage("Custom Salary không được lớn hơn Salary.");
        // Validate Status
        RuleFor(v => v.EmployeeContract.Status)
            //.NotNull().WithMessage("Trạng thái hợp đồng không được để trống.")
            .NotEmpty().WithMessage("Trạng thái hợp đồng không được để trống")
            .IsInEnum().WithMessage("Trạng thái hợp đồng không hợp lệ.");

      /*  RuleFor(v => v.EmployeeContract.InsuranceType)
            .NotEmpty().WithMessage("Loại bảo hiểm không được để trống")
            .IsInEnum().WithMessage("Loại bảo hiểm không hợp lệ.");*/

        RuleFor(v => v.EmployeeContract.SalaryType)
            .NotEmpty().WithMessage("Loại lương không được để trống")
           .IsInEnum().WithMessage("Loại lương không hợp lệ.");

        RuleFor(v => v.EmployeeContract.ContractType)
            .NotEmpty().WithMessage("Loại hợp đồng không được để trống")
           .IsInEnum().WithMessage("Loại hợp đồng không hợp lệ.");
    }

    
}
