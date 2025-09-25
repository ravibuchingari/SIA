import { auth } from "../lib/firebase";
import React, { useContext, useState, useEffect } from "react";
import { onAuthStateChanged } from "firebase/auth";

const AuthContext = React.createContext();

export function AuthProvider({ children }) {
    const [currentUser, setCurrentUser] = useState(null);
    const [loading, setLoading] = useState(true); 
    const [isAdmin, setIsAdmin] = useState(false);
    const [userLoggedIn, setUserLoggedIn] = useState(false);

    useEffect(() => {
        const unsubscribe = auth.onAuthStateChanged(async (user) => {
            await initilizeUser(user);
        });
        return () => unsubscribe();
    }, []);


    async function initilizeUser(user) {
        if (user) {
            setCurrentUser(...user);   
            setUserLoggedIn(true);
            // Check if the user has admin privileges
            // const tokenResult = await user.getIdTokenResult();
            // if (tokenResult.claims.admin) {
            //     setIsAdmin(true);
            // } else {
            //     setIsAdmin(false);
            // }
        } else {
            setCurrentUser(null);
            setIsAdmin(false);
            setUserLoggedIn(false);
        }   
        setLoading(false);
    }

    
    return (
        <AuthContext.Provider value={{ currentUser, isAdmin, userLoggedIn, loading }}>
            {!loading && children}
        </AuthContext.Provider>
    );
}