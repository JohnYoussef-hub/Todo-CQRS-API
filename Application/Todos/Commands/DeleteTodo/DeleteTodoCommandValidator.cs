using FluentValidation;

namespace Application.Todos.Commands.DeleteTodo;

public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithName("Id").WithMessage("{PropertyName} is required.");
    }
}
