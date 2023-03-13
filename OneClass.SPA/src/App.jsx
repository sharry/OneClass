import React, { useState } from "react";
import { createRoot } from "react-dom/client";
import ClassList from "./components/ClassList";
import OneClassContext from "./context/OneClassContext";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Link, BrowserRouter, Routes, Route } from "react-router-dom";
import { PublicClientApplication } from "@azure/msal-browser";
import { MsalProvider } from "@azure/msal-react";
import { msalConfig } from "./config/msalConfig";

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
      <React.StrictMode>
          <MsalProvider instance={msalInstance}>
            <BrowserRouter>
                <QueryClientProvider client={queryClient}>
                    <OneClassContext.Provider value={loggedUser}>
                        <heOneClassContexter>
                            <Link to="/">OneClass</Link>
                        </heOneClassContexter>
                        <Routes>
                            <Route path="/classes" element={<ClassList />} />
                            <Route path="/" element={<ClassList />} />
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