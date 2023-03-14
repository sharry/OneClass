import React from "react";
import Navbar from "../components/Navbar";
import CourseCard from "../components/CourseCard";
import BottomTabs from "../components/BottomTabs"
import { useIsAuthenticated } from "@azure/msal-react";
import { SignIn } from "../components/SignIn";

function Home() {
	const isAuthenticated = useIsAuthenticated();
	return (
		<div className="">
			{
				isAuthenticated ?
				<>
					<Navbar /> <CourseCard /> <CourseCard /> <CourseCard /><CourseCard />
					<BottomTabs/>
				</>
				: <SignIn />
			}
		</div>
	);
}

export default Home;
