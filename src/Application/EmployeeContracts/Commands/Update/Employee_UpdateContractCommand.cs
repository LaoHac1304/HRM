﻿using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using hrOT.Application.Common.Exceptions;
namespace hrOT.Application.EmployeeContracts.Commands.Update;

public class Employee_UpdateContractCommand : IRequest<string>
{
    public Guid EmployeeId { get; set; }
    public Guid ContractId { get; set; }
    public EmployeeContractCommandDTO EmployeeContract { get; set; }

    public Employee_UpdateContractCommand(Guid contractId, Guid employeeId, EmployeeContractCommandDTO employeeContract)
    {
        ContractId = contractId;
        EmployeeId = employeeId;
        EmployeeContract = employeeContract;
    }
}

public class Employee_UpdateContractCommandHandler : IRequestHandler<Employee_UpdateContractCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public Employee_UpdateContractCommandHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }
    //..................................................................
    public override bool Equals(object? obj)
    {
        return obj is Employee_UpdateContractCommandHandler handler &&
               EqualityComparer<IMapper>.Default.Equals(_mapper, handler._mapper);
    }
    //....................................................................
    public async Task<string> Handle(Employee_UpdateContractCommand request, CancellationToken cancellationToken)
    {
        var employee = _context.Employees
                    .Where(e => e.Id == request.EmployeeId)
                    .FirstOrDefault();
        if (employee == null)
        {
            throw new NotFoundException($"Em ID: {request.ContractId} đã bị xóa hoặc không tồn tại.");
        }


            var contract = _context.EmployeeContracts
                .Where(c => c.EmployeeId == employee.Id && c.Id == request.ContractId)
                .FirstOrDefault();

            if (contract != null && contract.IsDeleted == false)
            {
                contract.File = UploadFile(request);
                contract.StartDate = request.EmployeeContract.StartDate;
                contract.EndDate = request.EmployeeContract.EndDate;
                contract.Job = request.EmployeeContract.Job;
                contract.Salary = request.EmployeeContract.Salary;
                contract.CustomSalary = request.EmployeeContract.CustomSalary;
                contract.Status = request.EmployeeContract.Status;
                //contract.InsuranceType = request.EmployeeContract.InsuranceType;
                contract.SalaryType = request.EmployeeContract.SalaryType;
                contract.ContractType = request.EmployeeContract.ContractType;

                _context.EmployeeContracts.Update(contract);
                await _context.SaveChangesAsync(cancellationToken);
                return "Cập nhật thành công";
            }
            else
            {
                throw new NotFoundException($"Contract ID: {request.ContractId} đã bị xóa hoặc không tồn tại.");
            }

        
    }

    private String UploadFile(Employee_UpdateContractCommand request)
    {
        /*try
        {*/
        var file = request.EmployeeContract.File;
        if (file != null && file.Length > 0)
        {
            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Specify the directory to save the CV files
            string uploadDirectory = _configuration.GetSection("UploadDirectory").Value;

            // Combine the directory and file name to get the full file path
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            // Update the CVPath property of the employee
            return filePath;
        }
        /*} catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }*/

        return null;
    }
}