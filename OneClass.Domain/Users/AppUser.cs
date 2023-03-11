using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneClass.Domain.Users;
public record AppUser(
	string Id,
	string GivenName,
	string Surname,
	string DisplayName,
	string EmailAddress
);