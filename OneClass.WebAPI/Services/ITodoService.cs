using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public interface ITodoService
{
    public Task<TodoList> CreateTodoListAsync(
        HttpContext context,
        string name,
        CancellationToken cancellationToken
    );

    public Task<TodoTask> CreateTodoTaskAsync(
        HttpContext context,
        string title,
        string content,
        string dueDate,
        bool hasDueDate,
        string listId,
        CancellationToken cancellationToken
    );
}