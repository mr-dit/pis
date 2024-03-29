import React, { useEffect, useState } from "react";
import axios from "axios";
import MySelect from "../../components/MySelect/MySelect.tsx";
import { useParams, useNavigate } from "react-router-dom";
import { DatePicker } from "antd";
import PriceList from "./PriceList.js";
import dayjs from "dayjs";
import { getDataForRequest, getUserId, isRoleEdit } from "../../helpers";
const { REACT_APP_API_URL } = process.env;

const { RangePicker } = DatePicker;

const dateFormat = "YYYY-MM-DD";

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
  const [localitiesPriceList, setLocalitiesPriceList] = useState([
    { localityId: "", price: "" },
  ]);
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

      const fetchOrganisationRes = await axios.post(
        `${REACT_APP_API_URL}/Organisation/opensRegister`,
        {
          ...getDataForRequest(),
          params: {
            pageSize: 1000,
          },
        }
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

  const handleOrganisationCategory = (value, key) => {
    setContractsData((prev) => ({ ...prev, [key]: `${value}` }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (id) {
        await axios.post(
          `${REACT_APP_API_URL}/Contract/ChangeEntry/${id}?userId=${getUserId()}`,
          contractsData
        );
      } else {
        await axios.post(
          `${REACT_APP_API_URL}/Contract/AddEntry?userId=${getUserId()}`,
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
    setContractsData((prev) => ({
      ...prev,
      localitiesPriceList: updatedPriceList,
    }));
  };

  const handleDate = (val, str) => {
    if (val) {
      setContractsData((prev) => ({
        ...prev,
        conclusionDate: str[0],
      }));
      setContractsData((prev) => ({
        ...prev,
        expirationDate: str[1],
      }));
    } else {
      setContractsData((prev) => ({
        ...prev,
        conclusionDate: "",
        expirationDate: "",
      }));
    }
  };

  const createDate = (conclusionDate, expirationDate) => {
    if(!conclusionDate) return[]
    return [dayjs(conclusionDate), dayjs(expirationDate)];
  };

  const isNotEdit = !isRoleEdit([10, 15]);

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
          <label id="my-label">
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
              disabled={isNotEdit}
            />
          </label>

          <label id="my-label">
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
              disabled={isNotEdit}
            />
          </label>
        </div>

        <div className="input-group mb-4 flex-nowrap gap-3">
          <label id="my-label">
            Даты заключения и окончания контракта
            <RangePicker
              size="large"
              value={createDate(
                contractsData.conclusionDate,
                contractsData.expirationDate
              )}
              placeholder={["Начало", "Конец"]}
              onChange={handleDate}
              showToday
              disabled={isNotEdit}
              aria-required
              format={dateFormat}
            />
          </label>
        </div>
        <PriceList
          disabled={isNotEdit}
          priceList={localitiesPriceList}
          options={localityOptions}
          handlePriceList={handlePriceListChange}
        />
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

export default EditContractsForm;
