using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace hrOT.Application.EmployeeContracts.Commands.Add;

public class Employee_CreateContractCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public EmployeeContractCommandDTO EmployeeContractDTO { get; set; }

    public Employee_CreateContractCommand(Guid employeeId, EmployeeContractCommandDTO employeeContractDTO)
    {
        EmployeeId = employeeId;
        EmployeeContractDTO = employeeContractDTO;
    }
}

public class Employee_CreateContractCommandHandler : IRequestHandler<Employee_CreateContractCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Employee_CreateContractCommandHandler(IApplicationDbContext context, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _configuration = configuration;
    }

    public async Task<Guid> Handle(Employee_CreateContractCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .Where(e => e.Id == request.EmployeeId)
            .FirstOrDefaultAsync(cancellationToken);
        if (employee == null)
        {
            throw new NotFoundException($"Không tìm thấy nhân viên có ID: {request.EmployeeId}");
        }
        var employeeContract = await _context.EmployeeContracts
                .Where(e => e.EmployeeId == request.EmployeeId && e.Status == EmployeeContractStatus.Effective && e.IsDeleted == false)
                .FirstOrDefaultAsync(cancellationToken);
        if (employeeContract != null)
        {
            throw new Exception($"Nhân viên này đang có hợp đồng chưa hết hạn, không thể tạo hợp đồng mới");
        }
        var contract = new EmployeeContract
        {
            Id = new Guid(),
            EmployeeId = request.EmployeeId,
            File = UploadFile(request),
            StartDate = request.EmployeeContractDTO.StartDate,
            EndDate = request.EmployeeContractDTO.EndDate,
            Job = request.EmployeeContractDTO.Job,
            Salary = request.EmployeeContractDTO.Salary,
            //InsuranceType = request.EmployeeContractDTO.InsuranceType,
            CustomSalary = request.EmployeeContractDTO.CustomSalary,
            Status = EmployeeContractStatus.Effective,
            SalaryType = request.EmployeeContractDTO.SalaryType,
            ContractType = request.EmployeeContractDTO.ContractType,
        };
        if (true) { }
        await _context.EmployeeContracts.AddAsync(contract);
        await _context.SaveChangesAsync(cancellationToken);
        return contract.Id;
    }


    private String UploadFile(Employee_CreateContractCommand request)
    {

        var file = request.EmployeeContractDTO.File;
        if (file != null && file.Length > 0)
        {
            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Specify the directory to save the contract files
            string wwwrootdir = _webHostEnvironment.WebRootPath;

            // Combine the directory and file name to get the full file path
            var filePath = Path.Combine(wwwrootdir, "Uploads\\" + request.EmployeeId + "\\Contract\\");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);

            }
            filePath += fileName;
            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            // Update the ContractPath property of the employee
            return filePath;
        }

        return null;
    }
}