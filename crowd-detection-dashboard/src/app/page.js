"use client"
// pages/dashboard.js

import { useEffect, useState } from "react";
import { ref, onValue } from "firebase/database";
import { database } from "./lib/firebase.js"

export default function Dashboard() {
    const [peopleCount, setPeopleCount] = useState("Loading...");
    const [crowdDensity, setCrowdDensity] = useState("Loading...");
    const [timestamp, setTimestamp] = useState("Loading...");

    useEffect(() => {
        const camera1Ref = ref(database, 'cameras/camera1');

        const unsubscribe = onValue(camera1Ref, (snapshot) => {
            const data = snapshot.val();
            if (data) {
                setPeopleCount(data.people_count || 'No data');
                setCrowdDensity(data.crowd_density || 'No data');
                setTimestamp(data.timestamp || 'No data');
            } else {
                setPeopleCount('No data');
                setCrowdDensity('No data');
                setTimestamp('No data');
            }
        });

        // Cleanup listener on component unmount
        return () => unsubscribe();
    }, []);

    return (
        <div className="flex items-center justify-center p-10">
            <div id="camera1-status">
            <h1 className="text-3xl 2xl:text-5xl">Crowd Detection Status</h1>
                <h2>Camera 1</h2>
                <p><strong>People Count:</strong> <span>{peopleCount}</span></p>
                <p><strong>Crowd Density:</strong> <span>{crowdDensity}</span></p>
                <p><strong>Last Updated:</strong> <span>{timestamp}</span></p>
            </div>
        </div>
    );
}
