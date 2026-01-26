import { useState } from "react";
import { useNavigate } from "react-router-dom";

function SchoolCreate() {
    const navigate = useNavigate();

    const [name, setName] = useState("");
    const [address, setAddress] = useState("");
    const [studentCount, setStudentCount] = useState<number>(0);

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const createSchool = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");
        setLoading(true);

        try {
            const response = await fetch("/api/school", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    name,
                    address,
                    studentCount,
                }),
            });

            if (!response.ok) {
                const msg = await response.text().catch(() => "");
                throw new Error(msg || "Failed to create school");
            }
            navigate("/");
        } catch (err: any) {
            setError(err?.message ?? "Failed to create school");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <button onClick={() => navigate("/")} style={{ marginBottom: "20px" }}>
                Back to List
            </button>

            <h1>Create New School</h1>

            {error && <div style={{ marginBottom: "10px" }}>{error}</div>}

            <form onSubmit={createSchool}>
                <div style={{ marginBottom: "10px" }}>
                    <label>Name</label>
                    <br />
                    <input
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        type="text"
                        required
                    />
                </div>

                <div style={{ marginBottom: "10px" }}>
                    <label>Address</label>
                    <br />
                    <input
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                        type="text"
                        required
                    />
                </div>

                <div style={{ marginBottom: "10px" }}>
                    <label>Student Count</label>
                    <br />
                    <input
                        value={studentCount}
                        onChange={(e) => setStudentCount(Number(e.target.value))}
                        type="number"
                        min={0}
                        required
                    />
                </div>

                <button type="submit" disabled={loading}>
                    {loading ? "Saving..." : "Create"}
                </button>
            </form>
        </div>
    );
}

export default SchoolCreate;