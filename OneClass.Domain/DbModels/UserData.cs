using OneClass.Domain.GraphModels;

namespace OneClass.Domain.DbModels;
using ClassId = String;
using ClassRole = String;

public sealed class UserData
{
	public string Id { get; set; }
	public string GivenName { get; set; }
	public string Surname { get; set; }
	public string DisplayName { get; set; }
	public string EmailAddress { get; set; }
	public string OneClassRootFolderId { get; set; }
	public JoinedClass[] JoinedClasses { get; set; }
	public UserData(
		string id,
		string givenName,
		string surname,
		string displayName,
		string emailAddress,
		string oneClassRootFolderId,
		List<JoinedClass> joinedClasses
	)
	{
		Id = id;
		GivenName = givenName;
		Surname = surname;
		DisplayName = displayName;
		EmailAddress = emailAddress;
		OneClassRootFolderId = oneClassRootFolderId;
		JoinedClasses = joinedClasses.ToArray();
	}
	public static UserData FromMe(Me me)
	{
		return new UserData(
			me.Id,
			me.GivenName,
			me.Surname,
			me.DisplayName,
			me.UserPrincipalName,
			string.Empty,
			new List<JoinedClass>()
		);
	}
	public void JoinClass(string classId, string role)
		=> JoinedClasses = JoinedClasses
			.Append(new (classId, role))
			.ToArray();
}

public record JoinedClass(
	string ClassId,
	string Role
);