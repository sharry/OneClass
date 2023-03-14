import React, { useState } from "react";
import { createRoot } from "react-dom/client";
import OneClassContext from "./context/OneClassContext";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import Home from "./pages/Home";
import ToDo from "./pages/ToDo";
import NotificationPage from "./pages/NotificationPage";
import ClassRessources from "./pages/ClassRessources";
import ClassResourcesContent from "./pages/ClassRessourcesContent";

const msalInstance = new PublicClientApplication(msalConfig);
import CreateClass from "./pages/CreateClass";
import { MsalProvider } from "@azure/msal-react";
import { PublicClientApplication } from "@azure/msal-browser";
import { msalConfig } from "./config/msalConfig";
import Assignement from "./pages/Assignement";
import AssignmentContent from "./pages/AssignementContent";
import Members from "./pages/Members";
import { SignIn } from "./pages/SignIn";

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
                            </heOneClassContexter>
                            <Routes>
                                <Route path="/signin" element={<SignIn/>}/>
                                <Route path="/" element={<Home/>}/>
                                <Route path="/todo" element={<ToDo/>}/>
                                <Route path="/notification" element={<NotificationPage/>}/>
                                <Route path="/create" element={<CreateClass />} />
                                <Route path="/classrources" element={<ClassRessources/>}/>
                                <Route path="/classrources/classcontent" element={<ClassResourcesContent/>}/>
                                <Route path="/classrources/assignment" element={<Assignement/>}/>
                                <Route path="/classrources/members" element={<Members/>}/>
                                <Route path="/assignmentcontent" element={<AssignmentContent/>}/>  
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