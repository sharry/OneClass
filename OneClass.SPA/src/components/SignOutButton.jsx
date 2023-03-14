import React from "react";
import { useMsal } from "@azure/msal-react";
import { useNavigate } from "react-router-dom";

export const SignOutButton = ({ logoutType }) => {
	const { instance } = useMsal();
	const navigate = useNavigate();

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

		navigate("/signin");
	}
	return (
		<button
			type="button" className="bg-red-700 text-white px-10 py-2" onClick={() => handleLogout()}>
			Sign out
		</button>
	);
}