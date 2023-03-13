using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public interface IOneDriveService
{
    public Task<DriveItem> CreateClassroomFolderAsync(
        HttpContext context,
        ClassroomData classroom,
        CancellationToken cancellationToken
    );
}
