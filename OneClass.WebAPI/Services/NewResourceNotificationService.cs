using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using OneClass.Domain.DbModels;

namespace OneClass.WebAPI.Services;

public class NewResourceNotificationService
{
    public async Task notifyViaEmail(ResourceData resource, string[] recipients, string token)
    {
        var email = new NotificationEmail
        {
            Message = new NotificationMessage
            {
                Subject = "New Resource by " + resource.Teacher.Name,
                Body = new NotificationBody
                {
                    Content =
                        "New Resource by "
                        + resource.Teacher.Name
                        + " : \n"
                        + resource.Content.Substring(0, Math.Min(50, resource.Content.Length))
                        + "..."
                },
                ToRecipients = recipients
                    .Select(
                        r =>
                            new NotificationRecipient
                            {
                                EmailAddress = new EmailField { address = r }
                            }
                    )
                    .ToArray()
            }
        };

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );

        var response = await httpClient.PostAsync(
            $"https://graph.microsoft.com/v1.0/me/sendMail",
            new StringContent(
                JsonSerializer.Serialize(
                    email,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                ),
                Encoding.UTF8,
                "application/json"
            )
        );

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            throw new Exception("Bad Request");
        }
    }
}
