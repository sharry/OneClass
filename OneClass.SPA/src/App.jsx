import React, { useState } from "react";
import { createRoot } from "react-dom/client";
import OneClassContext from "./context/OneClassContext";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Link, BrowserRouter, Routes, Route } from "react-router-dom";
import { PublicClientApplication } from "@azure/msal-browser";
import { MsalProvider } from "@azure/msal-react";
import { msalConfig } from "./config/msalConfig";
import Home from "./pages/Home";
import ToDo from "./pages/ToDo";
import NotificationPage from "./pages/NotificationPage";

const msalInstance = new PublicClientApplication(msalConfig);

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
    return (
        <BrowserRouter>
            <QueryClientProvider client={queryClient}>
                <OneClassContext.Provider value={loggedUser}>
                    <heOneClassContexter>
                    </heOneClassContexter>
                    <Routes>
                        <Route path="/" element={<Home/>}/>
                        <Route path="/todo" element={<ToDo/>}/>
                        <Route path="/notification" element={<NotificationPage/>}/>
                    </Routes>
                </OneClassContext.Provider>
            </QueryClientProvider>
        </BrowserRouter>
    )
};
const container = document.getElementById("root");
const root = createRoot(container);
root.render(<App />);