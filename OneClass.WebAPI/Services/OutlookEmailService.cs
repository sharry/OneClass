using Microsoft.Graph.Me.SendMail;
using Microsoft.Graph.Models;

namespace OneClass.WebAPI.Services;

public class OutlookEmailService : IEmailService
{
	public async Task SendEmailAsync(
		string accessToken,
		string subject,
		string content,
		IEnumerable<string> recipients,
		CancellationToken cancellationToken)
	{
		var client = new GraphServiceClientProvider()
			.GetGraphServiceClient(accessToken);
		var emailRequest = new SendMailPostRequestBody
		{
			Message = new Message
			{
				Subject = subject,
				Body = new ItemBody
				{
					ContentType = BodyType.Text,
					Content = content
				},
				ToRecipients = recipients.Select(x => new Recipient
				{
					EmailAddress = new EmailAddress
					{
						Address = x
					}
				}).ToList()
			}
		};
		await client.Me.SendMail.PostAsync(emailRequest, cancellationToken: cancellationToken);
	}
}