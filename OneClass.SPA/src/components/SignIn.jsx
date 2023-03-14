import React from "react";
import { SignInButton } from "./SignInButton";
import { useIsAuthenticated } from "@azure/msal-react";
export const SignIn = () => {
	const isAuthenticated = useIsAuthenticated();
	return (
		<div>
			<h1>Sign In</h1>
			{ isAuthenticated ? <span>Signed In</span> : <SignInButton loginType="popup" /> }
		</div>
	);
}