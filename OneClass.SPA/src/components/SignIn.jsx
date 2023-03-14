import React from "react";
import { SignInButton } from "./SignInButton";
export const SignIn = () => {
	return (
		<div className="w-full h-full flex justify-center align-middle">
			<h1>Sign In</h1>
			<SignInButton loginType="popup" />
		</div>
	);
}