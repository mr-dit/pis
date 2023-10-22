import React from "react";

const Table = ({ data, headers, handleChange, handleDelete }) => {
  const handleRowChange = (registrationNumber) => () => {
    handleChange(registrationNumber);
  };

  const handleRowDelete = (registrationNumber) => () => {
    handleDelete(registrationNumber);
  };

  return (
    <table className="table table-striped table-hover">
      <thead>
        <tr>
          {headers.map((header) => (
            <th key={header.name}>{header.title}</th>
          ))}
        </tr>
      </thead>
      <tbody>
        {data.map((row) => (
          <tr key={row[headers[0].name]}>
            {headers.map((header, i) => (
              <td key={i}>{row[header.name]}</td>
            ))}
            <td>
              <button onClick={handleRowChange(row[headers[0].name])}>
                Изменить
              </button>
            </td>
            <td>
              <button onClick={handleRowDelete(row[headers[0].name])}>
                Удалить
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default Table;