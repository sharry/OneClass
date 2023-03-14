using Carter;
using Marten;
using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;
using OneClass.WebAPI.Services;

namespace OneClass.WebAPI.Assignment;

public class WorkSubmissionModule : ICarterModule
{
	private readonly IAccessTokenService _accessTokenService;
	private readonly IDriveService _driveService;
	public WorkSubmissionModule(IAccessTokenService accessTokenService, IDriveService driveService)
	{
		_accessTokenService = accessTokenService;
		_driveService = driveService;
	}

	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/classrooms/{classroomId}/assignments/{assignmentId}",
		async (
			HttpContext context,
			IUserService userService,
			HttpRequest request,
			IDocumentSession session,
			string classroomId,
			string assignmentId,
			CancellationToken cancellationToken) =>
		{
			if (!request.HasFormContentType)
			{
				return Results.BadRequest();
			}
			var form = await request.ReadFormAsync(cancellationToken);
			if (form.Files.Any() == false)
			{
				return Results.BadRequest("There are no files");
			}
			var file = form.Files.FirstOrDefault();
			if (file is null || file.Length == 0)
			{
				return Results.BadRequest("File cannot be empty");
			}
			if (file.Length > 4 * 1024 * 1024)
			{
				return Results.BadRequest("File cannot be larger than 4MB");
			}

			var accessToken = _accessTokenService.GetAccessToken(context);
			var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
			var isStudent = user
				.JoinedClasses.Any(x => x.ClassroomId == classroomId && x.Role == "Student");
			if (!isStudent)
			{
				return Results.Unauthorized();
			}
			var classroom = await session
				.Query<ClassroomData>()
				.FirstOrDefaultAsync(x => x.Id == classroomId, cancellationToken);
			if (classroom is null)
			{
				return Results.BadRequest("Classroom does not exist");
			}
			var onedriveFolderId = classroom.OneDriveFolderId;
			await using var stream = file.OpenReadStream();
			DriveItem? data = null;
			try
			{
				data = await _driveService.UploadFileAsync(
					accessToken,
					onedriveFolderId,
					stream,
					$"{file.FileName}",
					cancellationToken);
			}
			catch (Exception e)
			{
				Results.BadRequest(e);
			}
			if (data == null)
			{
				return Results.BadRequest();
			}
			return Results.Ok(data);
		});
	}
}

public record WorkSubmission(string UserId, string ClassroomId, string AssignmentId);