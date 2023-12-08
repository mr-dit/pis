import { useState, useEffect } from "react";
import Menu from "../../components/Menu/Menu";
import axios from "axios";
import Table from "../../components/Table/Table";
import { useNavigate } from "react-router-dom";

const { REACT_APP_API_URL } = process.env;

const cols = [
  { name: "id", title: "ID", nonVisible: true },

  { name: "fio", title: "ФИО" },

  { name: "orgName", title: "Организация" },

  { name: "userLogin", title: "Логин" },

  { name: "date", title: "Дата изменения" },

  { name: "idObject", title: "id животного" },

  { name: "descObject", title: "Описание животного" },
  { name: "actionType", title: "Совершенное действие" },
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
  const [journals, setJournals] = useState([]);
  const [filterValue, setFilterValue] = useState("");
  const [filterField, setFilterField] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalItems, setTotalItems] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  const fetchData = async () => {
    try {
      const response = await axios.get(
        `${REACT_APP_API_URL}/JournalAnimalContolller/openJournal`,
        {
          params: {
            filterValue,
            filterField,
            pageNumber,
            pageSize,
          },
        }
      );

      const { journals, totalItems, totalPages } = response.data;
      // const newOrganisations = organisations.map((i) => ({
      //   ...i,
      //   orgType: i.orgType.nameOrgType,
      //   locality: i.locality.nameLocality,
      // }));

      setJournals(journals);
      setTotalItems(totalItems);
      setTotalPages(totalPages);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchData();
  }, [filterField, pageNumber, pageSize]);

  const [selectedRows, setSelectedRows] = useState([]);
  const handleRowSelection = (selectedRows) => {
    setSelectedRows(selectedRows);
  };

  const handleDelete = async () => {
    try {
      await axios.post(
        `${REACT_APP_API_URL}/JournalAnimalContolller/deleteJournals`,
        selectedRows
      );

        await fetchData()
    } catch (error) {
      console.error(error);
    }
  };

  const navigate = useNavigate();
  const toMainPage = () => {
    navigate("/Animal");
  };

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
        <button onClick={handleDelete}>Удалить записи</button>
        <Table
          data={journals}
          headers={cols}
          isDelete
          sortField={""}
          onRowSelect={handleRowSelection}
        />
      </div>
      {/* Pagination */}
      {totalPages > 1 && (
        <div>
          <button
            disabled={pageNumber === 1}
            onClick={() => setPageNumber(pageNumber - 1)}
          >
            ←
          </button>
          <span>
            Страница {pageNumber} из {totalPages}
          </span>
          <button
            disabled={pageNumber === totalPages}
            onClick={() => setPageNumber(pageNumber + 1)}
          >
            →
          </button>
        </div>
      )}
    </>
  );
};

export default AnimalLogging;
