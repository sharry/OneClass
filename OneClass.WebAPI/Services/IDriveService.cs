using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public interface IDriveService
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
    public Task<DriveItem> UploadFileAsync(
        string accessToken,
        string onedriveFolderId,
        Stream file,
        string fileName,
        CancellationToken cancellationToken = default
    );
    public Task<DriveItem> CreateOneClassRootFolderAsync(
        HttpContext context,
        CancellationToken cancellationToken
    );

    public void ShareFileOrFolderAsync(
        HttpContext context,
        string fileOfFolderId,
        string[] userEmails,
        CancellationToken cancellationToken
    );
}
