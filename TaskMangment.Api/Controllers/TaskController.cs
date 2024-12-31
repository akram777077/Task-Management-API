using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Routes;
using TaskMangment.Buisness.Models;
using TaskMangment.Buisness.Services.STask;

namespace TaskMangment.Api.Controllers
{
    [Route(TaskRoute.Base)]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<ResponseDto>>>getAll()
        {
            var tasksList = await _taskService.GetAllAsync();
            if(tasksList.Count is 0)
                return NoContent();
            var responseList = _mapper.Map<List<ResponseDto>>(tasksList);
            return Ok(responseList);
        }
        [HttpGet]
        [Route(TaskRoute.Get)]
        public async Task<ActionResult<ResponseDto>> getById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if(task is null)
                return NotFound();
            var response = _mapper.Map<ResponseDto>(task);
            return Ok(response);
        }
        [HttpPost]
        [Route(TaskRoute.Create)]
        public async Task<ActionResult<ResponseDto>> create(CreateTaskRequest newTaskDto)
        {
            var taskModel = _mapper.Map<TaskModel>(newTaskDto);
            var task = await _taskService.CreateAsync(taskModel);
            var response = _mapper.Map<ResponseDto>(task);
            return CreatedAtAction(nameof(getById), new { id = task.Id }, response);
        }
        [HttpPut]
        [Route(TaskRoute.Update)]
        public async Task<ActionResult<ResponseDto>> update(int id, UpdateTaskRequest updatedTaskDto)
        {
            var taskModel = _mapper.Map<TaskModel>(updatedTaskDto,ops => ops.Items["Id"] = id);
            var result = await _taskService.UpdateAsync(taskModel);
            if (!result)
                return NotFound();
            var response = _mapper.Map<ResponseDto>(taskModel);
            return NoContent();
        }
        [HttpDelete]
        [Route(TaskRoute.Delete)]
        public async Task<ActionResult> delete(int id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }
    }
}
