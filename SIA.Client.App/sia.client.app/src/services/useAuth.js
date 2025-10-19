import {useState, useEffect, use} from 'react';

const useAuth = () => { 

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => { 
        const accessToken = localStorage.getItem('accessToken');
        if (accessToken) { 
            setIsAuthenticated(true);
        } else { 
            setIsAuthenticated(false);
        }
        setLoading(false);
    }, []);

    const logout = () => { 
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        setIsAuthenticated(false);
        window.location.href = '/login';
    }

    const authenticate = (value) => {
        setIsAuthenticated(value);
    }

    return { isAuthenticated, loading, logout, authenticate};
};

export default useAuth;