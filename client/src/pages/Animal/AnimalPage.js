import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "../../components/Table/Table";
import Menu from "../../components/Menu/Menu";
import { useNavigate } from "react-router-dom";
import Select from "react-select";
import { getDataForRequest, getUserId, isRoleEdit } from "../../helpers";

const { REACT_APP_API_URL } = process.env;

const cols = [
  {
    name: "registrationNumber",
    title: "Регистрационный номер",
    nonVisible: true,
  },
  { name: "locality", title: "Населенный пункт", sortName: "Locality" },
  {
    name: "animalCategory",
    title: "Категория животного",
    sortName: "AnimalCategory",
  },
  { name: "gender", title: "Пол животного", sortName: "Gender" },
  { name: "yearOfBirth", title: "Год рождения", sortName: "YearOfBirth" },
  {
    name: "electronicChipNumber",
    title: "Номер электронного чипа",
    sortName: "ElectronicChipNumber",
  },
  { name: "animalName", title: "Кличка", sortName: "AnimalName" },
  { name: "photoPath", title: "Фотографии" },
  { name: "specialSigns", title: "Особые приметы" },
  { name: "status", title: "Статус вакцины" },
];

const filterOptions = [
  { label: "Категория животного", value: "AnimalCategory" },
  { label: "Номер электронного чипа", value: "ElectronicChipNumber" },
  { label: "Кличка", value: "AnimalName" },
  { label: "Населенный пункт", value: "Locality" },
];

const vaccinationStatus = {
  1: "Не проводилась",
  2: "Вакцинировано",
  3: "Приближается срок вакцинирования",
};

const AnimalComponent = () => {
  const [animals, setAnimals] = useState([]);
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
      const response = await axios.post(
        `${REACT_APP_API_URL}/Animal/OpensRegister`,
        getDataForRequest(),
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

      const { animals, totalItems, totalPages } = response.data;
      const newAnimals = animals.map((i) => ({
        registrationNumber: i.registrationNumber,
        locality: i.locality.nameLocality,
        animalCategory: i.animalCategory.nameAnimalCategory,
        gender: i.gender.nameGender,
        yearOfBirth: i.yearOfBirth,
        electronicChipNumber: i.electronicChipNumber,
        animalName: i.animalName,
        photoPath: i.photoPath,
        specialSigns: i.specialSigns,
        status: vaccinationStatus[i.status],
      }));

      setAnimals(newAnimals);
      setTotalItems(totalItems);
      setTotalPages(totalPages);
    } catch (error) {
      console.error(error);
    }
  };

  const handleChange = (id) => {
    navigate(`/Animal/update/${id}`);
  };
  const handleCreate = () => {
    navigate(`/Animal/update`);
  };

  const handleLogging = () => {
    navigate(`/Animal/Logging`);
  };

  const handleVaccinLogging = () => {
    navigate(`/Vaccinations/Logging`);
  };

  const handleDelete = async (id) => {
    try {
      await axios.post(
        `${REACT_APP_API_URL}/Animal/DeleteEntry/${id}?userId=${getUserId()}`
      );
      setAnimals((prev) => prev.filter((n) => n.registrationNumber !== id));
    } catch (e) {
      alert(e);
    }
  };

  const isEdit = isRoleEdit([12, 13, 14, 15]);
  const isLogging = isRoleEdit([15]);

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

        {isEdit && (
          <button className="btn btn-primary btn-lg" onClick={handleCreate}>
            Создать
          </button>
        )}
      </div>
      <Table
        isEdit={isEdit}
        data={animals}
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
      {isLogging && (
        <div className="d-flex justify-content-end">
          <button className="btn btn-info btn-lg me-3" onClick={handleVaccinLogging}>
            Все вакцинации
          </button>
          <button className="btn btn-primary btn-lg" onClick={handleLogging}>
            Реестр изменений
          </button>
        </div>
      )}
    </div>
  );
};

export default AnimalComponent;
