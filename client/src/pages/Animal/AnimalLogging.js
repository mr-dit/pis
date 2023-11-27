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
// 1. ФИО
// 2. Имя организации
// 3. Логин пользователя
// 4. Дата изменения
// 5. ID Измененного объекта
// 6. Описание измененного объекта

const AnimalLogging = () => {
  const navigate = useNavigate();
  const toMainPage = () => {
    navigate("/Animal");
  };
  
  return (
    <>
      <div>
        <Menu />
        <div className="d-flex justify-content-end">   
          <button className="fs-1" onClick={toMainPage}>
          ×
        </button>
        </div>
        <Table data={[]} headers={cols} />
      </div>
    </>
  );
};

export default AnimalLogging;
