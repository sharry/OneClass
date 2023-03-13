namespace OneClass.Domain.GraphModels;

public record DriveItem(
    string Id,
    string Name
);

public record DriveItemRequest(
    string Name,
    Folder Folder
);
