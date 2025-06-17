import React, { useState } from "react";

function App() {
    const [userName, setUserName] = useState("");
    const [userId, setUserId] = useState(null);
    const [manualUserId, setManualUserId] = useState(""); // NEW
    const [entry, setEntry] = useState({
        weight: "",
        height: "",
        age: "",
        sex: "male",
        formulaChoice: "MifflinStJeorFormula",
        activityChoice: "Sedentry",
    });
    const [tdeeResult, setTdeeResult] = useState(null);
    const [savedTDEE, setSavedTDEE] = useState(null);
    const [error, setError] = useState(null);

    const createUser = async () => {
        setError(null);
        try {
            const res = await fetch("/api/tdee/users", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ name: userName }),
            });
            const data = await res.json();
            setUserId(data.userId);
            setSavedTDEE(null);
            setTdeeResult(null);
        } catch (err) {
            setError("Failed to create user.");
        }
    };

    const calculateTDEE = async () => {
        setError(null);
        try {
            const res = await fetch("/api/tdee/tdee/calculate", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    weight: Number(entry.weight),
                    height: Number(entry.height),
                    age: Number(entry.age),
                    sex: entry.sex,
                    formulaChoice: entry.formulaChoice,
                    activityChoice: entry.activityChoice,
                    userId: userId,
                }),
            });
            const data = await res.json();
            setTdeeResult(data.tdee);
        } catch (err) {
            setError("Failed to calculate TDEE.");
        }
    };

    const getSavedTDEE = async (idToFetch = userId) => {
        setError(null);
        try {
            const res = await fetch(`/api/tdee/users/${idToFetch}/tdee`);
            if (!res.ok) {
                throw new Error("User not found or no saved TDEE");
            }
            const data = await res.json();
            setSavedTDEE(data.tdee);
        } catch (err) {
            setError(err.message || "Failed to get saved TDEE.");
        }
    };

    return (
        <div style={{ maxWidth: 600, margin: "auto", padding: 20, fontFamily: "Arial, sans-serif" }}>
            <h1>TDEE Helper</h1>

            <section style={{ marginBottom: 20 }}>
                <h2>Create User</h2>
                <input
                    placeholder="Name"
                    value={userName}
                    onChange={(e) => setUserName(e.target.value)}
                    style={{ padding: 8, width: "100%", marginBottom: 10 }}
                />
                <button onClick={createUser} disabled={!userName}>
                    Create User
                </button>
                {userId && <p>User created with ID: {userId}</p>}
            </section>

            {userId && (
                <section style={{ marginBottom: 20 }}>
                    <h2>Calculate TDEE</h2>
                    <input
                        type="number"
                        placeholder="Weight (kg)"
                        value={entry.weight}
                        onChange={(e) => setEntry({ ...entry, weight: e.target.value })}
                        style={{ padding: 8, width: "100%", marginBottom: 10 }}
                    />
                    <input
                        type="number"
                        placeholder="Height (cm)"
                        value={entry.height}
                        onChange={(e) => setEntry({ ...entry, height: e.target.value })}
                        style={{ padding: 8, width: "100%", marginBottom: 10 }}
                    />
                    <input
                        type="number"
                        placeholder="Age"
                        value={entry.age}
                        onChange={(e) => setEntry({ ...entry, age: e.target.value })}
                        style={{ padding: 8, width: "100%", marginBottom: 10 }}
                    />
                    <select
                        value={entry.sex}
                        onChange={(e) => setEntry({ ...entry, sex: e.target.value })}
                        style={{ padding: 8, width: "100%", marginBottom: 10 }}
                    >
                        <option value="male">Male</option>
                        <option value="female">Female</option>
                    </select>
                    <select
                        value={entry.formulaChoice}
                        onChange={(e) => setEntry({ ...entry, formulaChoice: e.target.value })}
                        style={{ padding: 8, width: "100%", marginBottom: 10 }}
                    >
                        <option value="MifflinStJeorFormula">Mifflin St Jeor</option>
                        <option value="HarrisBenedictFormula">Harris Benedict</option>
                    </select>
                    <select
                        value={entry.activityChoice}
                        onChange={(e) => setEntry({ ...entry, activityChoice: e.target.value })}
                        style={{ padding: 8, width: "100%", marginBottom: 10 }}
                    >
                        <option value="Sedentry">Sedentry</option>
                        <option value="Moderate">Moderate</option>
                        <option value="VeryActive">Very Active</option>
                    </select>
                    <button onClick={calculateTDEE} disabled={!entry.weight || !entry.height || !entry.age}>
                        Calculate TDEE
                    </button>
                    {tdeeResult !== null && <p>Your calculated TDEE: <b>{tdeeResult}</b></p>}
                </section>
            )}

            <section>
                <h2>Get Saved TDEE</h2>
                <input
                    placeholder="Enter User ID"
                    value={manualUserId}
                    onChange={(e) => setManualUserId(e.target.value)}
                    style={{ padding: 8, width: "100%", marginBottom: 10 }}
                />
                <button onClick={() => getSavedTDEE(manualUserId)} disabled={!manualUserId}>
                    Get Saved TDEE by User ID
                </button>
                {savedTDEE !== null && <p>Saved TDEE: <b>{savedTDEE}</b></p>}
            </section>

            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    );
}

export default App;


