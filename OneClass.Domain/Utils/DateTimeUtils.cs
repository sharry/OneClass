using System.Globalization;

namespace OneClass.Domain.Utils;

public static class DateTimeUtils
{
	public static string ToIso8601String(this DateTime dateTime)
	{
		return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
	}
}