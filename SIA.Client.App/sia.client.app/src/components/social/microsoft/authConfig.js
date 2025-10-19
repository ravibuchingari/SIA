import { LogLevel } from "@azure/msal-browser";

export const msalConfig = {
  auth: {
    clientId: "6a069d24-8316-434b-bb5f-d53db6816204",
    authority: "https://login.microsoftonline.com/common", // Supports Hotmail
    redirectUri: "http://localhost:5556/microsoft/callback",
    postLogoutRedirectUri:'/',
    navigateToLoginRequestUrl:false,
  },
  cache: {
    cacheLocation: "sessionStorage",
    storeAuthStateInCookie: false,
  },
  system: {
    loggerOptions: {
      loggerCallback: (level, message, containsPii) => {
        if (containsPii) {
          return;
        }
        switch (level) {
          case LogLevel.Error:
            console.error(message);
            return;
          case LogLevel.Info:
            console.info(message);
            return;
          case LogLevel.Verbose:
            console.debug(message);
            return;
          case LogLevel.Warning:
            console.warn(message);
            return;
        }
      }
    }
  }
};

// export const loginRequest = {
//   scopes: ["user.read"],
// };

// export const protectedApiScope = {
//     scopes: ["api://YOUR_BACKEND_CLIENT_ID_HERE/access_as_user"] 
// };

//export default msalConfig;