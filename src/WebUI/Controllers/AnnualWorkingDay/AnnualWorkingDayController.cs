using hrOT.Application.AnnualWorkingDays.Queries.Create;
using hrOT.Application.Common.Exceptions;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.AnnualWorkingDay;

public class AnnualWorkingDayController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AnnualWorkingDayController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateEx")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> CreateEx(IFormFile file)
    {
        try
        {
            if (file != null && file.Length > 0)
            {
                // Kiểm tra kiểu tệp tin
                if (!IsExcelFile(file))
                {
                    return BadRequest("Chỉ cho phép sử dụng file Excel");
                }

                var filePath = Path.GetTempFileName(); // Tạo một tệp tạm để lưu trữ tệp Excel
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream); // Lưu tệp Excel vào tệp tạm
                }

                var command = new CreateAnnualWorkingDayEx
                {
                    FilePath = filePath
                };

                try
                {
                    await _mediator.Send(command);
                    return Ok("Thêm thành công");
                }
                catch (NotFoundException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (InvalidDataException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("File không được trống");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private bool IsExcelFile(IFormFile file)
    {
        // Kiểm tra phần mở rộng của tệp tin có phải là .xls hoặc .xlsx không
        var allowedExtensions = new[] { ".xls", ".xlsx" };
        var fileExtension = Path.GetExtension(file.FileName);
        return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
    }
}