using System.Net.Http.Headers;
using OneClass.Domain.DbModels;

namespace OneClass.WebAPI.Services;

public class UserServices
{
    public static async Task<UserData> GetAuthenticatedUserAsync(
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        // Get the token from the request
        var token = context.Request.Headers.Authorization[0];
        if (token is null)
        {
            throw new Exception("Unauthorized");
        }

        // Setup an HTTP request to the Microsoft Graph API
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token.Replace("Bearer", "")
        );

        // Send the request to the Microsoft Graph API
        var response = await httpClient.GetAsync(
            "https://graph.microsoft.com/v1.0/me",
            cancellationToken
        );

        // Check if the request was successful
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Bad Request");
        }

        // Get the user data from the response
        var user = await response.Content.ReadFromJsonAsync<UserData>(
            cancellationToken: cancellationToken
        );

        // Check if the user data was successfully retrieved
        if (user is null)
        {
            throw new Exception("Bad Request");
        }

        // Return the user data
        return user;
    }
}
