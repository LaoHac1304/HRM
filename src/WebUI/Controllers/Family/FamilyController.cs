using hrOT.Application.Departments.Queries.GetTotalEmployees;
using hrOT.Application.Employees;
using hrOT.Application.Families;
using hrOT.Application.Families.Commands.Create;
using hrOT.Application.Families.Commands.Delete;
using hrOT.Application.Families.Commands.Update;
using hrOT.Application.Families.Queries;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;


namespace WebUI.Controllers.Family;

public class FamilyController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public FamilyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<List<FamilyDto>>> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllFamilyQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetListByEmployeeId")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult<List<FamilyDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        if (EmployeeId == Guid.Empty) { return BadRequest("EmployeeId trống hoặc không đúng định dạng"); }
        try
        {
            var result = await Mediator.Send(new GetListFamilyByEmployeeIdQuery(EmployeeId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Create")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult<Guid>> Create([FromForm] CreateFamilyCommand command)
    {
        try
        {
            var request = await Mediator.Send(command);
            return Ok(request);
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

    [HttpPut("Update")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult> UpdateStatus([FromForm] UpdateFamilyCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
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

    [HttpDelete("Delete")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult> Delete([FromForm] DeleteFamilyCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok("Xóa thành công!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}