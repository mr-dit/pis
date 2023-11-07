import React, { useEffect, useState } from "react";
import axios from "axios";
import CreatableSelect from "react-select/creatable";

interface Option {
  readonly label: string;
  readonly value: string;
}

const createOption = (label: string, id: string) => ({
  label,
  value: id,
});

const { REACT_APP_API_URL } = process.env;

export default ({
  isCreate = true,
  newPlaceholder = 'Выберите',
  newOptions,
  handleChange,
  newValue,
  labelField,
  valueField,
  apiRoute,
}) => {
  const [isLoading, setIsLoading] = useState(false);
  const [options, setOptions] = useState(newOptions);

  const getCategoryById = async (id: string) => {
    if (!id) {
      return;
    }

    setIsLoading(true);
    try {
      const res = await axios.get(`${REACT_APP_API_URL}/${apiRoute}/${id}`);
      const category = createOption(res.data[labelField], res.data[valueField]);
      setValue(category);
      setIsLoading(false);
    } catch (e) {
      console.error(e);
      setIsLoading(false);
    }
  };

  useEffect(() => {
    setOptions(newOptions);
    getCategoryById(newValue);
  }, [newOptions, newValue]);

  const [value, setValue] = useState<Option | null>();

  const handleCreate = async (inputValue: string) => {
    setIsLoading(true);
    const data = { nameAnimalCategory: inputValue };
    try {
      await axios.post(`${REACT_APP_API_URL}/${apiRoute}/addEntry`, data);
      const newOption = createOption(inputValue, "777"); //----------------------------------------------------------
      setIsLoading(false);
      setOptions((prev) => [...prev, newOption]);
      setValue(newOption);
    } catch (e) {
      console.error(e);
      setIsLoading(false);
    }
  };

  return (
    <CreatableSelect
      required
      placeholder={newPlaceholder}
      isClearable={isCreate}
      isDisabled={isLoading}
      isLoading={isLoading}
      onChange={(newValue) => {
        setValue(newValue);
        handleChange(newValue?.value);
      }}
      onCreateOption={handleCreate}
      options={options}
      value={value}
    />
  );
};
