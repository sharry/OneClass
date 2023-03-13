using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public interface IOneDriveService
{
    public Task<DriveItem> CreateFolderAsync(
        HttpContext context,
        string name,
        string parentId,
        CancellationToken cancellationToken
    );

    public Task<DriveItem> CreateClassroomFolderAsync(
        HttpContext context,
        ClassroomData classroom,
        CancellationToken cancellationToken
    );

    public Task<DriveItem> CreateOneClassRootFolderAsync(
        HttpContext context,
        CancellationToken cancellationToken
    );
}
