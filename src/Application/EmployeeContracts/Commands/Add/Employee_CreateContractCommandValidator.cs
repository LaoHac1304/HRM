using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;

namespace hrOT.Application.EmployeeContracts.Commands.Add;
public class Employee_DeleteContractCommandValidator : AbstractValidator<Employee_CreateContractCommand>
{
    public Employee_DeleteContractCommandValidator()
    {
        // Validate Id
        RuleFor(v => v.EmployeeId)
            .NotEmpty().WithMessage("EmployeeId không được để trống.");

        // Validate File
        RuleFor(v => v.EmployeeContractDTO.File)
            .NotEmpty().WithMessage("File không được để trống.");
            //.MaximumLength(200).WithMessage("File cannot be more than 200 characters long.");

        // Validate StartDate
        RuleFor(v => v.EmployeeContractDTO.StartDate)
            .NotNull().WithMessage("Ngày bắt đầu không được để trống.")
            .LessThan(v => v.EmployeeContractDTO.EndDate).WithMessage("Ngày bắt đầu phải sớm hơn ngày kết thúc.");

        // Validate EndDate
        RuleFor(v => v.EmployeeContractDTO.EndDate)
            .NotNull().WithMessage("Ngày kết thúc không được để trống.")
            .GreaterThan(d => d.EmployeeContractDTO.StartDate).WithMessage("Ngày kết thúc phải trễ hơn ngày bắt đầu");

        // Validate Job
        RuleFor(v => v.EmployeeContractDTO.Job)
            .NotEmpty().WithMessage("Công việc không được để trống");

        // Validate Salary
        RuleFor(v => v.EmployeeContractDTO.Salary)
            .NotNull().WithMessage("Lương không được để trống")
            .GreaterThan(0).WithMessage("Lương không được âm.");

        // Validate CustomSalary
        RuleFor(v => v.EmployeeContractDTO.CustomSalary)
            .NotNull().WithMessage("Lương đóng bảo hiểm khác không được để trống.")
            .GreaterThan(0).WithMessage("Lương đóng bảo hiểm khác không được âm.")
            .GreaterThan(2000000).WithMessage("Lương được điều chỉnh phải lớn hơn hai triệu đồng.")
            .LessThanOrEqualTo(m => m.EmployeeContractDTO.Salary).WithMessage("Custom Salary không được lớn hơn Salary.");
        // Validate Status
        RuleFor(v => v.EmployeeContractDTO.Status)
            .NotEmpty().WithMessage("Trạng thái hợp đồng không được để trống.")
            .IsInEnum().WithMessage("Trạng thái hợp đồng không hợp lệ.");

        /*RuleFor(v => v.EmployeeContractDTO.InsuranceType)
            .IsInEnum().WithMessage("Loại bảo hiểm không hợp lệ.");*/

        RuleFor(v => v.EmployeeContractDTO.SalaryType)
           .NotEmpty().WithMessage("Loại lương không được để trống.")
           .IsInEnum().WithMessage("Loại lương không hợp lệ.");

        RuleFor(v => v.EmployeeContractDTO.ContractType)
            .NotEmpty().WithMessage("Loại hợp đồng không được để trống.")
           .IsInEnum().WithMessage("Loại hợp đồng không hợp lệ.");
    }
}
