using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Marten;
using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public class OneDriveService : IOneDriveService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IDocumentSession _session;

    public OneDriveService(IAccessTokenService accessTokenService, IDocumentSession session)
    {
        _accessTokenService = accessTokenService;
        _session = session;
    }

    public async Task<DriveItem> CreateClassroomFolderAsync(
        HttpContext context,
        ClassroomData classroom,
        CancellationToken cancellationToken
    )
    {
        var token = _accessTokenService.GetAccessToken(context);
        if (token is null)
        {
            throw new Exception("Unauthorized");
        }

        var driveItem = new DriveItemRequest($"OneClass - {classroom.Title}", new Folder { });

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );
        var response = await httpClient.PostAsync(
            "https://graph.microsoft.com/v1.0/me/drive/root/children",
            new StringContent(
                JsonSerializer.Serialize(
                    driveItem,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }
                ),
                Encoding.UTF8,
                "application/json"
            ),
            cancellationToken
        );

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
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
}
