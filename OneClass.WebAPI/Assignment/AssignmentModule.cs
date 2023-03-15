using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Carter;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Me.SendMail;
using Microsoft.Graph.Models;
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

		app.MapGet("/api/assignments/{assignmentId}", 
		async (
			string assignmentId,
			HttpContext context,
			IUserService userService,
			IDocumentSession session,
			CancellationToken cancellationToken
			) =>
		{
			var accessToken = _accessTokenService.GetAccessToken(context);
			var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
			var assignment = await session
				.Query<ClassroomAssignment>()
				.Where(x => x.Id == assignmentId)
				.FirstOrDefaultAsync(cancellationToken);
			
			return Results.Ok(assignment);
		});
		
		app.MapPost("/api/classrooms/{classroomId}/assignments",
		async (
			HttpContext context,
			IUserService userService,
			IDocumentSession session,
			IEmailService emailService,
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
			var studentsEmails = await session
				.Query<UserData>()
				.Where(u => u.JoinedClasses
					.Any(c => c.ClassroomId == classroomId && c.Role == "Student"))
				.Select(u => u.EmailAddress)
				.ToListAsync(cancellationToken);
			await emailService.SendEmailAsync(
				accessToken,
				assignment.Title,
				assignment.Content ?? string.Empty,
				studentsEmails,
				cancellationToken
			);
			return Results.Ok(classroomAssignment);
		});
		app.MapDelete("/api/classrooms/{classroomId}/assignments/{assignmentId}",
			async (
				HttpContext context,
				IUserService userService,
				IDocumentSession session,
				CancellationToken cancellationToken,
				string classroomId,
				string assignmentId) =>
			{
				var accessToken = _accessTokenService.GetAccessToken(context);
				var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
				var classroom = await session
					.Query<ClassroomData>()
					.Where(x => x.Id == classroomId)
					.FirstOrDefaultAsync(token: cancellationToken);
				if (classroom is null)
				{
					return Results.NotFound();
				}
				if (classroom.TeacherId != user.Id)
				{
					
				}
				var assignment = await session
					.Query<ClassroomAssignment>()
					.Where(x => x.Id == assignmentId)
					.FirstOrDefaultAsync(token: cancellationToken);
				if (assignment is null)
				{
					return Results.NotFound();
				}
				session.Delete(assignment);
				await session.SaveChangesAsync(cancellationToken);
				return Results.Ok();
			});

		app.MapPost(
			"/api/classrooms/{classroomId}/assignments/{assignmentId}/todo", 
			async (
				HttpContext context,
				IUserService userService,
				ITodoService todoService,
				IDocumentSession session,
				CancellationToken cancellationToken,
				string classroomId,
				string assignmentId
			) =>
			{
				var accessToken = _accessTokenService.GetAccessToken(context);
				var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
				var classroom = await session
					.Query<ClassroomData>()
					.Where(x => x.Id == classroomId)
					.FirstOrDefaultAsync(token: cancellationToken);
				if (classroom is null)
				{
					return Results.NotFound();
				}
				if (!user.JoinedClasses.Any(x => x.ClassroomId == classroomId && x.Role == "Student"))
				{
					return Results.Unauthorized();
				}

				var assignment = await session
					.Query<ClassroomAssignment>()
					.Where(x => x.Id == assignmentId)
					.FirstOrDefaultAsync(token: cancellationToken);
				if (assignment is null)
				{
					return Results.NotFound();
				}

				var todoListId = user.TodoLists
					.Where(x => x.ClassroomId == classroomId)
					.Select(x => x.MsTodoId)
					.FirstOrDefault();

				if (todoListId is not null) {
					var todo = await todoService.CreateTodoTaskAsync(
						accessToken, 
						assignment.Title,
						assignment.Content,
						assignment.DueDate,
						assignment.HasDueDate,
						todoListId,
						cancellationToken
					);
				}

				return Results.Ok();
			}
		);
	}
}
