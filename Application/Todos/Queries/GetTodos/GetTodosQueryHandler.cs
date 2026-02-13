using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Queries.GetTodos;

public sealed class GetTodosQueryHandler(IAppDbContext context)
: IRequestHandler<GetTodosQuery, List<Todo>>
{
    async Task<List<Todo>> IRequestHandler<GetTodosQuery, List<Todo>>.Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return await context.Todos.ToListAsync();
    }
}
