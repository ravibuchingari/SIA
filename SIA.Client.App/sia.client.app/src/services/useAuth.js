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

    return { isAuthenticated, loading, logout };
};

export default useAuth;