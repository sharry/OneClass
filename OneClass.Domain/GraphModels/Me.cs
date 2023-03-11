using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClass.Domain.GraphModels;
public record Me(
	string Id,
	string DisplayName,
	string GivenName,
	string Surname,
	string UserPrincipalName
);
