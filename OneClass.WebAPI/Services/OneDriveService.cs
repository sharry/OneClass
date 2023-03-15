using Marten;
using Microsoft.Graph;
using Microsoft.Graph.Drives.Item.Items.Item.Invite;
using Microsoft.Graph.Models;
using OneClass.Domain.DbModels;
using DriveItem = Microsoft.Graph.Models.DriveItem;
using Folder = Microsoft.Graph.Models.Folder;

namespace OneClass.WebAPI.Services;

public class OneDriveService : IDriveService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IDocumentSession _session;
    private readonly IUserService _userService;

    public OneDriveService(
        IAccessTokenService accessTokenService,
        IDocumentSession session,
        IUserService userService
    )
    {
        _accessTokenService = accessTokenService;
        _session = session;
        _userService = userService;
    }

    public async Task<DriveItem> CreateFolderAsync(
        HttpContext context,
        string name,
        string parentId,
        CancellationToken cancellationToken
    )
    {
        var token = _accessTokenService.GetAccessToken(context);
        if (token is null)
        {
            throw new Exception("Unauthorized");
        }
        var client = new GraphServiceClientProvider().GetGraphServiceClient(token);
        var item = await client
            .Drives["me"]
            .Items[parentId]
            .Children
            .PostAsync(
                new DriveItem
                {
                    Name = name,
                    Folder = new Folder()
                }, cancellationToken: cancellationToken
            );
        if (item is null)
        {
            throw new Exception("Error creating folder");
        }
        return item;
    }

    public async Task<DriveItem> CreateClassroomFolderAsync(
        HttpContext context,
        ClassroomData classroom,
        CancellationToken cancellationToken
    )
    {
        var user = await _userService.GetAuthenticatedUserAsync(
            _accessTokenService.GetAccessToken(context),
            cancellationToken
        );
        return await CreateFolderAsync(
            context,
            classroom.Title,
            user.OneClassRootFolderId ?? "root",
            cancellationToken
        );
    }

    public async Task<DriveItem> UploadFileAsync(string accessToken, string onedriveFolderId, Stream file,
        string fileName, CancellationToken cancellationToken = default)
    {
        var client = new GraphServiceClientProvider()
            .GetGraphServiceClient(accessToken);
        var drives = await client
            .Drives
            .GetAsync(cancellationToken: cancellationToken);
        if (drives?.Value is null)
        {
            throw new Exception("Error getting drives");
        } 
        var drive = drives.Value.FirstOrDefault(x => x.DriveType == "personal");
        if (drive is null)
        {
            throw new Exception("Error getting drive");
        }
        await client
            .Drives[drive.Id]
            .Items[onedriveFolderId]
            .ItemWithPath(fileName)
            .Content
            .PutAsync(file, cancellationToken: cancellationToken);
        var driveItem = await client
            .Drives[drive.Id]
            .Items[onedriveFolderId]
            .ItemWithPath(fileName)
            .GetAsync(cancellationToken: cancellationToken);
        if (driveItem is null)
        {
            throw new Exception("Error uploading file");
        }
        return driveItem;
    }

    public async Task<DriveItem> CreateOneClassRootFolderAsync(
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        return await CreateFolderAsync(context, "OneClass", "root", cancellationToken);
    }

    public async Task ShareFileOrFolderAsync(
        string token,
        string fileOfFolderId,
        string[] userEmails,
        CancellationToken cancellationToken
    )
    {
        var recipients = userEmails
            .Select(x => new DriveRecipient
            {
                Email = x
            })
            .ToList();
        var driveItemInvitation = new InvitePostRequestBody
        {
            Roles = new List<string> { "write" },
            Recipients = recipients
        };
        var client = new GraphServiceClientProvider()
            .GetGraphServiceClient(token);
        var drives = await client
            .Drives
            .GetAsync(cancellationToken: cancellationToken);
        if (drives?.Value is null)
        {
            throw new Exception("Error getting drives");
        } 
        var drive = drives.Value.FirstOrDefault(x => x.DriveType == "personal");
        if (drive is null)
        {
            throw new Exception("Error getting drive");
        }
        await client.Drives[drive.Id]
            .Items[fileOfFolderId]
            .Invite.PostAsync(
                driveItemInvitation, 
                cancellationToken: cancellationToken);
    }
}
