using Duende.IdentityServer.Extensions;
using hrOT.Application.BankAccounts.Queries;
using hrOT.Application.Degrees;
using hrOT.Application.Degrees.Commands.Create;
using hrOT.Application.Degrees.Commands.Delete;
using hrOT.Application.Degrees.Commands.Update;
using hrOT.Application.Degrees.Queries;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;

namespace WebUI.Controllers.Degree;

[ApiController]
[Route("api/[controller]")]
/*[Authorize(Policy = "ManagerOrStaff")]*/
public class DegreeController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public DegreeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    /*[Authorize(Policy = "manager")]*/
    public async Task<ActionResult<List<DegreeDto>>> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllDegreeQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetListByEmployeeId")]
    public async Task<ActionResult<List<DegreeDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        if (EmployeeId == Guid.Empty) { return BadRequest("EmployeeId trống hoặc không đúng định dạng"); }
        try{
            var result = await _mediator.Send(new GetListDegreeByEmployeeIdQuery(EmployeeId));
           
            return Ok(result);
        }
            
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Create")]
    /*[Authorize(Policy = "ManagerOrStaff")]*/
    public async Task<ActionResult<Guid>> Create(CreateDegreeCommand command)
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

    [HttpPut("Update")]
    public async Task<ActionResult> Update(UpdateDegreeCommand command)
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
    public async Task<ActionResult> Delete([FromForm]DeleteDegreeCommand command)
    {

        try
        {
            await _mediator.Send(command);
            return Ok("Xóa thành công");
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
}