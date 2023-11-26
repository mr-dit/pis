import React from "react";
import { NavLink } from "react-router-dom";
import { clearLocalStorage, isRoleEdit } from "../../helpers";
import axios from "axios";
const { REACT_APP_API_URL } = process.env;


const Menu = () => {
  const isOrgRead = isRoleEdit([1, 2, 3, 6, 7, 8, 9, 11, 4, 10, 15]);
  const isContrRead = isRoleEdit([1, 4, 6, 3, 2, 8, 7, 9, 11, 10, 15]);

  const handleDownload = () => {
    axios({
      url: `${REACT_APP_API_URL}/Statistica/`,
      method: 'GET',
      responseType: 'blob',
    }).then(response => {
      const url = window.URL.createObjectURL(new Blob([response.data]));
      const link = document.createElement('a');
      link.href = url;
      link.setAttribute('download', 'имя_файла.xlsx');
      document.body.appendChild(link);
      link.click();
    });
  };

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
            <li className="nav-item">
              <button className="pe-4" onClick={handleDownload}>
                Скачать файл
              </button>
            </li>
          </ul>
        </div>
      </div>
      <button className="pe-4" onClick={clearLocalStorage}>
        Выйти
      </button>
    </nav>
  );
};

export default Menu;
