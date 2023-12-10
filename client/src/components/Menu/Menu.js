import React, { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";
import {
  clearLocalStorage,
  isRoleEdit,
  getDataForRequest,
} from "../../helpers";
import axios from "axios";
import { Modal, DatePicker } from "antd";
import dayjs from "dayjs";

const { REACT_APP_API_URL } = process.env;

const dateFormat = "MM-DD-YYYY";


const Menu = () => {
  const isOrgRead = isRoleEdit([1, 2, 3, 6, 7, 8, 9, 11, 4, 10, 15]);
  const isContrRead = isRoleEdit([1, 4, 6, 3, 2, 8, 7, 9, 11, 10, 15]);
  const notification = isRoleEdit([9,10,11,15]);

  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");

  const [a, setDorabotka] = useState([]);

  const fetchData = async () => {
    try {
      const response = await axios.get(
        `${REACT_APP_API_URL}/api/Statistica/getCountDorabotka`)
        const a = response.data;
        setDorabotka(a);
    } catch (error) {
      console.error(error);
    }
  };
  useEffect(() => {
    fetchData();
  }, [a]);


  const handleDownload = () => {
    axios({
      url: `${REACT_APP_API_URL}/Statistica/${startDate}/${endDate}`,
      method: "GET",
      responseType: "blob",
    }).then((response) => {
      const url = window.URL.createObjectURL(new Blob([response.data]));
      const link = document.createElement("a");
      link.href = url;
      link.setAttribute("download", "статистика.xlsx");
      document.body.appendChild(link);
      link.click();
    });
  };

  const [isModalOpen, setIsModalOpen] = useState(false);
  const showModal = () => {
    setIsModalOpen(true);
  };
  const handleOk = async () => {
    handleDownload();
    setIsModalOpen(false);
    setEndDate();
    setStartDate();
  };
  const handleCancel = () => {
    setEndDate();
    setStartDate();
    setIsModalOpen(false);
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
            {isContrRead && (
              <NavLink className={"nav-link"} to="/Reports">
                <li className="nav-item">Отчеты</li>
              </NavLink>
            )}
            {notification && 
            (
            <div className={"nav-link"} to="/Reports">
              <li className="nav-item">Отчеты на доработке {a}</li>
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
        🚪
      </button>
      <Modal
        title="Статистика за промежуток"
        open={isModalOpen}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <label id="my-label">
          Дата начала
          <DatePicker
            size="large"
            aria-required
            value={startDate ? dayjs(startDate, dateFormat) : ""}
            format={dateFormat}
            onChange={(dayjs, string) => setStartDate(string)}
          />
        </label>
        <label id="my-label">
          Дата конца
          <DatePicker
            size="large"
            aria-required
            value={endDate ? dayjs(endDate, dateFormat) : ""}
            format={dateFormat}
            onChange={(dayjs, string) => setEndDate(string)}
          />
        </label>
      </Modal>
    </nav>
  );
};

export default Menu;
