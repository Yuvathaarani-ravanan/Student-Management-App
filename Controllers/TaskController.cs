using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;
using StudentManagementApp.Services;

namespace StudentManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskModel task)
        {
            // Validate allowed status values
            var validStatuses = new[] { "On Progress", "Completed" };
            if (!validStatuses.Contains(task.TaskStatus))
                return BadRequest("Invalid task status. Must be 'On Progress' or 'Completed'.");

            await _taskService.CreateAsync(task);
            return Ok("Task added successfully.");
        }
    }
}
