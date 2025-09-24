import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Header = () => {
  const navigate = useNavigate();
  const [isNavOpen, setIsNavOpen] = useState(false);

  const toggleNav = () => {
    setIsNavOpen(!isNavOpen);
  };

  const handleSignIn = () => {
    navigate('/signin');
  }

  const handleSignUp = () => {
    navigate('/signup');
  }

  return (
    <header>
      <nav className="navbar navbar-expand-lg navbar-light navbar-custom">
        <div className="container">
          {/* Brand */}
          <a className="navbar-brand fw-bold" href="#">
            SIA
          </a>

          {/* Mobile toggle button */}
          <button
            className="navbar-toggler"
            type="button"
            onClick={toggleNav}
            aria-controls="navbarNav"
            aria-expanded={isNavOpen}
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>

          {/* Navigation items */}
          <div className={`collapse navbar-collapse ${isNavOpen ? 'show' : ''}`} id="navbarNav">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              <li className="nav-item">
                <a className="nav-link active" aria-current="page" href="#">
                  Home
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#">
                  About
                </a>
              </li>
              <li className="nav-item dropdown">
                <a
                  className="nav-link dropdown-toggle"
                  href="#"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Services
                </a>
                <ul className="dropdown-menu">
                  <li><a className="dropdown-item" href="#">Web Development</a></li>
                  <li><a className="dropdown-item" href="#">Mobile Apps</a></li>
                  <li><hr className="dropdown-divider" /></li>
                  <li><a className="dropdown-item" href="#">Consulting</a></li>
                </ul>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#">
                  Contact
                </a>
              </li>
            </ul>

            {/* Right side items */}
            <div className="d-flex">
              <div className="btn-group" role="group">
                <button type="button" className="btn btn-light me-2" onClick={handleSignIn}>Sign In</button>
                <button type="button" className="btn btn-light" onClick={handleSignUp}>Sign Up</button>
              </div>
            </div>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default Header;