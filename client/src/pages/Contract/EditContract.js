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

const EditContractsForm = () => {
  const [сontractsData, setContractsData] = useState({
    orgName: "",
    inn: "",
    kpp: "",
    adressReg: "",
    orgTypeId: "",
    localityId: "",
  });
  const [organisationTypeOptions, setOrganisationTypeOptions] = useState([]);
  const [localityOptions, setLocalityOptions] = useState([]);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchOrganisationById = async () => {
    try {
      const res = await axios.get(`${REACT_APP_API_URL}/Contract/${id}`);
      setContractsData({ ...res.data });
    } catch (e) {
      console.error(e);
    }
  };

  const fetchData = async () => {
    try {
      const fetchOrganisationTypeRes = await axios.get(
        `${REACT_APP_API_URL}/OrgType/opensRegister`
      );
      setOrganisationTypeOptions(
        createArrayOptions(fetchOrganisationTypeRes.data.users)
      );

      const localityRes = await axios.get(
        `${REACT_APP_API_URL}/Locality/opensRegister`
      );
      setLocalityOptions(createArrayOptions(localityRes.data.localities));

      if (id) {
        fetchOrganisationById(id);
      }
    } catch (e) {
      console.error(e);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleChange = (value, key) => {
    setContractsData((prev) => ({ ...prev, [key]: value }));
  };

  const handleOrganisationCategory = (value) => {
    setContractsData((prev) => ({ ...prev, orgTypeId: value }));
  };

  const handleLocality = (value) => {
    setContractsData((prev) => ({ ...prev, localityId: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (id) {
        await axios.post(
          `${REACT_APP_API_URL}/Contract/ChangeEntry/${id}`,
          сontractsData
        );
      } else {
        await axios.post(
          `${REACT_APP_API_URL}/Contract/AddEntry`,
          сontractsData
        );
      }
      toMainPage();
    } catch (e) {
      console.error(e);
    }
  };

  const toMainPage = () => {
    navigate("/Contract");
  };

  return (
    <>
      <div className="d-flex justify-content-between">
        {id ? (
          <h1>Редактирование Контракта</h1>
        ) : (
          <h1>Добавление контракта</h1>
        )}
        <button className="fs-1" onClick={toMainPage}>
          ×
        </button>
      </div>
      <form onSubmit={handleSubmit}>
        <div className="input-group mb-4 flex-nowrap gap-3">
          <label>
            Тип организации
            <MySelect
              newOptions={organisationTypeOptions}
              handleChange={handleOrganisationCategory}
              newValue={сontractsData.orgTypeId}
              labelField={"nameOrgType"}
              valueField={"idOrgType"}
              apiRoute={"OrgType"}
            />
          </label>

          <label>
            Город
            <MySelect
              newOptions={localityOptions}
              handleChange={handleLocality}
              newValue={сontractsData.localityId}
              labelField={"nameLocality"}
              valueField={"idLocality"}
              apiRoute={"Locality"}
            />
          </label>

          <label>
            Название организации
            <input
              className="form-control"
              type="text"
              value={сontractsData.orgName}
              onChange={(e) => handleChange(e.target.value, "orgName")}
              required
            />
          </label>
        </div>

        <div className="input-group mb-4 flex-nowrap gap-3">
          <label>
            ИНН
            <input
              className="form-control"
              type="text"
              value={сontractsData.inn}
              onChange={(e) => handleChange(e.target.value, "inn")}
              required
              maxLength={13}
            />
          </label>

          <label>
            КПП
            <input
              className="form-control"
              type="text"
              value={сontractsData.kpp}
              onChange={(e) => handleChange(e.target.value, "kpp")}
              required
              maxLength={15}
            />
          </label>
          <label>
            Адрес регистрации
            <input
              className="form-control"
              type="text"
              value={сontractsData.adressReg}
              onChange={(e) => handleChange(e.target.value, "adressReg")}
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

export default EditOrganisationForm;
