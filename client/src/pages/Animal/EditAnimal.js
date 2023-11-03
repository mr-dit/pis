import React, { useEffect, useState } from "react";
import axios from "axios";
import MySelect from "../../components/MySelect/MySelect.tsx";
import { useParams } from "react-router-dom";

const { REACT_APP_API_URL } = process.env;

const createArrayOptions = (data) => {
  return data.map((item) => {
    const values = Object.values(item);

    return {
      label: values[1],
      value: `${values[0]}`,
    };
  });
};

const EditAnimalForm = ({ animal, handleUpdate }) => {
  const [animalData, setAnimalData] = useState({
    localityId: "",
    animalCategoryId: "",
    genderId: "",
    yearOfBirth: "",
    electronicChipNumber: "",
    animalName: "",
    photoPath: "",
    specialSigns: "",
  });
  const [animalCategoryOptions, setAnimalCategoryOptions] = useState([]);
  const [localityOptions, setLocalityOptions] = useState([]);
  const { id } = useParams();

  const fetchAnimalById = async () => {
    try {
      const res = await axios.get(`${REACT_APP_API_URL}/Animal/${id}`);
      setAnimalData(res.data);
    } catch (e) {
      console.error(e);
    }
  };

  const fetchAnimalCategory = async () => {
    try {
      const res = await axios.get(
        `${REACT_APP_API_URL}/AnimalCategory/opensRegister`
      );
      setAnimalCategoryOptions(createArrayOptions(res.data));
    } catch (e) {
      console.error(e);
    }
  };

  const fetchLocality = async () => {
    try {
      const res = await axios.get(
        `${REACT_APP_API_URL}/Locality/opensRegister`
      );
      setLocalityOptions(createArrayOptions(res.data));
    } catch (e) {
      console.error(e);
    }
  };

  useEffect(() => {
    if (id) {
      fetchAnimalById(id);
    }
    fetchAnimalCategory();
    fetchLocality();
  }, []);

  const handleChange = (value, key) => {
    setAnimalData((prev) => ({ ...prev, [key]: value }));
  };

  const handleAnimalCategory = (value) => {
    setAnimalData((prev) => ({ ...prev, animalCategoryId: value }));
  };

  const filterOption = (input, option) =>
    (option?.label ?? "").toLowerCase().includes(input.toLowerCase());

  const handleSubmit = (e) => {
    e.preventDefault();
    // handleUpdate(animalData);
    console.log(animalData);
  };

  return (
    <>
      {id ? <h1>Редактирование животного</h1> : <h1>Добавление животного</h1>}
      <form
        className="input-group mb-3 flex-nowrap gap-3"
        onSubmit={handleSubmit}
      >
        <label>
          Категория животного
          <MySelect
            newOptions={animalCategoryOptions}
            handleChange={handleAnimalCategory}
            newValue={animalData.animalCategoryId}
            labelField={"nameAnimalCategory"}
            valueField={"idAnimalCategory"}
            apiRoute={"AnimalCategory"}
          />
        </label>

        <label>
          Город
          <MySelect
            newOptions={localityOptions}
            handleChange={handleAnimalCategory}
            newValue={animalData.animalCategoryId}
            labelField={"nameAnimalCategory"}
            valueField={"idAnimalCategory"}
            apiRoute={"AnimalCategory"}
          />
        </label>

        <label>
          Пол
          <MySelect
            newOptions={animalCategoryOptions}
            handleChange={handleAnimalCategory}
            newValue={animalData.animalCategoryId}
            labelField={"nameAnimalCategory"}
            valueField={"idAnimalCategory"}
            apiRoute={"AnimalCategory"}
          />
        </label>

        <label>
          Год рождения
          <input
            className="form-control"
            type="text"
            value={animalData.yearOfBirth}
            onChange={(e) => handleChange(e.target.value, "yearOfBirth")}
            required
            maxLength={4}
          />
        </label>

        <button type="submit">Update</button>
      </form>
    </>
  );
};

export default EditAnimalForm;
