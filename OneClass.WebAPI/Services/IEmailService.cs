using Azure.Core;

namespace OneClass.WebAPI.Services;

public interface IEmailService
{
	Task SendEmailAsync(
		string accessToken,
		string subject,
		string content,
		IEnumerable<string> recipients,
		CancellationToken cancellationToken = default
	);
}