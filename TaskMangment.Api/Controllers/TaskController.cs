using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Roles;
using TaskMangment.Api.Routes;
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
    }
}
