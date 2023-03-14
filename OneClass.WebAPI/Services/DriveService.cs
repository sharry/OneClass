using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Marten;
using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public class DriveService : IDriveService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IDocumentSession _session;
    private readonly IUserService _userService;

    public DriveService(
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

        var driveItem = new DriveItemRequest(name, new Folder { });

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );

        var response = await httpClient.PostAsync(
            $"https://graph.microsoft.com/v1.0/me/drive/items/{parentId}/children",
            new StringContent(
                JsonSerializer.Serialize(
                    driveItem,
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

        var folder = await response.Content.ReadFromJsonAsync<DriveItem>(
            cancellationToken: cancellationToken
        );

        if (folder is null)
        {
            throw new Exception("Bad Request");
        }

        return folder;
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
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            accessToken
        );
        var response = await httpClient.PostAsync($"https://graph.microsoft.com/v1.0/me/drive/items/{onedriveFolderId}/children",
            new StreamContent(file)
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/octet-stream"),
                    ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = fileName
                    }
                }
            },
            cancellationToken
        );
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Bad Request");
        }
        var driveItem = await response.Content.ReadFromJsonAsync<DriveItem>(
            cancellationToken: cancellationToken
        );
        if (driveItem is null)
        {
            throw new Exception("Bad Request");
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

    public async void ShareFileOrFolderAsync(
        HttpContext context,
        string fileOfFolderId,
        string[] userEmails,
        CancellationToken cancellationToken
    )
    {
        var token = _accessTokenService.GetAccessToken(context);
        if (token is null)
        {
            throw new Exception("Unauthorized");
        }

        var recipients = userEmails
            .Select(x => new Recipient(x))
            .ToArray();
        var driveItemInvitation = new DriveItemInvitation(
            new[] { "write" },
            recipients
        );

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );

        var response = await httpClient.PostAsync(
            $"https://graph.microsoft.com/v1.0/me/drive/items/{fileOfFolderId}/invite",
            new StringContent(
                JsonSerializer.Serialize(
                    driveItemInvitation,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                ),
                Encoding.UTF8,
                "application/json"
            ),
            cancellationToken
        );

        if (response.IsSuccessStatusCode) return;
        Console.WriteLine(await response.Content.ReadAsStringAsync(cancellationToken));
        throw new Exception("Bad Request");
    }
}
