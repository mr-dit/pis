import React from "react";
import { NavLink } from "react-router-dom";

const Menu = () => {
  return (
    <nav
      className="navbar navbar-expand-lg"
      style={{ "background-color": "#e3f2fd" }}
    >
      <div className="container-fluid">
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            {/* <li className="nav-item">
              <a className="nav-link active" aria-current="page" href="#">Home</a>
              Животные
            </li> */}
            <NavLink className={'nav-link'} to="/Animal">
              <li className="nav-item">
                {/* <a className="nav-link" href="#"> */}
                  Животные
                {/* </a> */}
              </li>
            </NavLink>

            <li className="nav-item">
              <a className="nav-link" href="#">
                Pricing
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link disabled" aria-disabled="true">
                Disabled
              </a>
            </li>
          </ul>
        </div>
      </div>

      {/* <NavLink to="/Animal">
        Животные
      </NavLink>
      <NavLink to="/Organisaton">
        Организации
      </NavLink> */}
    </nav>
  );
};

export default Menu;
