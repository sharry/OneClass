namespace OneClass.Domain.GraphModels;

public record TodoList(string Id, string Name);

public record TodoListRequest(string displayName);
