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
				CancellationToken cancellationToken,
				IDriveService oneDriveService) =>
			{
			var accessToken = atService.GetAccessToken(context);
			var client = new GraphServiceClientProvider()
				.GetGraphServiceClient(accessToken);
			var graphUser = await client.Me.GetAsync(cancellationToken: cancellationToken);
			if (graphUser is null)
			{
				return Results.BadRequest();
			}
			UserData user = UserData.FromGraphUser(graphUser);
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
			var client = new GraphServiceClientProvider()
				.GetGraphServiceClient(token);
			var stream = await client.Me.Photo.Content.GetAsync(cancellationToken: cancellationToken);
			if (stream is null)
			{
				return Results.NotFound();
			}
			return Results.Stream(stream, "image/png");
		});
	}
}