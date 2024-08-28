using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task_Management_System.Core.Dto;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Service;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserTasksController : ControllerBase
{
    private readonly IUserTaskService _usertaskService;

    public UserTasksController(IUserTaskService usertaskService)
    {
        _usertaskService = usertaskService;
    }

    [HttpGet("GetAllTasks")]
    public async Task<IActionResult> GetAllTasks(string? search, int? pageIndex, int? pageSize)
    {
        var currentPageIndex = pageIndex ?? 1;
        var currentPageSize = pageSize ?? 10;

        var tasks = await _usertaskService.GetAllTasks(search, currentPageIndex, currentPageSize);
        return Ok(tasks);
    }

    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask([FromBody] UserTaskDto dto)
    {
        await _usertaskService.CreateTaskAsync(dto);
        return Ok(dto);
    }

    [HttpGet("GetTasksByUserId/{id}")]
    public async Task<IActionResult> GetTaskByUserId(int id)
    {
        return Ok(await _usertaskService.GetTasksByUserIdAsync(id));
    }

    [HttpDelete("DeleteTask/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _usertaskService.DeleteTask(id));
    }


    [HttpPut("UpdateTask")]
    public async Task<IActionResult> UpdateTask([FromBody] CreateAndUpdateTaskDto dto, int id)
    {
        return Ok(await _usertaskService.UpdateTask(dto, id));
    }

    
    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateStatus(int userTaskId, int newTaskStatusTypeId)
    {
        return Ok(await _usertaskService.UpdateStatusAndAddNewAsync(userTaskId, newTaskStatusTypeId));
    }

    [HttpGet("GetHistoryStatusTask/{id}")]
    public async Task<IActionResult> GetHistoryStatusTask(int id)
    {
        return Ok(await _usertaskService.GetHistoryStatusTask(id));
    }
}

