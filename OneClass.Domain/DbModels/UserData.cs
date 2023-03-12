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
			Id: me.Id,
			GivenName: me.GivenName,
			Surname: me.Surname,
			DisplayName: me.DisplayName,
			EmailAddress: me.UserPrincipalName,
			JoinedClasses: new List<JoinedClass>()
		);
	}
}

public record JoinedClass(
	string ClassId,
	string Role
);