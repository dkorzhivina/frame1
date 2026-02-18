using frame1.Models;
using frame1.Services;
using Microsoft.AspNetCore.Mvc;

namespace frame1.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] int? difficulty)
    {
        var items = _service.GetAll();

        if (difficulty.HasValue)
        {
            items = items.Where(x => x.Difficulty == difficulty.Value);
        }

        return Ok(items);
    }


    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var item = _service.GetById(id);
        if (item == null)
            throw new KeyNotFoundException("Задача не найдена");

        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TaskItem item)
    {
        var created = _service.Create(item);
        return Ok(created);
    }
}
