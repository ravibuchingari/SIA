import axios from "axios";

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    withCredentials: true,
    headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
    },
});

api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem("accessToken");  
        if (token)
            config.headers["Authorization"] = `Bearer ${token}`;
        config.headers["Content-Type"] = "application/json";
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

api.interceptors.response.use(
    (response) => {
        return response;    
    },
     async (error) => {
        const originalRequest = error.config;
        if (error.response && error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;  
            try {
                const refreshResponse = await refreshAccessToken();
                const newAccessToken = refreshResponse.data.accessToken;
                localStorage.setItem("accessToken", newAccessToken);
                originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
                return api(originalRequest);
            } catch (refreshError) {
                return Promise.reject(refreshError);
            }   
        }
        return Promise.reject(error);
    }
);

export const signIn = async (email, password) => { 
    return api.post("/auth/signin", { email, password });
}

export const refreshAccessToken = async () => { 
    return api.post("/auth/refresh-token");
}

export const getAsync = async (controllerName, actionName, query = "") => { 
    return api.get(`/${controllerName}/${actionName}${query ? `?${query}` : ''}`);
}

export const postAsync = async (controllerName, actionName, jsonString) => { 
    return api.post(`/${controllerName}/${actionName}`, jsonString);
}

export default api;