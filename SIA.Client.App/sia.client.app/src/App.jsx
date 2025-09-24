import { useState } from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import SignIn from './components/SignIn'
import UserDashboard from './components/UserDashboard'
import AdminDashboard from './components/AdminDashboard'
import CheckAuth from './components/CheckAuth'



function App() {
  const apiUrl = import.meta.env.VITE_API_URL;
  const appName = import.meta.env.VITE_APP_NAME;
  console.log(`API URL: ${apiUrl}, App Name: ${appName}`);

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<SignIn />} />
          <Route path='/' element={<CheckAuth><UserDashboard /></CheckAuth>} />
          <Route path='/' element={<CheckAuth><AdminDashboard /></CheckAuth>} />
        </Routes>
      </BrowserRouter>
      <ToastContainer position="top-right" autoClose={5000} hideProgressBar={false} newestOnTop={false} closeOnClick rtl={false} pauseOnFocusLoss draggable pauseOnHover theme="light" />
    </>
  )
}

export default App
