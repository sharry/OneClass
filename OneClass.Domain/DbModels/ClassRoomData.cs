namespace OneClass.Domain.DbModels;

public record ClassRoomData(
    string Id,
    string Title,
    string Description,
    string Image,
    string TeacherId
);
