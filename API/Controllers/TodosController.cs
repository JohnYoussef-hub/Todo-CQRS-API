using API.Requests;
using Application.Todos.Commands.CreateTodo;
using Application.Todos.Commands.DeleteTodo;
using Application.Todos.Commands.UpdateTodo;
using Application.Todos.Queries.GetTodoById;
using Application.Todos.Queries.GetTodos;
using Domain.Todos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await mediator.Send(new GetTodosQuery());
        return Ok(result);
    }

    [HttpGet("{todoId:guid}", Name = "GetTodoById")]
    public async Task<IActionResult> GetTodoById(Guid todoId)
    {
        var result = await mediator.Send(new GetTodoByIdQuery(todoId));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(CreateTodoRequest request)
    {
        var command = new CreateTodoCommand(request.Title);

        var todoId = await mediator.Send(command);

        return CreatedAtRoute(nameof(GetTodoById), new { todoId }, null);
    }


    [HttpPut("{todoId:guid}")]
    public async Task<IActionResult> UpdateTodo(Guid todoId, UpdateTodoRequest request)
    {
        var command = new UpdateTodoCommand(todoId, request.Title, request.Completed);
        await mediator.Send(command);
        return NoContent();
    }

    //delete
    [HttpDelete("{todoId:guid}")]
    public async Task<IActionResult> DeleteTodo(Guid todoId)
    {
        var command = new DeleteTodoCommand(todoId);
        await mediator.Send(command);
        return NoContent();
    }
}