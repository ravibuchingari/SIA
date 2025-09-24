import React from "react";
import { Navigate } from "react-router-dom";
import useAuth from "../services/useAuth"

const CheckAuth = ({ children }) => {
    const { isAuthenticated, loading } = useAuth();
    if (loading) { 
        return <div>Loading...</div>;
    }   
    return isAuthenticated ? children : <Navigate to="/signin" replace />;
}

export default CheckAuth;