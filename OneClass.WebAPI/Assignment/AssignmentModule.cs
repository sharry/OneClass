﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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
			var studentsEmails = await session
				.Query<UserData>()
				.Where(u => u.JoinedClasses
					.Any(c => c.ClassroomId == classroomId && c.Role == "Student"))
				.Select(u => u.EmailAddress)
				.ToListAsync(cancellationToken);
			var httpClient = new HttpClient();
			var emailRequest = new EmailMessage(
				assignment.Title,
				new EmailMessageBody(assignment.Content ?? string.Empty, "Text"),
				studentsEmails.Select(x => new EmailMessageToRecipient(
					new EmailMessageToRecipientEmailAddress(x))).ToArray());
			var emailRequestJson = JsonSerializer.Serialize(new { Message = emailRequest }, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			});
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			var emailResponse = await httpClient
				.PostAsync("https://graph.microsoft.com/v1.0/me/sendMail",
					new StringContent(emailRequestJson, Encoding.UTF8, "application/json"),
					cancellationToken);
			return Results.Ok(classroomAssignment);
		});
	}
}
