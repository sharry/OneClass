using OneClass.Domain.DbModels;

namespace OneClass.WebAPI.Classroom;

public record ClassroomResponse(
	string Id,
	string Title,
	string Description,
	string Image,
	TeacherResponse Teacher,
	string[] StudentIds,
	string JoinCode,
	string OneDriveFolderId,
	int AssignmentsCount
);

public record TeacherResponse(
	string Id,
	string GivenName,
	string Surname,
	string DisplayName,
	string EmailAddress
);