import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { GoogleOAuthProvider } from '@react-oauth/google';
import './index.css'
import App from './App.jsx'
import { decryptData, encryptData } from './services/secureData.js';
import { msalConfig } from './components/social/microsoft/authConfig.js';
import { PublicClientApplication } from "@azure/msal-browser";
import { MsalProvider } from "@azure/msal-react";

const msalInstance = new PublicClientApplication(msalConfig);
const clientId = decryptData("U2FsdGVkX1+c+q0DIzdeqfQYDHEDj6y8L3crQblaVHqcJAA1R6opidwn/yErYhGyZ0oL+qfgYUZ8dB+HCRGaukmtgi6Bpy/azFEHkt5rpe5TrYZncfQF15oG6kKGhMwo", import.meta.env.VITE_EKEY);

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <GoogleOAuthProvider clientId={clientId}>
      <MsalProvider instance={msalInstance}>
        <App />
      </MsalProvider>
    </GoogleOAuthProvider>
  </StrictMode>

)
