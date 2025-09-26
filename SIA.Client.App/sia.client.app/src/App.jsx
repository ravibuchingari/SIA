import { useEffect, useState } from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import SignIn from './components/SignIn'
import UserDashboard from './components/user/UserDashboard'
import AdminDashboard from './components/admin/AdminDashboard'
import Header from './components/Header';
import Footer from './components/Footer';
import SignUp from './components/SignUp';
import CheckAuth from './components/checkAuth';
import LoadingSpinner from './components/LoadingSpinner';
import OAuthSuccess from './components/user/OAuthSuccess';


function App() {
  //const apiUrl = import.meta.env.VITE_API_URL;
  //const appName = import.meta.env.VITE_APP_NAME;
  //console.log(`API URL: ${apiUrl}, App Name: ${appName}`);

  return (
    <div>
        <BrowserRouter>
          <Header></Header>
          <Routes>
            <Route path='/signin' element={<SignIn />} />
            <Route path='/user/signin' element={<SignIn />} />
            <Route path="/signup" element={<SignUp />} />
            <Route path='/user/oauth/success' element={<OAuthSuccess />} />
            <Route path='/user/dashboard' element={<CheckAuth><UserDashboard /></CheckAuth>} />
            <Route path='/admin/dashboard' element={<CheckAuth><AdminDashboard /></CheckAuth>} />
          </Routes>
          <ToastContainer position="top-right" autoClose={5000} hideProgressBar={false} newestOnTop={false} closeOnClick rtl={false} pauseOnFocusLoss draggable pauseOnHover theme="light" /> 
       </BrowserRouter>
    </div>
  )
}

export default App
