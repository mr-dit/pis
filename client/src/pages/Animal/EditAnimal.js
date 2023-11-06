import React, { useEffect, useState } from "react";
import axios from "axios";
import MySelect from "../../components/MySelect/MySelect.tsx";
import { useParams, useNavigate } from "react-router-dom";
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
  const [genderOptions, setGenderOptions] = useState([]);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchAnimalById = async () => {
    try {
      const res = await axios.get(`${REACT_APP_API_URL}/Animal/${id}`);
      setAnimalData(res.data);
    } catch (e) {
      console.error(e);
    }
  };

  const fetchData = async () => {
    try {
      const animalCategoryRes = await axios.get(
        `${REACT_APP_API_URL}/AnimalCategory/opensRegister`
      );
      setAnimalCategoryOptions(createArrayOptions(animalCategoryRes.data));

      const localityRes = await axios.get(
        `${REACT_APP_API_URL}/Locality/opensRegister`
      );
      setLocalityOptions(createArrayOptions(localityRes.data.localities));

      const genderRes = await axios.get(
        `${REACT_APP_API_URL}/Gender/opensRegister`
      );
      setGenderOptions(createArrayOptions(genderRes.data));

      if (id) {
        fetchAnimalById(id);
      }
    } catch (e) {
      console.error(e);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleChange = (value, key) => {
    setAnimalData((prev) => ({ ...prev, [key]: value }));
  };

  const handleAnimalCategory = (value) => {
    setAnimalData((prev) => ({ ...prev, animalCategoryId: value }));
  };

  const handleLocality = (value) => {
    setAnimalData((prev) => ({ ...prev, localityId: value }));
  };

  const handleGender = (value) => {
    setAnimalData((prev) => ({ ...prev, genderId: value }));
  };

  const filterOption = (input, option) =>
    (option?.label ?? "").toLowerCase().includes(input.toLowerCase());

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (id) {
        await axios.post(
          `${REACT_APP_API_URL}/Animal/ChangeEntry/${id}`,
          animalData
        );
      }
      toMainPage()
    } catch (e) {
      console.error(e);
    }
  };

  const toMainPage = () =>{
    navigate('/Animal')
  }

  return (
    <>
      <div className="d-flex justify-content-between">
        {id ? <h1>Редактирование животного</h1> : <h1>Добавление животного</h1>}
        <button className="fs-1" onClick={toMainPage}>×</button>
      </div>
      <form onSubmit={handleSubmit}>
        <div className="input-group mb-4 flex-nowrap gap-3">
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
              handleChange={handleLocality}
              newValue={animalData.localityId}
              labelField={"nameLocality"}
              valueField={"idLocality"}
              apiRoute={"Locality"}
            />
          </label>

          <label>
            Пол
            <MySelect
              newOptions={genderOptions}
              handleChange={handleGender}
              newValue={animalData.genderId}
              labelField={"nameGender"}
              valueField={"idGender"}
              apiRoute={"Gender"}
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
        </div>

        <div className="input-group mb-4 flex-nowrap gap-3">
          <label>
            Номер электронного чипа
            <input
              className="form-control"
              type="text"
              value={animalData.electronicChipNumber}
              onChange={(e) =>
                handleChange(e.target.value, "electronicChipNumber")
              }
              required
            />
          </label>
          <label>
            Номер электронного чипа
            <input
              className="form-control"
              type="text"
              value={animalData.electronicChipNumber}
              onChange={(e) =>
                handleChange(e.target.value, "electronicChipNumber")
              }
              required
            />
          </label>
          <label>
            Особые приметы
            <input
              className="form-control"
              type="text"
              value={animalData.specialSigns || ""}
              onChange={(e) => handleChange(e.target.value, "specialSigns")}
              required
            />
          </label>
          <label>
            Кличка животного
            <input
              className="form-control"
              type="text"
              value={animalData.animalName}
              onChange={(e) => handleChange(e.target.value, "animalName")}
              required
            />
          </label>
        </div>

        <button className="btn btn-primary btn-lg" type="submit">
          Сохранить
        </button>
      </form>
    </>
  );
};

export default EditAnimalForm;
