using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClass.Domain.Classes;
public sealed record Class(
	Guid Id,
	string Name,
	string Description,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt
);