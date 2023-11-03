import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "../../components/Table/Table";
import Menu from "../../components/Menu/Menu";
import { useNavigate } from "react-router-dom";

const {REACT_APP_API_URL} = process.env

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
];

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
    fetchAnimals();
  }, [filterValue, sortBy, isAscending, filterField, pageNumber, pageSize]);

  const fetchAnimals = async () => {
    try {
      const response = await axios.get(
        `${REACT_APP_API_URL}/Animal/OpensRegister`,
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

  const handleDelete = async (id) => {
    try {
      const index = animals.findIndex((n) => n.registrationNumber === id);
      await axios.post(`${REACT_APP_API_URL}/Animal/DeleteEntry/${id}`);
      if (index !== -1) {
        animals.splice(index, 1);
      }
    } catch (e) {
      alert(e);
    }
  };

  return (
    <div>
      <Menu />
      <Table
        data={animals}
        headers={cols}
        handleChange={handleChange}
        handleDelete={handleDelete}
        handleSortName={(value) => {
          setSortBy(value);
          setIsAscending((prev) => !prev);
        }}
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

export default AnimalComponent;
