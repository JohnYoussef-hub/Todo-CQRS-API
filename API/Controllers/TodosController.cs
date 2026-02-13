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
public class TodosController(IMediator mediator, ILogger<TodosController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        logger.LogInformation("Getting all todos");
        var result = await mediator.Send(new GetTodosQuery());
        logger.LogInformation("Retrieved {Count} todos", result.Count());
        return Ok(result);
    }

    [HttpGet("{todoId:guid}", Name = "GetTodoById")]
    public async Task<IActionResult> GetTodoById(Guid todoId)
    {
        logger.LogInformation("Getting todo with ID: {TodoId}", todoId);
        var result = await mediator.Send(new GetTodoByIdQuery(todoId));

        if (result is null)
        {
            logger.LogWarning("Todo with ID: {TodoId} not found", todoId);
            return NotFound();
        }

        logger.LogInformation("Found todo: {TodoTitle}", result.Title);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(CreateTodoRequest request)
    {
        logger.LogInformation("Creating new todo: {Title}", request.Title);
        var command = new CreateTodoCommand(request.Title);

        var todoId = await mediator.Send(command);
        logger.LogInformation("Todo created successfully with ID: {TodoId}", todoId);

        return CreatedAtRoute(nameof(GetTodoById), new { todoId }, null);
    }


    [HttpPut("{todoId:guid}")]
    public async Task<IActionResult> UpdateTodo(Guid todoId, UpdateTodoRequest request)
    {
        logger.LogInformation("Updating todo {TodoId} - Title: {Title}, Completed: {Completed}", todoId, request.Title, request.Completed);
        var command = new UpdateTodoCommand(todoId, request.Title, request.Completed);
        await mediator.Send(command);
        logger.LogInformation("Todo {TodoId} updated successfully", todoId);
        return NoContent();
    }

    //delete
    [HttpDelete("{todoId:guid}")]
    public async Task<IActionResult> DeleteTodo(Guid todoId)
    {
        logger.LogInformation("Deleting todo with ID: {TodoId}", todoId);
        var command = new DeleteTodoCommand(todoId);
        await mediator.Send(command);
        logger.LogInformation("Todo {TodoId} deleted successfully", todoId);
        return NoContent();
    }
}