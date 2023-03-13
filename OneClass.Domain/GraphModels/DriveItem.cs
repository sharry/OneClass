namespace OneClass.Domain.GraphModels;

public record DriveItem(
    string Id,
    string Name
);

public record DriveItemRequest(
    string Name,
    Folder Folder
);

public record DriveItemInvitation(
    String[] roles,
    Recipient[] Recipients,
    bool SendInvitation = false
);

public record Recipient(
    string Email
);
