import React, { useEffect, useState } from "react";
import axios from "axios";
import MySelect from "../../components/MySelect/MySelect.tsx";
import { useParams, useNavigate } from "react-router-dom";
import { getUserId, isRoleEdit } from "../../helpers";
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

const EditOrganisationForm = () => {
  const [organisationData, setOrganisationData] = useState({
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
      const res = await axios.get(`${REACT_APP_API_URL}/Organisation/${id}`);
      setOrganisationData({ ...res.data });
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
    setOrganisationData((prev) => ({ ...prev, [key]: value }));
  };

  const handleOrganisationCategory = (value) => {
    setOrganisationData((prev) => ({ ...prev, orgTypeId: value }));
  };

  const handleLocality = (value) => {
    setOrganisationData((prev) => ({ ...prev, localityId: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (id) {
        await axios.post(
          `${REACT_APP_API_URL}/Organisation/ChangeEntry/${id}?userId=${getUserId()}`,
          organisationData
        );
      } else {
        await axios.post(
          `${REACT_APP_API_URL}/Organisation/AddEntry?userId=${getUserId()}`,
          organisationData
        );
      }
      toMainPage();
    } catch (e) {
      console.error(e);
    }
  };

  const toMainPage = () => {
    navigate("/Organisation");
  };

  const isNotEdit = !isRoleEdit([4, 10, 15]);

  return (
    <>
      <div className="d-flex justify-content-between">
        {id ? (
          <h1>Редактирование организации</h1>
        ) : (
          <h1>Добавление организации</h1>
        )}
        <button className="fs-1" onClick={toMainPage}>
          ×
        </button>
      </div>
      <form onSubmit={handleSubmit}>
        <div className="input-group mb-4 flex-nowrap gap-3">
          <label id="my-label">
            Тип организации
            <MySelect
              newOptions={organisationTypeOptions}
              handleChange={handleOrganisationCategory}
              newValue={organisationData.orgTypeId}
              labelField={"nameOrgType"}
              valueField={"idOrgType"}
              apiRoute={"OrgType"}
              disabled={isNotEdit}
            />
          </label>

          <label id="my-label">
            Город
            <MySelect
              newOptions={localityOptions}
              handleChange={handleLocality}
              newValue={organisationData.localityId}
              labelField={"nameLocality"}
              valueField={"idLocality"}
              apiRoute={"Locality"}
              disabled={isNotEdit}
            />
          </label>

          <label id="my-label">
            Название организации
            <input
              className="form-control"
              type="text"
              value={organisationData.orgName}
              onChange={(e) => handleChange(e.target.value, "orgName")}
              disabled={isNotEdit}
              required
            />
          </label>
        </div>

        <div className="input-group mb-4 flex-nowrap gap-3">
          <label id="my-label">
            ИНН
            <input
              className="form-control"
              type="text"
              value={organisationData.inn}
              onChange={(e) => handleChange(e.target.value, "inn")}
              disabled={isNotEdit}
              required
              maxLength={13}
            />
          </label>

          <label id="my-label">
            КПП
            <input
              className="form-control"
              type="text"
              value={organisationData.kpp}
              onChange={(e) => handleChange(e.target.value, "kpp")}
              disabled={isNotEdit}
              required
              maxLength={15}
            />
          </label>
          <label id="my-label">
            Адрес регистрации
            <input
              className="form-control"
              type="text"
              value={organisationData.adressReg}
              onChange={(e) => handleChange(e.target.value, "adressReg")}
              disabled={isNotEdit}
              required
            />
          </label>
        </div>
        {!isNotEdit && (
          <div className="d-flex justify-content-end">
            <button className="btn btn-primary btn-lg" type="submit">
              Сохранить
            </button>
          </div>
        )}
      </form>
    </>
  );
};

export default EditOrganisationForm;
