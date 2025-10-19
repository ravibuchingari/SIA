import React from 'react';
import { useEffect } from 'react';
import { toast } from 'react-toastify';
import { postAsync } from '../../../services/apiService';
import { CONTROLLER_HOME } from '../../../services/constants';

const MicrosoftCallback = () => {
     useEffect(() => {
        const urlParams = new URLSearchParams(window.location.search);
        const code = urlParams.get('code');
         if(code != null)
            authenticate(code);
        else 
            toast.error("No code found in the URL.");
    }, []);

     const authenticate = async (code) => {
           try {
                    await postAsync(CONTROLLER_HOME, `microsoft/authentication?code=${code}&codeVerifier=${sessionStorage.getItem("codeVerifier")}`, "").then((response) => {
                    console.log(response); 
                }).catch((error) => {
                    toast.error("There was an error! " + (error.response?.data || error.message))
                });
            } catch (error) {
                toast.error(`Error sending code to backend: ${error}`);
            }
        }
    
    return (
        <div style={{textAlign: 'center', marginTop: '100px'}}>
            Microsoft Callback
        </div>
    )
}

export default MicrosoftCallback;