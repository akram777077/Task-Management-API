using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Middlewares.Attributes;
using TaskMangment.Api.Roles;
using TaskMangment.Api.Routes;
using TaskMangment.Buisness.Models;
using TaskMangment.Buisness.Services.STask;

namespace TaskMangment.Api.Controllers
{
    [Authorize(Roles = KeysRoles.Admin)]
    [Route(TaskMangmentRoute.Base)]
    [ApiController]
    public class TaskMangmentController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskMangmentController(ITaskService taskService,IMapper mapper)
        {
            this._taskService = taskService;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route(TaskMangmentRoute.GetAll)]
        [SkipValidateId]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<ResponseDto>))]
        public async Task<ActionResult<IEnumerable<ResponseDto>>>GetAll()
        {
            var tasksList = await _taskService.GetAllAsync();
            if(tasksList.Count is 0)
                return NoContent();
            var responseList = _mapper.Map<List<ResponseDto>>(tasksList);
            return Ok(responseList);
        }
        [HttpGet]
        [Route(TaskMangmentRoute.Get)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<ResponseDto>> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if(task is null)
                return NotFound();
            var response = _mapper.Map<ResponseDto>(task);
            return Ok(response);
        }
        [HttpPost]
        [Route(TaskMangmentRoute.Create)]
        [SkipValidateId]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponseDto>> Create(CreateTaskRequest newTaskDto)
        {
            var taskModel = _mapper.Map<TaskModel>(newTaskDto);
            var task = await _taskService.CreateAsync(taskModel);
            var response = _mapper.Map<ResponseDto>(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, response);
        }
        [HttpPut]
        [Route(TaskMangmentRoute.Update)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<ResponseDto>> Update(int id, UpdateTaskRequest updatedTaskDto)
        {
            var taskModel = _mapper.Map<TaskModel>(updatedTaskDto,ops => ops.Items["Id"] = id);
            var result = await _taskService.UpdateAsync(taskModel);
            if (!result)
                return NotFound();
            var response = _mapper.Map<ResponseDto>(taskModel);
            return NoContent();
        }
        [HttpDelete]
        [Route(TaskMangmentRoute.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _taskService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch]
        [Route(TaskMangmentRoute.Complete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult> Complete(int id)
        {
            var result = await _taskService.CompleteAsync(id);
            if(!result)
                return NotFound();
            return NoContent();
        }
        [HttpPatch]
        [Route(TaskMangmentRoute.Reopen)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult> Reopen(int id)
        {
            var result = await _taskService.ReopenAsync(id);
            if(!result)
                return NotFound();
            return NoContent();
        }
    }
}
