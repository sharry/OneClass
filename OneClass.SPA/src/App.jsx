import React, { useState } from "react";
import { createRoot } from "react-dom/client";
import ClassList from "./components/ClassList";
import OneClassContext from "./context/OneClassContext";

const queryClient = new QueryClient({
    defaultOptions: {
        queries: {
            staleTime: 100000,
            cacheTime: 100000,
        },
    },
});

const App = () => {

    return (
        <BrowserRouter>
            <QueryClientProvider client={queryClient}>
                <OneClassContext.Provider value={adoptedPet}>
                    <header>
                        <Link to="/">OneClass</Link>
                    </header>
                    <Routes>
                        <Route path="/classes" element={<ClassList />} />
                        <Route path="/" element={<ClassList />} />
                    </Routes>
                </OneClassContext.Provider>
            </QueryClientProvider>
        </BrowserRouter>
    )
};
const container = document.getElementById("root");
const root = createRoot(container);
root.render(<App />);