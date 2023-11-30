import React, { useEffect, useState } from "react";
import axios from "axios";
import MySelect from "../../components/MySelect/MySelect.tsx";
import { useParams, useNavigate } from "react-router-dom";
import Select from "react-select";
import Vaccine from "./Vaccine.js";
import { isRoleEdit } from "../../helpers.js";
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
//ааааажопаgjgjgjgjg
const EditAnimalForm = () => {
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
  const [vaccinations, setVaccines] = useState([]);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchAnimalById = async () => {
    try {
      const res = await axios.get(`${REACT_APP_API_URL}/Animal/${id}`);
      setAnimalData({ ...res.data, photoPath: "" });
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

      const vaccinesRes = await axios.get(
        `${REACT_APP_API_URL}/Vaccination/getVaccinationsByAnimal/${id}`
      );

      setVaccines(
        vaccinesRes.data.vaccinations.map((item) => ({
          idVactination: item.idVactination,
          vaccinationDate: item.vaccinationDate,
          vaccinationValidDate: item.vaccinationValidDate,
          vaccineSeriesNumber: item.vaccineSeriesNumber,
          vaccine: item.vaccine.nameVaccine,
          contractId: item.contractId,
        }))
      );

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
    setAnimalData((prev) => ({ ...prev, genderId: value.value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (id) {
        await axios.post(
          `${REACT_APP_API_URL}/Animal/ChangeEntry/${id}`,
          animalData
        );
      } else {
        await axios.post(`${REACT_APP_API_URL}/Animal/AddEntry`, animalData);
      }
      toMainPage();
    } catch (e) {
      console.error(e);
    }
  };

  const toMainPage = () => {
    navigate("/Animal");
  };

  const isNotEdit = !isRoleEdit([12, 13, 14, 15]);

  return (
    <>
      <div className="d-flex justify-content-between">
        {id ? <h1>Редактирование животного</h1> : <h1>Добавление животного</h1>}
        <button className="fs-1" onClick={toMainPage}>
          ×
        </button>
      </div>
      <form onSubmit={handleSubmit}>
        <div className="input-group mb-4 flex-nowrap gap-3">
          <label id="my-label">
            Категория животного
            <MySelect
              newOptions={animalCategoryOptions}
              handleChange={handleAnimalCategory}
              newValue={animalData.animalCategoryId}
              labelField={"nameAnimalCategory"}
              valueField={"idAnimalCategory"}
              apiRoute={"AnimalCategory"}
              addEntryRoute={"nameAnimalCategory"}
              disabled={isNotEdit}
            />
          </label>

          <label id="my-label">
            Город
            <MySelect
              newOptions={localityOptions}
              handleChange={handleLocality}
              newValue={animalData.localityId}
              labelField={"nameLocality"}
              valueField={"idLocality"}
              apiRoute={"Locality"}
              addEntryRoute={"localityName"}
              disabled={isNotEdit}
            />
          </label>

          <label id="my-label">
            Пол
            <MySelect
              newOptions={genderOptions}
              handleChange={handleGender}
              newValue={animalData.genderId}
              labelField={"nameGender"}
              valueField={"idGender"}
              apiRoute={"Gender"}
            />
            {/* {console.log(genderOptions)}
            <Select
              isClearable
              isSearchable
              placeholder="Выберите"
              value={`${animalData.genderId}`}
              options={genderOptions}
              onChange={(val) => handleGender(val)}
              isDisabled={isNotEdit}
            /> */}
          </label>

          <label id="my-label">
            Год рождения
            <input
              className="form-control"
              type="text"
              value={animalData.yearOfBirth}
              onChange={(e) => handleChange(e.target.value, "yearOfBirth")}
              disabled={isNotEdit}
              required
              maxLength={4}
            />
          </label>
        </div>

        <div className="input-group mb-4 flex-nowrap gap-3">
          <label id="my-label">
            Номер электронного чипа
            <input
              className="form-control"
              type="text"
              value={animalData.electronicChipNumber}
              onChange={(e) =>
                handleChange(e.target.value, "electronicChipNumber")
              }
              disabled={isNotEdit}
              required
            />
          </label>
          <div className="input-group mb-3">
            <label style={{ width: "350px" }}>
              Изображение
              <input
                type="file"
                className="form-control"
                id="inputGroupFile01"
                disabled={isNotEdit}
              />
            </label>
          </div>
          <label id="my-label">
            Особые приметы
            <input
              className="form-control"
              type="text"
              value={animalData.specialSigns || ""}
              onChange={(e) => handleChange(e.target.value, "specialSigns")}
              disabled={isNotEdit}
            />
          </label>
          <label id="my-label">
            Кличка животного
            <input
              className="form-control"
              type="text"
              value={animalData.animalName}
              onChange={(e) => handleChange(e.target.value, "animalName")}
              disabled={isNotEdit}
              required
            />
          </label>
        </div>
        {!isNotEdit && (
          <div className="d-flex justify-content-end mt-5 mb-5">
            <button className="btn btn-primary btn-lg" type="submit">
              Сохранить карточку
            </button>
          </div>
        )}
      </form>
      <Vaccine vaccinations={vaccinations} />
    </>
  );
};

export default EditAnimalForm;
