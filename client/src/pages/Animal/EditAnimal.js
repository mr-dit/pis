import React, { useState } from "react";
import { Select } from "antd";

const EditAnimalForm = ({ animal, handleUpdate }) => {
  const [animalData, setAnimalData] = useState({});
  const [locality, setLocality] = useState({})

  const fetchAnimal = async () => {};

  const handleChange = (e) => {
    const { name, value } = e.target;
    setAnimalData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const filterOption = (input, option) =>
  (option?.label ?? '').toLowerCase().includes(input.toLowerCase());


  const handleSubmit = (e) => {
    e.preventDefault();
    // handleUpdate(animalData);
    console.log("edit");
  };

  return (
    <form onSubmit={handleSubmit}>
      {/* <label>
        Locality ID:
        <input
          type="text"
          name="localityId"
          value={animalData.localityId}
          onChange={handleChange}
          required
        />
      </label> */}
      <Select
        labelInValue="Город"
        showSearch
        placeholder="Select a person"
        optionFilterProp="children"
        onChange={(value) => setLocality(value)}
        filterOption={filterOption}
        options={[
          {
            value: "jack",
            label: "Jack",
          },
          {
            value: "lucy",
            label: "Lucy",
          },
          {
            value: "tom",
            label: "Tom",
          },
        ]}
      />
      <label>
        Animal Category ID:
        <input
          type="text"
          name="animalCategoryId"
          value={animalData.animalCategoryId}
          onChange={handleChange}
          required
        />
      </label>
      <label>
        Gender ID:
        <input
          type="text"
          name="genderId"
          value={animalData.genderId}
          onChange={handleChange}
          required
        />
      </label>
      <label>
        Year of Birth:
        <input
          type="text"
          name="yearOfBirth"
          value={animalData.yearOfBirth}
          onChange={handleChange}
          required
        />
      </label>
      <label>
        Electronic Chip Number:
        <input
          type="text"
          name="electronicChipNumber"
          value={animalData.electronicChipNumber}
          onChange={handleChange}
          required
        />
      </label>
      <label>
        Animal Name:
        <input
          type="text"
          name="animalName"
          value={animalData.animalName}
          onChange={handleChange}
          required
        />
      </label>
      <button type="submit">Update</button>
    </form>
  );
};

export default EditAnimalForm;
