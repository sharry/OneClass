import React, { useState } from "react";
import { createRoot } from "react-dom/client";
import OneClassContext from "./context/OneClassContext";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import Home from "./pages/Home";
import ToDo from "./pages/ToDo";
import NotificationPage from "./pages/NotificationPage";
import CreateClass from "./pages/CreateClass";
import { MsalProvider } from "@azure/msal-react";
import { PublicClientApplication } from "@azure/msal-browser";
import { msalConfig } from "./config/msalConfig";
import { SignIn } from "./components/SignIn";


const queryClient = new QueryClient({
	defaultOptions: {
		queries: {
			staleTime: 100000,
			cacheTime: 100000,
		},
	},
});

const App = () => {
	const loggedUser = useState(null);
	const msalInstance = new PublicClientApplication(msalConfig);
	return (
        <React.StrictMode>
            <MsalProvider instance={msalInstance}>
                <BrowserRouter>
                    <QueryClientProvider client={queryClient}>
                        <OneClassContext.Provider value={loggedUser}>
                            <heOneClassContexter>
                                <Link to="/">OneClass</Link>
                            </heOneClassContexter>
                            <Routes>
                                <Route path="/sign-in" element={<SignIn />} />
                                <Route path="/" element={<Home />} />
                                <Route path="/create" element={<CreateClass />} />
                                <Route path="/todo" element={<ToDo/>}/>
                                <Route path="/notification" element={<NotificationPage/>}/>
                            </Routes>
                        </OneClassContext.Provider>
                    </QueryClientProvider>
                </BrowserRouter>
            </MsalProvider>
        </React.StrictMode>
    )
};
const container = document.getElementById("root");
const root = createRoot(container);
root.render(<App />);