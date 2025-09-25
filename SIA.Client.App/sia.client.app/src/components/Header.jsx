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
      <nav className="navbar navbar-expand-lg navbar-light fixed-top navbar-custom">
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
            </ul>
            {/* Right side items */}
            <div className="d-flex">
              <div className="btn-group" role="group">
                <div className='d-flex'>
                  <div className='d-flex align-items-center fw-semibold'>New to SIA?</div>
                  <button type="button" className="btn btn-outline-light theme-font-color fw-semibold" onClick={handleSignUp}>Sign Up</button>
                </div>
                <button type="button" className="btn btn-outline-light theme-font-color fw-semibold me-2" onClick={handleSignIn}>Sign In</button>
              </div>
            </div>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default Header;