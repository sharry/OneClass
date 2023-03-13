import React, { useState } from "react";
import { createRoot } from "react-dom/client";
import ClassList from "./components/ClassList";



const App = () => {

    return (
        <>
        <ClassList />
        </>
    )
};
const container = document.getElementById("root");
const root = createRoot(container);
root.render(<App />);