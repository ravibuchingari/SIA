import React from 'react';

const Footer = () => {
  return (
    <footer>
      <div className="row">
          <div className="col-lg-4 col-md-6 mb-4">
            <h6 className="text-uppercase mb-3">Company Name</h6>
            <p className="mb-3">
              Building amazing web experiences with modern technologies. 
              We specialize in creating responsive and user-friendly applications.
            </p>
            <div className="d-flex">
              <a href="#" className="text-light me-3" aria-label="Facebook">
                <i className="fab fa-facebook-f"></i>
              </a>
              <a href="#" className="text-light me-3" aria-label="Twitter">
                <i className="fab fa-twitter"></i>
              </a>
              <a href="#" className="text-light me-3" aria-label="LinkedIn">
                <i className="fab fa-linkedin-in"></i>
              </a>
              <a href="#" className="text-light" aria-label="Instagram">
                <i className="fab fa-instagram"></i>
              </a>
            </div>
          </div>

          <div className="col-lg-2 col-md-6 mb-4">
            <h6 className="text-uppercase mb-3">Quick Links</h6>
            <ul className="list-unstyled">
              <li><a href="#" className="text-light text-decoration-none">Home</a></li>
              <li><a href="#" className="text-light text-decoration-none">About</a></li>
              <li><a href="#" className="text-light text-decoration-none">Services</a></li>
              <li><a href="#" className="text-light text-decoration-none">Portfolio</a></li>
              <li><a href="#" className="text-light text-decoration-none">Contact</a></li>
            </ul>
          </div>

          <div className="col-lg-2 col-md-6 mb-4">
            <h6 className="text-uppercase mb-3">Services</h6>
            <ul className="list-unstyled">
              <li><a href="#" className="text-light text-decoration-none">Web Development</a></li>
              <li><a href="#" className="text-light text-decoration-none">Mobile Apps</a></li>
              <li><a href="#" className="text-light text-decoration-none">UI/UX Design</a></li>
              <li><a href="#" className="text-light text-decoration-none">Consulting</a></li>
              <li><a href="#" className="text-light text-decoration-none">Support</a></li>
            </ul>
          </div>

          <div className="col-lg-4 col-md-6 mb-4">
            <h6 className="text-uppercase mb-3">Contact Info</h6>
            <div className="mb-2">
              <i className="fas fa-map-marker-alt me-2"></i>
              <span>123 Business Street, City, State 12345</span>
            </div>
            <div className="mb-2">
              <i className="fas fa-phone me-2"></i>
              <span>+1 (555) 123-4567</span>
            </div>
            <div className="mb-3">
              <i className="fas fa-envelope me-2"></i>
              <span>contact@company.com</span>
            </div>
          </div>
      </div>

        <hr className="my-4" />

        <div className="row align-items-center">
          <div className="col-md-6">
            <p className="mb-0">
              &copy; 2025 Company Name. All rights reserved.
            </p>
          </div>
          <div className="col-md-6 text-md-end">
            <a href="#" className="text-light text-decoration-none me-3">Privacy Policy</a>
            <a href="#" className="text-light text-decoration-none me-3">Terms of Service</a>
            <a href="#" className="text-light text-decoration-none">Sitemap</a>
          </div>
        </div>
    </footer>
  );
};

export default Footer;