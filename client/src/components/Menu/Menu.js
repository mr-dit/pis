import React from "react";
import { NavLink } from "react-router-dom";

const Menu = () => {
  return (
    <div>
      <NavLink to="/Animal">
        Животные
      </NavLink>
      <NavLink to="/Organisaton">
        Организации
      </NavLink>
    </div>
  );
};

export default Menu;
