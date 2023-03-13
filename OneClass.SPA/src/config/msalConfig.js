export const msalConfig = {
	auth: {
		clientId: "896a45d1-095f-416d-baf3-ff19493fc2c1",
		authority: "https://login.microsoftonline.com/common",
		redirectUri: "http://localhost:3000",
	},
	cache: {
		cacheLocation: "sessionStorage", // This configures where your cache will be stored
		storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
	}
};
export const loginRequest = {
	scopes: [
		"User.Read",
		"User.ReadBasic.All",
		"Tasks.ReadWrite",
		"Calendars.ReadWrite",
		"Mail.ReadWrite",
		"Files.ReadWrite",
		"Files.ReadWrite.All"
	]
};
export const graphConfig = {
	graphMeEndpoint: "https://graph.microsoft.com/v1.0/me"
};