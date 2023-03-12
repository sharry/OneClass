using OneClass.Domain.GraphModels;

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

public record JoinedClass(
	string ClassId,
	string Role
);