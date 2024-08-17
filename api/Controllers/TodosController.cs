using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize(Policy = "ApiCaller")]
[Authorize(Policy = "InteractiveUser")]
[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    private static readonly List<ToDo> data = new List<ToDo>()
        {
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow, Name = "Demo ToDo API", User = "2 (Bob Smith)" },
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow.AddHours(1), Name = "Stop Demo", User = "2 (Bob Smith)" },
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow.AddHours(4), Name = "Have Dinner", User = "1 (Alice Smith)" },
        };

    private readonly ILogger<TodosController> _logger;

    public TodosController(ILogger<TodosController> logger)
    {
        _logger = logger;
    }

    // GET: api/todo
    [HttpGet]
    public IActionResult GetAllToDos()
    {
        return Ok(data);
    }

    //GET: api/todo/{id}
    [HttpGet("{id:int}")]
    public IActionResult GetToDoById(int id)
    {
        var todo = data.FirstOrDefault(x => x.Id == id);
        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    // POST: api/todo
    [HttpPost]
    public IActionResult CreateToDo([FromBody] ToDo model)
    {
        if (model == null)
            return BadRequest();

        model.Id = ToDo.NewId();
        model.User = $"{User.FindFirst("sub")?.Value} ({User.FindFirst("name")?.Value})";

        data.Add(model);

        return CreatedAtAction(nameof(GetToDoById), new { id = model.Id }, model);
    }

    // PUT: api/todo/{id}
    [HttpPut("{id:int}")]
    public IActionResult UpdateToDo(int id, [FromBody] ToDo model)
    {
        var todo = data.FirstOrDefault(x => x.Id == id);
        if (todo == null)
            return NotFound();

        todo.Name = model.Name;
        todo.Date = model.Date;

        return NoContent();
    }
}

public class ToDo
{
    static int _nextId = 1;
    public static int NewId()
    {
        return _nextId++;
    }

    public int Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? Name { get; set; }
    public string? User { get; set; }
}
