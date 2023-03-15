using Microsoft.Graph.Models;

namespace OneClass.WebAPI.Services;

public interface ITodoService
{
    public Task<TodoTaskList> CreateTodoListAsync(
        string accessToken,
        string name,
        CancellationToken cancellationToken
    );

    public Task<TodoTask> CreateTodoTaskAsync(
        string accessToken,
        string title,
        string content,
        string dueDate,
        bool hasDueDate,
        string listId,
        CancellationToken cancellationToken
    );
}