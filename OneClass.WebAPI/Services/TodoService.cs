using Microsoft.Graph.Models;

namespace OneClass.WebAPI.Services;

public class TodoService : ITodoService
{
    public async Task<TodoTaskList> CreateTodoListAsync(
        string accessToken,
        string name,
        CancellationToken cancellationToken
    )
    {
        var client = new GraphServiceClientProvider()
            .GetGraphServiceClient(accessToken);
        var list = await client.Me.Todo.Lists.PostAsync(
            new TodoTaskList
            {
                DisplayName = name
            },
            cancellationToken: cancellationToken
        );

        if (list is null)
        {
            throw new Exception("Bad Request");
        }
        return list;
    }

    public async Task<TodoTask> CreateTodoTaskAsync(
        string accessToken,
        string title,
        string content,
        string dueDate,
        bool hasDueDate,
        string listId,
        CancellationToken cancellationToken
    )
    {
        var todoTask = new Microsoft.Graph.Models.TodoTask
        {
            Title = title,
            Body = new ItemBody
            {
                Content = content
            },
            DueDateTime = hasDueDate
                ? new DateTimeTimeZone
                {
                    DateTime = dueDate,
                    TimeZone = "UTC"
                }
                : null
        };
        var client = new GraphServiceClientProvider()
            .GetGraphServiceClient(accessToken);
        var task = await client.Me.Todo.Lists[listId].Tasks.PostAsync(
            todoTask,
            cancellationToken: cancellationToken 
        );

        if (task is null)
        {
            throw new Exception("Bad Request");
        }

        return task;
    }
}
