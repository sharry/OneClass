import React, { useState } from "react";
import { createRoot } from "react-dom/client";
import ClassList from "./components/ClassList";
import OneClassContext from "./context/OneClassContext";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Link, BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";


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
                    </Routes>
                </OneClassContext.Provider>
            </QueryClientProvider>
        </BrowserRouter>
    )
};
const container = document.getElementById("root");
const root = createRoot(container);
root.render(<App />);