using System.Net.Http.Headers;
using Carter;
using Marten;
using OneClass.Domain.DbModels;
using OneClass.WebAPI.Services;

namespace OneClass.WebAPI.User;

public class UserModule : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/users/me", 
			async (
				IDocumentSession session,
				HttpContext context,
				IAccessTokenService atService,
				IUserService userService,
				CancellationToken cancellationToken,
				IOneDriveService oneDriveService) =>
			{
			var accessToken = atService.GetAccessToken(context);
			UserData user;
			try
			{
				user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
			}
			catch (Exception)
			{
				return Results.Unauthorized();
			}
			var existingUser = await session
				.Query<UserData>()
				.FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);
			if (existingUser is not null)
			{
				return Results.Ok(existingUser); 
			}

			var folder = await oneDriveService.CreateOneClassRootFolderAsync(context, cancellationToken);
			
			user.OneClassRootFolderId = folder.Id;
			session.Store(user);
			await session.SaveChangesAsync(cancellationToken);
			return Results.Ok(user);
		});
		app.MapGet("/api/users/me/photo", async (
			HttpContext context,
			CancellationToken cancellationToken,
			IStorage storage,
			IAccessTokenService atService) =>
		{
			var token = atService.GetAccessToken(context);
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var request = new HttpRequestMessage(HttpMethod.Get, 
				"https://graph.microsoft.com/v1.0/me/photo/$value");
			var response = await httpClient.SendAsync(request, cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				return Results.Unauthorized();
			}
			var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
			return Results.Stream(stream, "image/png");
		});
	}
}