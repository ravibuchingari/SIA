import React from 'react';
import { GoogleLogin, useGoogleLogin } from '@react-oauth/google';
import googleLogo from "../../assets/google.svg";
import { toast } from 'react-toastify';
import { postAsync } from '../../services/apiService';
import { CONTROLLER_HOME } from '../../services/constants';

const GoogleAuthentication = () => {

   const socialMediaIcon = {
        padding: "12px",
        border: "solid 1px #ddd",
        borderRadius: "16px",
        cursor: "pointer",
        width: "48px",
        height: "48px",
        objectFit: "contain",
        margin: "4px",
    };

  const login = useGoogleLogin({
    scope: 'openid profile email',
    onSuccess: async codeResponse => {
      try {
        console.log(JSON.stringify(codeResponse))
        await postAsync(CONTROLLER_HOME, "signin/google/validation", JSON.stringify(codeResponse)).then((response) => {
          console.log('User info from backend:', response); 
        }).catch((error) => {
            console.log(error);
            toast.error("There was an error! " + (error.response?.data?.message || error.message))
        });
      } catch (error) {
        toast.error(`Error sending code to backend: ${error}`);
      }
    },
    onError: error => {
      toast.error(`Google login failed: ${error}`);
    },
  });

  return (
    <img
        src={googleLogo}
        style={socialMediaIcon}
        onClick={() => login()}
    ></img>
  );
};

export default GoogleAuthentication;