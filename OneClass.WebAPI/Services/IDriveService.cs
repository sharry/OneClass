using Microsoft.Graph.Models;
using OneClass.Domain.DbModels;

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

    public Task ShareFileOrFolderAsync(
        string token,
        string fileOfFolderId,
        string[] userEmails,
        CancellationToken cancellationToken
    );
}
