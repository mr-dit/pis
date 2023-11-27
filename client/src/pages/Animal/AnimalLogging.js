import Table from "../../components/Table/Table";

const cols = [
  {
    name: "fio",
    title: "ФИО",
  },
  { name: "orgName", title: "Организация" },
  {
    name: "userLogin",
    title: "Логин",
  },
  { name: "date", title: "Дата" },
  { name: "idObject", title: "Изменение" },
  {
    name: "descObject",
    title: "Описание изменения",
  },
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
  return (
    <>
      <Table
        data={[]}
        headers={cols}
      />
    </>
  );
};

export default AnimalLogging;
