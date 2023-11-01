using hrOT.Application.Common.Exceptions;
using hrOT.Application.PaySlips;
using hrOT.Application.PaySlips.Commands.CreatePaySlip;
using hrOT.Application.PaySlips.Queries;
//using hrOT.Application.PaySlips.Queries;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.PaySlips;

public class PaySlipController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public PaySlipController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("TotalSalary")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double?>> GetTotalSalary(DateTime FromDate, DateTime ToDate)
    {
        try
        {
            return await Mediator.Send(new GetTotalSalaryPayForEmployeeQuery(FromDate, ToDate));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("TotalCostOfInsurance")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double?>> GetTotalCostOfInsurance(DateTime FromDate, DateTime ToDate)
    {
        try
        {
            return await Mediator.Send(new GetTotalCostOfInsurance(FromDate, ToDate));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("TotalTaxIncome")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double?>> GetTotalTaxIncome(DateTime FromDate, DateTime ToDate)
    {
        try
        {
            return await Mediator.Send(new GetTotalTaxIncomeQuery(FromDate, ToDate));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetPayslipByEmployeeId")]
    public async Task<ActionResult<List<PaySlipDto>>> Get(Guid EmployeeId)
    {
        try
        {
            return await Mediator.Send(new GetListPaySlipByEmployeeIdQuery(EmployeeId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /*[HttpPost("CreateByEachMonth")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> CreateByEachMonth(CreatePaySlipByEachMonthCommand command)
    {
        try
        {
            if (ModelState.IsValid && command != null)
            {
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            return BadRequest("Thêm thất bại");
        }
        catch (Exception ex)
        {
            if (ex is ValidationException)
            {
                ValidationException error = (ValidationException)ex;
                var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                return BadRequest(errorsDiction);
            }
            return BadRequest(ex.Message);
        }
    }*/

    [HttpPost("CreateByRangeDate")]
    public async Task<ActionResult<Guid>> CreateByRangeDate(CreatePaySlipByRangeDateCommand command)
    {
        try
        {
            if (ModelState.IsValid && command != null)
            {
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            return BadRequest("Thêm thất bại");
        }
        catch (Exception ex)
        {
            if (ex is ValidationException)
            {
                ValidationException error = (ValidationException)ex;
                var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                return BadRequest(errorsDiction);
            }
            return BadRequest(ex.Message);
        }
    }

    /*[HttpPost("CreatePaySlipForAllEmployee")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> CreatePaySlipForAllEmployee(CreateAllPaySlipCommand command)
    {
        *//*if (ModelState.IsValid && command != null)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        return BadRequest("Thêm thất bại");*//*

        try
        {
            if (ModelState.IsValid && command != null)
            {
                var result = await Mediator.Send(command);
                return result.Contains("thành công")
                ? Ok(result)
                : BadRequest(result);
            }
            return BadRequest("Thêm thất bại");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }*/

    /* [HttpGet]
     public async Task<ActionResult<HttpResponseMessage>> GetPaySlipById(Guid id)
     {
         var query = new GetPaySlipByIdQuery { Id = id };
         var response = await _mediator.Send(query);

         if (response == null)
         {
             return NotFound();
         }

         return response;
     }

     [HttpGet("{id}/DownloadPdf")]
     public async Task<IActionResult> DownloadPdf(Guid id)
     {
         var query = new GetPaySlipByIdQuery { Id = id };
         var response = await Mediator.Send(query);

         if (response.StatusCode == HttpStatusCode.NotFound)
         {
             return NotFound();
         }

         var pdfBytes = await response.Content.ReadAsByteArrayAsync();
         return File(pdfBytes, "application/pdf", "paySlip.pdf");
     }*/

    [HttpGet("GetPaySlipByDateRange")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<List<PaySlipDto>>> GetByDateRange(DateTime fromDate, DateTime toDate)
    {
        try
        {
            return await _mediator.Send(new GetListPaySlipByDate(fromDate, toDate));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("TotalSalaryOfCompany")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double>> GetTotalSalaryOfCompany()
    {
        try
        {
            var query = new GetTotalSalaryOfCompanyQuery();
            var totalSalary = await _mediator.Send(query);
            return totalSalary;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("TotalSalaryOfDepartment")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double>> GetTotalSalaryOfDepartment(Guid id)
    {
        try
        {
            var query = new GetTotalSalaryOfDepartmentQuery(id);
            var totalSalary = await _mediator.Send(query);

            return totalSalary;
        }
        catch (NotFoundException ex)
        {
            // Handle the NotFoundException
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            return BadRequest(ex.Message);
            //return StatusCode(500, "An error occurred.");
        }
    }
}