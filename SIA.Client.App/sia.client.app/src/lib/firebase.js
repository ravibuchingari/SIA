import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import { getAuth, GoogleAuthProvider } from "firebase/auth";



const firebaseConfig = {
  apiKey: "AIzaSyBQHN67EV13QVOgYEI6QFSf1GtjdxdIYQ4",
  authDomain: "api-project-919229948417.firebaseapp.com",
  databaseURL: "https://api-project-919229948417.firebaseio.com",
  projectId: "api-project-919229948417",
  storageBucket: "api-project-919229948417.firebasestorage.app",
  messagingSenderId: "919229948417",
  appId: "1:919229948417:web:26f43b76093132d48ad600",
  measurementId: "G-V0KM5GVHS4"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const auth = getAuth(app);
//const analytics = getAnalytics(app);

export { app, auth };