import React from "react";
import { SignInButton } from "./SignInButton";
export const SignIn = () => {
	return (
		<div>
			<h1>Sign In</h1>
			<SignInButton loginType="popup" />
		</div>
	);
}