using Carter;
using Marten;
using Microsoft.AspNetCore.Mvc;
using OneClass.Domain.DbModels;
using OneClass.WebAPI.Services;

namespace OneClass.WebAPI.Classroom;

public class ResourceModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/classrooms/{classroomId}/resources",
            async (
                string classroomId,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );

                var classroom = await session
                    .Query<ClassroomData>()
                    .FirstOrDefaultAsync(x => x.Id == classroomId, cancellationToken);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id && classroom.StudentIds.All(x => x != user.Id))
                {
                    return Results.Unauthorized();
                }

                var resources = await session
                    .Query<ResourceData>()
                    .Where(x => x.ClassroomId == classroomId)
                    .ToListAsync(cancellationToken);

                return Results.Ok(resources);
            }
        );

        app.MapGet(
            "/api/resources/{resourceId}",
            async (
                string resourceId,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );

                var resource = await session
                    .Query<ResourceData>()
                    .FirstOrDefaultAsync(x => x.Id == resourceId, cancellationToken);

                if (resource is null)
                {
                    return Results.NotFound();
                }

                var classroom = await session
                    .Query<ClassroomData>()
                    .FirstOrDefaultAsync(x => x.Id == resource.ClassroomId, cancellationToken);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id && classroom.StudentIds.All(x => x != user.Id))
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(resource);
            }
        );

        app.MapPost(
            "/api/classrooms/{classroomId}/resources",
            async (
                string classroomId,
                ResourceData resource,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );

                var classroom = await session
                    .Query<ClassroomData>()
                    .FirstOrDefaultAsync(x => x.Id == classroomId, cancellationToken);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id)
                {
                    return Results.Unauthorized();
                }

                resource.Id = Guid.NewGuid().ToString();
                resource.Teacher = new Teacher
                {
                    Id = user.Id,
                    Name = user.DisplayName
                };
                resource.ClassroomId = classroomId;
                resource.CreatedAt = DateTime.UtcNow.ToString();

                session.Store(resource);
                await session.SaveChangesAsync(cancellationToken);

                return Results.Ok(resource);
            }
        );

        app.MapPost(
            "/api/resources/{resourceId}/attachments",
            async (
                string resourceId,
                AttachedFile attachment,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );

                var resource = session
                    .Query<ResourceData>()
                    .FirstOrDefault(x => x.Id == resourceId);

                if (resource is null)
                {
                    return Results.NotFound();
                }

                var classroom = session
                    .Query<ClassroomData>()
                    .FirstOrDefault(x => x.Id == resource.ClassroomId);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id)
                {
                    return Results.Unauthorized();
                }

                // TODO: Upload attachment to cloud storage
                // ...

                resource.AddAttachment(attachment);
                session.Store(resource);
                session.SaveChanges();

                return Results.Ok(resource);
            }
        );

        app.MapPut(
            "/api/resources/{resourceId}",
            async (
                string resourceId,
                ResourceData resourceData,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );

                var resource = await session
                    .Query<ResourceData>()
                    .FirstOrDefaultAsync(x => x.Id == resourceId, cancellationToken);

                if (resource is null)
                {
                    return Results.NotFound();
                }

                var classroom = await session
                    .Query<ClassroomData>()
                    .FirstOrDefaultAsync(x => x.Id == resource.ClassroomId, cancellationToken);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id)
                {
                    return Results.Unauthorized();
                }

                resource.Content = resourceData.Content;
                session.Store(resource);
                await session.SaveChangesAsync(cancellationToken);

                return Results.Ok(resource);
            }
        );

        app.MapDelete(
            "/api/resources/{resourceId}",
            (
                string resourceId,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                ).Result;

                var resource = session
                    .Query<ResourceData>()
                    .FirstOrDefault(x => x.Id == resourceId);

                if (resource is null)
                {
                    return Results.NotFound();
                }

                var classroom = session
                    .Query<ClassroomData>()
                    .FirstOrDefault(x => x.Id == resource.ClassroomId);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id)
                {
                    return Results.Unauthorized();
                }

                session.Delete(resource);
                session.SaveChanges();

                return Results.Ok();
            }
        );
    }
}
