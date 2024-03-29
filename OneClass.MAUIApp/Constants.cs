﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClass.MAUIApp;
public static class Constants
{
	public static string AccessTokenSecureStorageKey = "access_token";
	public static string AccessTokenExpirationDateSecureStorageKey = "access_token_expiration_date";
	public static string ClientId = "896a45d1-095f-416d-baf3-ff19493fc2c1";
	public static string[] Scopes = new[]
	{
		"User.Read",
		"User.ReadBasic.All",
		"Tasks.ReadWrite",
		"Calendars.ReadWrite",
		"Mail.ReadWrite",
		"Files.ReadWrite",
		"Files.ReadWrite.All",
	};
}