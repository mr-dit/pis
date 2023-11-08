import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "../../components/Table/Table";
import Menu from "../../components/Menu/Menu";
import MySelect from "../../components/MySelect/MySelect.tsx";
import { useNavigate } from "react-router-dom";

const { REACT_APP_API_URL } = process.env;

const cols = [
  {
    name: "idContract",
    title: "id",
    nonVisible: true,
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

  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, [sortBy, isAscending, filterField, pageNumber, pageSize]);

  const fetchData = async () => {
    try {
      const response = await axios.get(
        `${REACT_APP_API_URL}/Contract/OpensRegister`,
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

  return (
    <div>
      <Menu />
      <div className="filter d-flex justify-content-between mb-1 mt-3">
        <div className="d-flex align-items-center">
          <MySelect
            isCreate={false}
            newPlaceholder="Поле фильтра..."
            newOptions={filterOptions}
            handleChange={(val) => setFilterField(val)}
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
