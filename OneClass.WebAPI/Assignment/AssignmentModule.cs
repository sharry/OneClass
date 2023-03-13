using Carter;
using Marten;
using Microsoft.AspNetCore.Mvc;
using OneClass.Domain.DbModels;
using OneClass.Domain.Utils;
using OneClass.WebAPI.Services;

namespace OneClass.WebAPI.Assignment;

public class AssignmentModule : ICarterModule
{
	private readonly IAccessTokenService _accessTokenService;
	public AssignmentModule(IAccessTokenService accessTokenService)
		=> _accessTokenService = accessTokenService;

	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/classrooms/{classroomId}/assignments", 
		async (
			HttpContext context,
			IUserService userService,
			IDocumentSession session,
			CancellationToken cancellationToken, 
			string classroomId) =>
		{
			var accessToken = _accessTokenService.GetAccessToken(context);
			var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
			var assignments = await session
				.Query<ClassroomAssignment>()
				.Where(x => x.ClassroomId == classroomId)
				.ToListAsync(cancellationToken);
			return Results.Ok(assignments);
		});
		app.MapPost("/api/classrooms/{classroomId}/assignments",
		async (
			HttpContext context,
			IUserService userService,
			IDocumentSession session,
			[FromBody] ClassroomAssignmentRequest assignment,
			string classroomId,
			CancellationToken cancellationToken) =>
		{
			var accessToken = _accessTokenService
				.GetAccessToken(context);
			var user = await userService
				.GetAuthenticatedUserAsync(accessToken, cancellationToken);
			var isTeacher = user.JoinedClasses
				.Any(x => x.ClassroomId == classroomId && x.Role == "Teacher");
			if (!isTeacher)
			{
				return Results.Unauthorized();
			}

			if (assignment is { HasDueDate: true, DueDate: null })
			{
				return Results.BadRequest();
			}
			var classroomAssignment = new ClassroomAssignment(user.Id, classroomId)
			{
				Title = assignment.Title,
				Content = assignment.Content ?? string.Empty,
				HasDueDate = assignment.HasDueDate ?? false,
				DueDate = assignment.DueDate ?? DateTime.MinValue.ToIso8601String()
			};
			session.Store(classroomAssignment);
			await session.SaveChangesAsync(cancellationToken);
			return Results.Ok(classroomAssignment);
		});
	}
}