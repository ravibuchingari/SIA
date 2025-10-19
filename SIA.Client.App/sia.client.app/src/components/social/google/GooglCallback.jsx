import React from 'react';
import { useEffect } from 'react';
import { toast } from 'react-toastify';
import { postDataAsync, postAsync, getAsync } from '../../../services/apiService';
import { CONTROLLER_HOME } from '../../../services/constants';
import { useNavigate } from 'react-router-dom';

const GoolgeCallback = () => { 
  const navigate = useNavigate();

    useEffect(() => {
        const urlParams = new URLSearchParams(window.location.search);
        const code = urlParams.get('code');
        console.log("Code from URL:", code);
        console.log(sessionStorage.getItem("is_calendar_auth"));
        if(sessionStorage.getItem("is_calendar_auth") == "false"){
            if(code != null)
                authenticate(code);
            else 
                toast.error("No code found in the URL.");
        }
        else 
        {
            const formData = new FormData();
            formData.append("userAccountId", sessionStorage.getItem("userAccountId"));
            formData.append("userAccountGuId", sessionStorage.getItem("userAccountGuId"));
            formData.append("code", code);
            formData.append("securityKey", localStorage.getItem("securityKey"));

            postDataAsync("calendar", "user/account/auth", formData)
            .then((response) => {
                calendarEvents();
            })
            .catch((error) => {
                console.error("Error initiating connection:", error);
            });
        }
     
    }, []);

    const calendarEvents = async () => {
        try {

            await getAsync("calendar", `user/account/events/${sessionStorage.getItem("userAccountId")}/${sessionStorage.getItem("userAccountGuId")}/${localStorage.getItem("securityKey")}`).then((response) => {  
                console.log("Calendar events fetched:", response.data);
            }
            ).catch((error) => {
                toast.error(error.response?.data || error.message);
            });
        } catch (error) {
            toast.error(`Fetching calendar events failed: ${error}`);
        }   
    }

    const authenticate = async (code) => {
       try {
                await postAsync(CONTROLLER_HOME, `google/authentication?code=${code}`, "").then((response) => {
                localStorage.setItem("accessToken", response.data.accessToken);
                localStorage.setItem("refreshToken", response.data.refreshToken);
                localStorage.setItem("securityKey", response.data.securityKey);
                console.log("Authentication successful:", response.data);
                navigate("/google/calendar/integration");
            }).catch((error) => {
                toast.error("There was an error! " + (error.response?.data || error.message))
            });
        } catch (error) {
            toast.error(`Error sending code to backend: ${error}`);
        }
    }

    return (
        <div style={{textAlign: 'center', marginTop: '100px'}}>
            Google Callback
        </div>
    )
}

export default GoolgeCallback;