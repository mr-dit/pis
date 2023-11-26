import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Select from "react-select";
import axios from "axios";
import MySelect from "../../components/MySelect/MySelect.tsx";
import { Modal } from "antd";
import CreatableSelect from "react-select/creatable";
import { getDataForRequest } from "../../helpers";

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
const createOption = (label, id) => ({
  label,
  value: id,
});

const Vaccine = ({ vaccinations }) => {
  const { id } = useParams();
  const [value, setValue] = useState();
  const [isLoading, setIsLoading] = useState(false);
  const [contracts, setContracts] = useState([]);
  const [vaccines, setVaccines] = useState([]);
  const [vaccineSeriesNumber, setVaccineSeriesNumber] = useState("");
  const [isVisible, setIsVisible] = useState(false);
  const [vaccination, setVaccination] = useState({
    contractId: "",
    vaccineId: "",
    vaccineSeries: "",
  });

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const contractsRes = await axios.post(
        `${REACT_APP_API_URL}/Contract/opensRegister?pageSize=1000`,
        getDataForRequest()
      );
      setContracts(
        contractsRes.data.contracts.map((i) => ({
          value: i.idContract,
          label: i.idContract,
        }))
      );

      const vaccineRes = await axios.get(
        `${REACT_APP_API_URL}/Vaccine/opensRegister?pageSize=1000`
      );
      setVaccines(
        vaccineRes.data.vaccines.map((i) => ({
          value: i.idVaccine,
          label: `${i.nameVaccine} / ${i.validDaysVaccine} суток`,
        }))
      );
    } catch (e) {
      console.error(e);
    }
  };

  const saveVaccination = async () => {
    console.log(vaccination);
    try {
      axios.post(
        `${REACT_APP_API_URL}/Vaccination/add/${id}/${vaccination.vaccineId}/${
          vaccination.contractId
        }/${getDataForRequest().login}/${vaccination.vaccineSeries}`
      );
    } catch (error) {
      console.error(error);
    }
  };

  const [newVaccineName, setNewVaccineName] = useState('');
  const [newValidDate, setNewValidDate] = useState('');
  
  const handleCreate = async (inputValue) => {
    setIsLoading(true);
    showModal();
    setNewVaccineName(inputValue);
  };

  const [isModalOpen, setIsModalOpen] = useState(false);
  const showModal = () => {
    setIsModalOpen(true);
  };
  const handleOk = async () => {
    if (!newValidDate) return;
    const data = { nameVaccine: newVaccineName, validDaysVaccine: newValidDate };
    try {
      const res = await axios.post(
        `${REACT_APP_API_URL}/Vaccine/addEntry`,
        data
      );
      const newOption = createOption(`${newVaccineName} / ${newValidDate} суток`, res.data); 
      setIsLoading(false);
      setVaccines((prev) => [...prev, newOption]);
      setValue(newOption);
    } catch (e) {
      console.error(e);
      setIsLoading(false);
    }
    setIsModalOpen(false);
  };
  const handleCancel = () => {
    setIsModalOpen(false);
    setIsLoading(false);
    setNewVaccineName('')
    setNewValidDate('')
  };

  return (
    <>
      {vaccinations.map((i) => (
        <div
          key={i.idVactination}
          className="input-group mb-4 flex-nowrap gap-3"
        >
          <label id="my-label">
            Номер контракта
            <input
              className="form-control"
              type="text"
              value={i.contractId}
              disabled
            />
          </label>
          <label id="my-label">
            Вакцина
            <input
              className="form-control"
              type="text"
              value={i.vaccine}
              disabled
            />
          </label>
          <label id="my-label">
            Дата вакцинации
            <input
              className="form-control"
              type="text"
              value={i.vaccinationDate}
              disabled
            />
          </label>
          <label id="my-label">
            Дата действия
            <input
              className="form-control"
              type="text"
              value={i.vaccinationValidDate}
              disabled
            />
          </label>

          <label id="my-label">
            Серия вакцины
            <input
              className="form-control"
              type="text"
              value={i.vaccineSeriesNumber}
              disabled
            />
          </label>
        </div>
      ))}
      {isVisible ? (
        <div className="input-group mb-4 flex-nowrap gap-3">
          <label id="my-label">
            Номер контракта
            <Select
              isClearable
              isSearchable
              placeholder="Выберите"
              options={contracts}
              onChange={(val) =>
                setVaccination((prev) => ({ ...prev, contractId: val }))
              }
            />
          </label>
          <label id="my-label">
            Вакцина
            <CreatableSelect
              required
              placeholder={"Выберите"}
              isClearable
              isDisabled={isLoading}
              isLoading={isLoading}
              onChange={(newValue) => {
                setValue(newValue);
                setVaccination((prev) => ({
                  ...prev,
                  vaccineId: newValue?.value,
                }));
              }}
              onCreateOption={handleCreate}
              options={vaccines}
              value={value}
            />
            {/* <MySelect
              isClearable
              isSearchable
              placeholder="Выберите"
              newOptions={vaccines}
              handleChange={(val) =>
                setVaccination((prev) => ({ ...prev, vaccineId: val }))
              }
            /> */}
          </label>
          <label id="my-label">
            Серия вакцины
            <input
              className="form-control"
              type="number"
              value={vaccineSeriesNumber}
              onChange={(e) => setVaccineSeriesNumber(e.target.value)}
            />
          </label>
          <button
            type="button"
            className="btn btn-outline-primary"
            style={{ borderRadius: "0.5rem" }}
            onClick={saveVaccination}
          >
            Сохранить вакцинацию
          </button>
          {/* <label id="my-label">
          Вакцина
          <input
            className="form-control"
            type="text"
            value={vaccine}
            disabled
          />
        </label>

   */}
        </div>
      ) : (
        ""
      )}
      {!isVisible && (
        <button
          type="button"
          className="btn btn-outline-primary"
          onClick={() => setIsVisible(true)}
        >
          Добавить вакцинацию
        </button>
      )}
      <Modal
        title="Длительность вакцины в днях"
        open={isModalOpen}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <label id="my-label">
          <input
            className="form-control"
            type="number"
            value={newValidDate}
            onChange={(e) => setNewValidDate(e.target.value)}
          />
        </label>
      </Modal>
    </>
  );
};

export default Vaccine;
