import React from "react";
import { SignInButton } from "../components/SignInButton";
export const SignIn = () => {
	return (
		<div className="flex justify-center items-center py-20 text-center">
			<div>
				<img src="assets/Logo.svg" alt="" className="h-[100px] mb-10" />
				<h1 className="text-4xl mb-5">Sign In</h1>
				<SignInButton loginType="popup" />
			</div>
		</div>
	);
}