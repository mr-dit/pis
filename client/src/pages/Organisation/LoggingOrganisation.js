import { useState } from "react";
import Menu from "../../components/Menu/Menu";
import Table from "../../components/Table/Table";
import { useNavigate } from "react-router-dom";

const cols = [
  { name: "id", title: "ID", nonVisible: true },

  { name: "fio", title: "ФИО" },

  { name: "orgName", title: "Организация" },

  { name: "userLogin", title: "Логин" },

  { name: "date", title: "Дата" },

  { name: "idObject", title: "Изменение" },

  { name: "descObject", title: "Описание изменения" },
];

const filterOptions = [
  { label: "ФИО", value: "fio" },
  { label: "Организация", value: "orgName" },
  { label: "Логин", value: "userLogin" },
  { label: "Дата", value: "date" },
  { label: "Изменение ", value: "idObject" },
  { label: "Описание изменения", value: "descObject" },
];

const LoggingOrganisation = () => {
  const navigate = useNavigate();
  const toMainPage = () => {
    navigate("/Organisation");
  };

  const [selectedRows, setSelectedRows] = useState([]);
  const handleRowSelection = (selectedRows) => {
    setSelectedRows(selectedRows);
  };

  const handleLog = () => {
    console.log(selectedRows);
  }

  return (
    <>
      <div>
        <Menu />
        <div className="d-flex justify-content-between mt-3 mb-2">
          <h1>Журнал изменений</h1>
          <button className="fs-1" onClick={toMainPage}>
            ×
          </button>
        </div>
        <Table
          isDelete
          sortField={""}
          onRowSelect={handleRowSelection}
          data={[
            {
              id: 1,
              fio: "fio",
              orgName: "orgName",
              userLogin: "userLogin",
              date: "date",
              idObject: "idObject",
              descObject: "descObject",
            },
            {
              id: 2,
              fio: "fio",
              orgName: "orgName",
              userLogin: "userLogin",
              date: "date",
              idObject: "idObject",
              descObject: "descObject",
            },
          ]}
          headers={cols}
        />
      </div>
      <button onClick={handleLog}>ryjgrf</button>
    </>
  );
};

export default LoggingOrganisation;
