import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "../../components/Table/Table";
import Menu from "../../components/Menu/Menu";
import Select from "react-select";
import { useNavigate } from "react-router-dom";
import { DatePicker } from "antd";
import { getDataForRequest } from "../../helpers";

const { RangePicker } = DatePicker;

const { REACT_APP_API_URL } = process.env;

const cols = [
  {
    name: "idContract",
    title: "Номер",
  },
  {
    name: "conclusionDate",
    title: "Дата заключения",
    sortName: "ConclusionDate",
  },
  {
    name: "expirationDate",
    title: "Дата действия",
    sortName: "ExpirationDate",
  },
  { name: "performer", title: "Исполнитель", sortName: "Performer" },
  { name: "customer", title: "Заказчик", sortName: "Customer" },
];
const filterOptions = [
  { label: "Исполнитель", value: "Performer" },
  { label: "Заказчик", value: "Customer" },
];

const ContractPage = () => {
  const [contracts, setOrganisations] = useState([]);
  const [filterValue, setFilterValue] = useState("");
  const [sortBy, setSortBy] = useState("");
  const [isAscending, setIsAscending] = useState(true);
  const [filterField, setFilterField] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalItems, setTotalItems] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [startDateFilter, setStartDateFilter] = useState();
  const [endDateFilter, setEndDateFilter] = useState();

  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, [
    startDateFilter,
    endDateFilter,
    sortBy,
    isAscending,
    filterField,
    pageNumber,
    pageSize,
  ]);

  const fetchData = async () => {
    try {
      const response = await axios.post(
        `${REACT_APP_API_URL}/Contract/OpensRegister`,
        getDataForRequest(),
        {
          params: {
            startDateFilter,
            endDateFilter,
            filterValue,
            sortBy,
            isAscending,
            filterField,
            pageNumber,
            pageSize,
          },
        }
      );

      const { contracts, totalItems, totalPages } = response.data;
      const newContracts = contracts.map((i) => ({
        idContract: i.idContract,
        conclusionDate: i.conclusionDate,
        expirationDate: i.expirationDate,
        performer: i.performer.orgName,
        customer: i.customer.orgName,
      }));

      setOrganisations(newContracts);
      setTotalItems(totalItems);
      setTotalPages(totalPages);
    } catch (error) {
      console.error(error);
    }
  };

  const handleChange = (id) => {
    navigate(`/Contract/update/${id}`);
  };
  const handleCreate = () => {
    navigate(`/Contract/update`);
  };

  const handleDelete = async (id) => {
    try {
      await axios.post(`${REACT_APP_API_URL}/Contract/DeleteEntry/${id}`);
      setOrganisations((prev) => prev.filter((n) => n.orgId !== id));
    } catch (e) {
      alert(e);
    }
  };

  const handleDate = (val) => {
    if (val) {
      const startDate = val[0];
      setStartDateFilter(`${startDate.$y}-${startDate.$M + 1}-${startDate.$D}`);
      const endDate = val[1];
      setEndDateFilter(`${endDate.$y}-${endDate.$M + 1}-${endDate.$D}`);
    } else {
      setStartDateFilter();
      setEndDateFilter();
    }
  };

  return (
    <div>
      <Menu />
      <div className="filter d-flex justify-content-between mb-3 mt-3">
        <div className="d-flex align-items-center">
          <div className="mt-4">
            <Select
              isClearable
              isSearchable
              placeholder="Поле фильтра..."
              options={filterOptions}
              onChange={(val) => setFilterField(val?.value)}
            />
          </div>
          <div className="input-group ms-2 mt-4">
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
          <div className="ms-3">
            <label id="my-label">
              Диапазон для даты заключения
              <RangePicker
                size="large"
                placeholder={["Начало", "Конец"]}
                onChange={handleDate}
                showToday
              />
            </label>
          </div>
        </div>

        <button className="btn btn-primary btn-lg" onClick={handleCreate}>
          Создать
        </button>
      </div>
      <Table
        data={contracts}
        headers={cols}
        handleChange={handleChange}
        handleDelete={handleDelete}
        handleSortName={(value) => {
          setSortBy(value);
          setIsAscending((prev) => !prev);
        }}
        sortField={sortBy}
      />

      {/* Pagination */}
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
    </div>
  );
};

export default ContractPage;
