import { useEffect, useState, useMemo } from "react";
import MySelect from "../../components/MySelect/MySelect.tsx";

const PriceList = ({ priceList, options, handlePriceList }) => {
  const [localitiesPriceList, setLocalitiesPriceList] = useState([]);
  const [localityOptions, setLocalityOptions] = useState();
  const [usedLocalities, setUsedLocalities] = useState();

  useEffect(() => {
    setUsedLocalities(priceList.map((item) => `${item.localityId}`));
    setLocalitiesPriceList(priceList);
    setLocalityOptions(
      options.map((op) => ({
        ...op,
        isDisabled: usedLocalities.includes(op.value),
      }))
    );
  }, [options, priceList]);

  useEffect(() => {
    setLocalityOptions((prev) =>
      prev.map((op) => ({
        ...op,
        isDisabled: usedLocalities.includes(op.value),
      }))
    );
  }, [usedLocalities]);

  useEffect(() => {
    handlePriceList(localitiesPriceList);
  }, [localitiesPriceList, handlePriceList]);

  const handleAddPrice = () => {
    setLocalitiesPriceList((prev) => [
      ...prev,
      {
        localityId: "",
        price: "",
      },
    ]);
  };

  const handleLocality = (value, idx) => {
    setLocalitiesPriceList((prev) => {
      const updatedPriceList = [...prev];
      const unUsedLoc = updatedPriceList[idx].localityId;
      if (unUsedLoc) {
        setUsedLocalities((prev) => prev.filter((i) => i != unUsedLoc));
      }
      updatedPriceList[idx].localityId = value;
      setUsedLocalities((prev) =>
        prev.includes(value) ? prev : [...prev, value]
      );

      setLocalityOptions((prev) =>
        prev.map((op) => ({
          ...op,
          isDisabled:
            usedLocalities.includes(op.value) &&
            op.value !== value &&
            !usedLocalities.includes(unUsedLoc),
        }))
      );

      return updatedPriceList;
    });
  };

  const handleChange = (value, idx) => {
    setLocalitiesPriceList((prev) => {
      const updatedPriceList = [...prev]; // создаем новый массив на основе предыдущего состояния
      updatedPriceList[idx].price = value; // обновляем значение в массиве localitiesPriceList
      return updatedPriceList; // возвращаем обновленный массив
    });
  };

  const handleRowDelete = (idx, localityId) => () => {
    setLocalitiesPriceList((prev) => prev.filter((i, num) => num !== idx));
    setUsedLocalities((prev) => prev.filter((i) => i != localityId));
  };

  return (
    <>
      {localitiesPriceList.length > 0
        ? localitiesPriceList.map((i, idx) => (
            <div
              key={idx}
              className="d-flex gap-3 mb-3"
              style={{ maxWidth: "500px" }}
            >
              <label>
                Город
                <MySelect
                  isClearable={false}
                  newOptions={localityOptions}
                  handleChange={(val) => handleLocality(val, idx)}
                  newValue={i.localityId}
                  labelField={"nameLocality"}
                  valueField={"idLocality"}
                  apiRoute={"Locality"}
                />
              </label>
              <label>
                Цена
                <input
                  className="form-control"
                  type="text"
                  value={i.price}
                  onChange={(e) => handleChange(e.target.value, idx)}
                  required
                />
              </label>
              <button
                onClick={handleRowDelete(idx, i.localityId)}
                className="mt-4"
                type="button"
              >
                <svg
                  style={{ color: "red" }}
                  xmlns="http://www.w3.org/2000/svg"
                  width="20"
                  height="20"
                  fill="currentColor"
                  className="bi bi-trash"
                  viewBox="0 0 16 16"
                >
                  {" "}
                  <path
                    d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"
                    fill="red"
                  ></path>{" "}
                  <path
                    fillRule="evenodd"
                    d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"
                    fill="red"
                  ></path>{" "}
                </svg>
              </button>
            </div>
          ))
        : null}
      <button
        type="button"
        className="btn btn-outline-primary"
        onClick={handleAddPrice}
      >
        Добавить цену
      </button>
    </>
  );
};

export default PriceList;
