import React from "react";
import { NavLink } from "react-router-dom";
import { clearLocalStorage } from "../../helpers";


const Menu = () => {
  return (
    <nav
      className="navbar navbar-expand-lg"
      style={{ "backgroundColor": "#e3f2fd" }}
    >
      <div className="container-fluid">
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            <NavLink className={'nav-link'} to="/Animal">
              <li className="nav-item">
                  Животные
              </li>
            </NavLink>
            <NavLink className={'nav-link'} to="/Organisation">
              <li className="nav-item">
                  Организации
              </li>
            </NavLink>
            <NavLink className={'nav-link'} to="/Contract">
              <li className="nav-item">
                  Контракты
              </li>
            </NavLink>

          </ul>
        </div>
      </div>
            <button onClick={clearLocalStorage}>Выйти</button>
    </nav>
  );
};

export default Menu;
