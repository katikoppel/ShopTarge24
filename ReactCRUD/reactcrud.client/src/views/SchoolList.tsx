import { useCallback, useEffect, useState } from "react";
import type { School } from "../types/school";
import { useNavigate } from "react-router-dom";
import React from "react";

function SchoolList() {
    const [schools, setSchools] = useState<School[]>([]);
    const navigate = useNavigate();

    const fetchSchools = useCallback(async () => {
        try {
            const response = await fetch("/api/school");
            if (response.ok) {
                const data = await response.json();
                setSchools(data);
            }
        } catch (error) {
            console.error("Fetch error:", error);
        }
    }, []);

    useEffect(() => {
        fetchSchools();
    }, [fetchSchools]);

    return (
        <div className="container">
            <h1>School List</h1>

            <button
                onClick={() => navigate("/school/create")}
                style={{ marginBottom: "20px" }}
            >
                Create New School
            </button>

            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Address</th>
                        <th>Student Count</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {schools.map((school) => (
                        <tr key={school.id}>
                            <td>{school.id}</td>
                            <td>{school.name}</td>
                            <td>{school.address}</td>
                            <td>{school.studentCount}</td>
                            <td>
                                <button onClick={() => navigate(`/details/${school.id}`)}>
                                    Details
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default SchoolList;