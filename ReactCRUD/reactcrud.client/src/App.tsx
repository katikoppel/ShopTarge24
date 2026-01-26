/*import { useEffect, useState } from 'react';*/
import './App.css';
import SchoolList from "./views/SchoolList";
import SchoolDetail from "./views/SchoolDetail";
import SchoolCreate from "./views/SchoolCreate";
import { BrowserRouter, Routes, Route } from "react-router-dom"


function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<SchoolList />} />
                <Route path="/details/:id" element={<SchoolDetail />} />
                <Route path="/school/create" element={<SchoolCreate />} />
            </Routes>
        </BrowserRouter>
    )
}

export default App;