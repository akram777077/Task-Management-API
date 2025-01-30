using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Roles;
using TaskMangment.Api.Routes;
using TaskMangment.Buisness.Models;
using TaskMangment.Buisness.Services.STask;

namespace TaskMangment.Api.Controllers
{
    [Route(TaskRoute.Base)]
    [ApiController]
    [Authorize(Roles = KeysRoles.User)]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService,IMapper mapper)
        {
            this._taskService = taskService;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route(TaskRoute.GetAll)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<ResponseDto>))]
        public async Task<ActionResult<IEnumerable<ResponseDto>>> GetAll()
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var tasksList = await _taskService.GetByUserAsync(userName);
            if(tasksList.Count is 0)
                return NoContent();
            var responseList = _mapper.Map<List<ResponseDto>>(tasksList);
            return Ok(responseList);
        }
        [HttpPost]
        [Route(TaskRoute.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponseDto>> Create(CreateTaskRequest newTaskDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var taskWithUser = _mapper.Map<CreateTaskWithUserDto>(newTaskDto, ops => ops.Items["username"] = username);
            var taskModel = _mapper.Map<TaskModel>(taskWithUser);
            var task = await _taskService.AssignToUserAsync(taskModel);
            var response = _mapper.Map<ResponseDto>(task);
            return Created();
        }
        [HttpPut]
        [Route(TaskRoute.Update)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<ResponseDto>> Update(int id, UpdateTaskRequest updatedTaskDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var taskWithUser = _mapper.Map<UpdateTaskWithUser>(updatedTaskDto, ops => ops.Items["username"] = username);
            var taskModel = _mapper.Map<TaskModel>(taskWithUser, ops => ops.Items["id"] = id);
            var result = await _taskService.UpdateFromUserAsync(taskModel);
            if (!result)
                return NotFound();
            var response = _mapper.Map<ResponseDto>(taskModel);
            return NoContent();
        }
        [HttpDelete]
        [Route(TaskRoute.Delete)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult> Delete(int id)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _taskService.RemoveFromUserAsync(id,username);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch]
        [Route(TaskRoute.Reopen)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult> Reopen(int id)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _taskService.ReopenOfUserAsync(id,username);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
