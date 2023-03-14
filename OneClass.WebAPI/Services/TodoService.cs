using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Marten;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public class TodoService : ITodoService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IDocumentSession _session;
    private readonly IUserService _userService;

    public TodoService(
        IAccessTokenService accessTokenService,
        IDocumentSession session,
        IUserService userService
    )
    {
        _accessTokenService = accessTokenService;
        _session = session;
        _userService = userService;
    }

    public async Task<TodoList> CreateTodoListAsync(
        HttpContext context,
        string name,
        CancellationToken cancellationToken
    )
    {
        var token = _accessTokenService.GetAccessToken(context);
        if (token is null)
        {
            throw new Exception("Unauthorized");
        }

        var todoList = new TodoListRequest(name);

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );

        var response = await httpClient.PostAsync(
            $"https://graph.microsoft.com/v1.0/me/todo/lists",
            new StringContent(
                JsonSerializer.Serialize(
                    todoList,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                ),
                Encoding.UTF8,
                "application/json"
            ),
            cancellationToken
        );

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Bad Request");
        }

        var list = await response.Content.ReadFromJsonAsync<TodoList>(
            cancellationToken: cancellationToken
        );

        if (list is null)
        {
            throw new Exception("Bad Request");
        }

        return list;
    }

    public async Task<TodoTask> CreateTodoTaskAsync(
        HttpContext context,
        string title,
        string content,
        string dueDate,
        string listId,
        CancellationToken cancellationToken
    )
    {
        var token = _accessTokenService.GetAccessToken(context);
        if (token is null)
        {
            throw new Exception("Unauthorized");
        }

        var todoTask = new TodoTaskRequest(
            title,
            new TodoTaskBody(content),
            new TodoTaskDueDate(dueDate)
        );

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );

        var response = await httpClient.PostAsync(
            $"https://graph.microsoft.com/v1.0/me/todo/lists/{listId}/tasks",
            new StringContent(
                JsonSerializer.Serialize(
                    todoTask,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                ),
                Encoding.UTF8,
                "application/json"
            ),
            cancellationToken
        );

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            throw new Exception("Bad Request");
        }

        var task = await response.Content.ReadFromJsonAsync<TodoTask>(
            cancellationToken: cancellationToken
        );

        if (task is null)
        {
            throw new Exception("Bad Request");
        }

        return task;
    }
}
