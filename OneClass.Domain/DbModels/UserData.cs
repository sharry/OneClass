using OneClass.Domain.GraphModels;
using OneClass.Domain.Users;

namespace OneClass.Domain.DbModels;
using ClassId = String;
using ClassRole = String;

public record UserData(
	string Id,
	string GivenName,
	string Surname,
	string DisplayName,
	string EmailAddress,
	IEnumerable<JoinedClass> JoinedClasses
)
{
	public static UserData FromMe(Me me)
	{
		return new UserData(
			me.Id,
			me.DisplayName,
			me.GivenName,
			me.Surname,
			me.UserPrincipalName,
			new List<JoinedClass>()
		);
	}
}
// private List<ClassId> JoinedClasses { get; set; } = new();
// public void JoinClass(ClassId classId)
// 	=> JoinedClasses.Add(classId);
// public void LeaveClass(ClassId classId)
// 	=> JoinedClasses.RemoveAll(x => x == classId);
// public bool IsInClass(ClassId classId)
// 	=> JoinedClasses.Any(x => x == classId);
// public IEnumerable<ClassId> GetJoinedClasses()
// 	=> JoinedClasses;

public record JoinedClass(
	string ClassId,
	string Role
);