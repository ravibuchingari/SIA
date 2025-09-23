import { useState } from 'react'
import { BrowserRouter, Routes } from 'react-router-dom'
import './App.css'
import SignIn from './components/SignIn'
import UserDashboard from './components/UserDashboard'
import AdminDashboard from './components/AdminDashboard'
import CheckAuth from './components/CheckAuth'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<SignIn />} />
        <Route path='/' element={<CheckAuth><UserDashboard /></CheckAuth>} />
        <Route path='/' element={<CheckAuth><AdminDashboard /></CheckAuth>} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
