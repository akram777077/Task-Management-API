using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMangment.Api.DTOs;
using TaskMangment.Buisness.Services.STask;

namespace TaskMangment.Api.Controllers
{
    [Route("api/Task")]
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
        [Route("/")]
        public async Task<ActionResult<IEnumerable<ResponseDto>>>getAll()
        {
            var tasksList = await _taskService.GetAllAsync();
            if(tasksList.Count is 0)
                return NoContent();
            var responseList = _mapper.Map<List<ResponseDto>>(tasksList);
            return Ok(responseList);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponseDto>> getById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if(task is null)
                return NotFound();
            var response = _mapper.Map<ResponseDto>(task);
            return Ok(response);
        }
    }
}
