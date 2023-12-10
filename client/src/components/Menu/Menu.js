import React, { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";
import {
  clearLocalStorage,
  isRoleEdit,
  getDataForRequest,
} from "../../helpers";

const Menu = () => {
  const isOrgRead = isRoleEdit([1, 2, 3, 6, 7, 8, 9, 11, 4, 10, 15]);
  const isContrRead = isRoleEdit([1, 4, 6, 3, 2, 8, 7, 9, 11, 10, 15]);
  const notification = isRoleEdit([9,10,11,15]);

  return (
    <nav
      className="navbar navbar-expand-lg"
      style={{ backgroundColor: "#e3f2fd" }}
    >
      <div className="container-fluid">
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            <NavLink className={"nav-link"} to="/Animal">
              <li className="nav-item">Животные</li>
            </NavLink>
            {isOrgRead && (
              <NavLink className={"nav-link"} to="/Organisation">
                <li className="nav-item">Организации</li>
              </NavLink>
            )}
            {isContrRead && (
              <NavLink className={"nav-link"} to="/Contract">
                <li className="nav-item">Контракты</li>
              </NavLink>
            )}
            {isContrRead && (
              <NavLink className={"nav-link"} to="/Reports">
                <li className="nav-item">Отчеты</li>
              </NavLink>
            )}
            {notification && 
            (
            <div className={"nav-link"} to="/Reports">
              <li className="nav-item">Отчеты на доработке {countDorabotka}</li>
            </div>
            )}
          </ul>
        </div>
      </div>
      {/* <button className="btn btn-success me-4" onClick={showModal}>
        Отчет
      </button> */}
      {getDataForRequest().firstName} {getDataForRequest().surname}
      <button
        style={{ fontSize: "30px" }}
        className="pe-4"
        onClick={clearLocalStorage}
      >
        <NavLink className={"nav-link"} to="/Auth">
          🚪
        </NavLink>
      </button>
    </nav>
  );
};

export default Menu;
