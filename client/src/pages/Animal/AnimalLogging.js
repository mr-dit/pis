import { useState, useEffect } from "react";
import Menu from "../../components/Menu/Menu";
import axios from "axios";
import Table from "../../components/Table/Table";
import { useNavigate } from "react-router-dom";
import Select from "react-select";
import { getDataForRequest } from "../../helpers";

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
  { label: "Дата изменения", value: "date" },
  { label: "id животного", value: "idObject" },
  { label: "Описание животного", value: "descObject" },
];

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
        getDataForRequest(),
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

      setJournals(journals);
      setTotalItems(totalItems);
      setTotalPages(totalPages);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchData();
  }, [pageNumber, pageSize]);

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
      setSelectedRows([]);
      await fetchData();
      handleRowSelection([]);
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
        <div className="filter d-flex justify-content-between mb-4 mt-3">
          <div className="d-flex align-items-center">
            <Select
              isClearable
              isSearchable
              placeholder="Поле фильтра..."
              options={filterOptions}
              onChange={(val) => setFilterField(val?.value)}
            />
            <div className="input-group ms-2">
              <input
                type="text"
                className="form-control"
                placeholder="Значение фильтра..."
                value={filterValue}
                onChange={(e) => setFilterValue(e.target.value)}
                aria-label="Recipient's username"
                aria-describedby="button-addon2"
              />
              <button
                className="btn btn-outline-secondary"
                type="button"
                id="button-addon2"
                onClick={fetchData}
              >
                Поиск
              </button>
            </div>
          </div>
          <button className="btn btn-danger" onClick={handleDelete}>
            Удалить выбранные записи
          </button>
        </div>

        <Table
          data={journals}
          headers={cols}
          isDelete
          sortField={""}
          selRow={selectedRows}
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
