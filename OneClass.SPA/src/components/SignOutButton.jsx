import React from "react";
import { useMsal } from "@azure/msal-react";

export const SignOutButton = ({ logoutType }) => {
	const { instance } = useMsal();
	const handleLogout = () => {
		if (logoutType === "popup") {
			instance.logoutPopup({
				postLogoutRedirectUri: "/",
				mainWindowRedirectUri: "/"
			}).catch(console.error);
		} else if (logoutType === "redirect") {
			instance.logoutRedirect({
				postLogoutRedirectUri: "/",
			}).catch(console.error);
		}
	}
	return (
		<button
			type="button" className="ml-auto" onClick={() => handleLogout()}>
			Sign out
		</button>
	);
}