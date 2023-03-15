import React, { useState } from "react";
import { createRoot } from "react-dom/client";
import OneClassContext from "./context/OneClassContext";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import ToDo from "./pages/ToDo";
import NotificationPage from "./pages/NotificationPage";
import ClassRessources from "./pages/ClassRessources";
import ClassResourcesContent from "./pages/ClassRessourcesContent";

import CreateClass from "./pages/CreateClass";
import { MsalProvider } from "@azure/msal-react";
import { PublicClientApplication } from "@azure/msal-browser";
import { msalConfig } from "./config/msalConfig";
import Assignement from "./pages/Assignement";
import AssignmentContent from "./pages/AssignementContent";
import Members from "./pages/Members";
import { SignIn } from "./pages/SignIn";
import JoinClass from "./pages/JoinClass";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 100000,
      cacheTime: 100000,
    },
  },
});

const msalInstance = new PublicClientApplication(msalConfig);
const App = () => {
  const loggedUser = useState(null);
  return (
    <React.StrictMode>
      <MsalProvider instance={msalInstance}>
        <BrowserRouter>
          <QueryClientProvider client={queryClient}>
            <OneClassContext.Provider value={loggedUser}>
              <Routes>
                <Route path="/signin" element={<SignIn />} />
                <Route path="/" element={<Home />} />
                <Route path="/todo" element={<ToDo />} />
                <Route path="/notification" element={<NotificationPage />} />
                <Route path="/create" element={<CreateClass />} />
                <Route path="/join" element={<JoinClass />} />
                <Route path="/classrources/:id" element={<ClassRessources />} />
                <Route
                  path="/classrources/classcontent"
                  element={<ClassResourcesContent />}
                />
                <Route
                  path="/classrources/:id/assignment"
                  element={<Assignement />}
                />
                <Route path="/classrources/:id/members" element={<Members />} />
                <Route
                  path="/assignmentcontent"
                  element={<AssignmentContent />}
                />
              </Routes>
            </OneClassContext.Provider>
          </QueryClientProvider>
        </BrowserRouter>
      </MsalProvider>
    </React.StrictMode>
  );
};
const container = document.getElementById("root");
const root = createRoot(container);
root.render(<App />);
