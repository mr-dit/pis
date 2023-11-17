import React, { useEffect, useState } from "react";
import axios from "axios";
import MySelect from "../../components/MySelect/MySelect.tsx";
import { useParams, useNavigate } from "react-router-dom";
import { DatePicker } from "antd";
import PriceList from "./PriceList.js";
const { REACT_APP_API_URL } = process.env;

const createArrayOptions = (data) => {
  return data.map((item) => {
    const values = Object.values(item);

    return {
      value: `${values[0]}`,
      label: values[1],
    };
  });
};

const EditContractsForm = () => {
  const [contractsData, setContractsData] = useState({
    conclusionDate: "",
    expirationDate: "",
    performerId: 0,
    customerId: 0,
    localitiesPriceList: {},
  });
  const [localitiesPriceList, setLocalitiesPriceList] = useState([]);
  const [organisationTypeOptions, setOrganisationTypeOptions] = useState([]);
  const [localityOptions, setLocalityOptions] = useState([]);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchData = async () => {
    try {
      if (id) {
        const res = await axios.get(`${REACT_APP_API_URL}/Contract/${id}`);
        setContractsData({ ...res.data });
        setLocalitiesPriceList(res.data.localities);
      }

      const fetchOrganisationRes = await axios.get(
        `${REACT_APP_API_URL}/Organisation/opensRegister`
      );
      setOrganisationTypeOptions(
        createArrayOptions(
          fetchOrganisationRes.data.organisations.map((i) => ({
            value: i.orgId,
            title: i.orgName,
          }))
        )
      );

      const localityRes = await axios.get(
        `${REACT_APP_API_URL}/Locality/opensRegister`
      );
      setLocalityOptions(createArrayOptions(localityRes.data.localities));
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

  const handleOrganisationCategory = (value, key) => {
    setContractsData((prev) => ({ ...prev, key: value }));
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
          contractsData
        );
      } else {
        await axios.post(
          `${REACT_APP_API_URL}/Contract/AddEntry`,
          contractsData
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

  const handlePriceListChange = (updatedPriceList) => {
    // setLocalitiesPriceList(updatedPriceList);
    console.log(updatedPriceList);
  };

  return (
    <>
      <div className="d-flex justify-content-between">
        {id ? <h1>Редактирование Контракта</h1> : <h1>Добавление контракта</h1>}
        <button className="fs-1" onClick={toMainPage}>
          ×
        </button>
      </div>
      <form onSubmit={handleSubmit}>
        <div className="input-group mb-4 flex-nowrap gap-3">
          <label>
            Исполнитель
            <MySelect
              newOptions={organisationTypeOptions}
              handleChange={(val) =>
                handleOrganisationCategory(val, "performerId")
              }
              newValue={contractsData.performer?.orgId}
              labelField={"orgName"}
              valueField={"orgId"}
              apiRoute={"Organisation"}
            />
          </label>

          <label>
            Заказчик
            <MySelect
              newOptions={organisationTypeOptions}
              handleChange={(val) =>
                handleOrganisationCategory(val, "customerId")
              }
              newValue={contractsData.customer?.orgId}
              labelField={"orgName"}
              valueField={"orgId"}
              apiRoute={"Organisation"}
            />
          </label>
        </div>

        <div className="input-group mb-4 flex-nowrap gap-3">
          <label>
            Дата заключения
            <DatePicker size="large" aria-required />
          </label>
          <label>
            Дата действия
            <DatePicker size="large" aria-required />
          </label>
        </div>
        <PriceList
          priceList={localitiesPriceList}
          options={localityOptions}
          handlePriceList={handlePriceListChange}
        />

        <div className="d-flex justify-content-end">
          <button className="btn btn-primary btn-lg" type="submit">
            Сохранить
          </button>
        </div>
      </form>
    </>
  );
};

export default EditContractsForm;
