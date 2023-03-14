import React from "react";
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../config/msalConfig";

export const SignInButton = ({ loginType }) => {
	const { instance } = useMsal();

	const handleLogin = (loginType) => {
		if (loginType === "popup") {
			instance.loginPopup(loginRequest).catch(e => {
				console.log(e);
			});
		} else if (loginType === "redirect") {
			instance.loginRedirect(loginRequest).catch(e => {
				console.log(e);
			});
		}
	}
	return (
		<button type="button" onClick={
			() => handleLogin(loginType) }>
			Sign in using Popup
		</button>
	);
}