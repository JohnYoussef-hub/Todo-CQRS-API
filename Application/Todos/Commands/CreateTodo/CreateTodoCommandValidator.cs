using Application.Todos.Commands.CreateTodo;
using FluentValidation;

namespace Application.Todos.Commands.CreateTod;

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
    }
}
