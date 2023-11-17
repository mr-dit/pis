import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "../../components/Table/Table";
import Menu from "../../components/Menu/Menu";
import Select from "react-select";
import { useNavigate } from "react-router-dom";

const { REACT_APP_API_URL } = process.env;

const cols = [
  {
    name: "orgId",
    title: "id",
    nonVisible: true,
  },
  { name: "orgName", title: "Название организации", sortName: "OrgName" },
  {
    name: "inn",
    title: "ИНН",
    sortName: "Inn",
  },
  { name: "kpp", title: "КПП", sortName: "Kpp" },
  { name: "adressReg", title: "Адрес регистрации", sortName: "AdressReg" },
  {
    name: "orgType",
    title: "Тип организации",
    sortName: "OrgType",
  },
  { name: "locality", title: "Город", sortName: "Locality" },
];
const filterOptions = [
  { label: "Название организации", value: "OrgName" },
  { label: "ИНН", value: "Inn" },
  { label: "КПП", value: "Kpp" },
  { label: "Адрес регистрации", value: "AdressReg" },
  { label: "Город", value: "Locality" },
];

const OrganisationPage = () => {
  const [organisations, setOrganisations] = useState([]);
  const [filterValue, setFilterValue] = useState("");
  const [sortBy, setSortBy] = useState("");
  const [isAscending, setIsAscending] = useState(true);
  const [filterField, setFilterField] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalItems, setTotalItems] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, [sortBy, isAscending, filterField, pageNumber, pageSize]);

  const fetchData = async () => {
    try {
      const response = await axios.get(
        `${REACT_APP_API_URL}/Organisation/OpensRegister`,
        {
          params: {
            filterValue,
            sortBy,
            isAscending,
            filterField,
            pageNumber,
            pageSize,
          },
        }
      );

      const { organisations, totalItems, totalPages } = response.data;
      const newOrganisations = organisations.map((i) => ({
        ...i,
        orgType: i.orgType.nameOrgType,
        locality: i.locality.nameLocality,
      }));

      setOrganisations(newOrganisations);
      setTotalItems(totalItems);
      setTotalPages(totalPages);
    } catch (error) {
      console.error(error);
    }
  };

  const handleChange = (id) => {
    navigate(`/Organisation/update/${id}`);
  };
  const handleCreate = () => {
    navigate(`/Organisation/update`);
  };

  const handleDelete = async (id) => {
    try {
      await axios.post(`${REACT_APP_API_URL}/Organisation/DeleteEntry/${id}`);
      setOrganisations((prev) => prev.filter((n) => n.orgId !== id));
    } catch (e) {
      alert(e);
    }
  };

  return (
    <div>
      <Menu />
      <div className="filter d-flex justify-content-between mb-1 mt-3">
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

        <button className="btn btn-primary btn-lg" onClick={handleCreate}>
          Создать
        </button>
      </div>
      <Table
        data={organisations}
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

export default OrganisationPage;
