import { useState, useEffect } from "react";
import lupa from "../../magnifying-glass-bold.svg";

const Table = ({
  data,
  headers,
  handleChange,
  handleDelete,
  handleSortName,
  sortField,
  isEdit = false,
  isDelete = false,
  onRowSelect,
}) => {
  const [isAscending, setIsAscending] = useState(false);
  const [selectedRows, setSelectedRows] = useState([]); 

  const handleRowChange = (registrationNumber) => () => {
    handleChange(registrationNumber);
  };

  const handleRowDelete = (registrationNumber) => () => {
    handleDelete(registrationNumber);
  };

  const handleColSortName = (value) => () => {
    handleSortName(value);
    setIsAscending((prev) => !prev);
  };

  const handleSelectAll = () => {
    const allRegistrationNumbers = data.map((row) => {
      return row.id;
    });
    setSelectedRows(allRegistrationNumbers);
  };

  const handleCheckboxChange = (registrationNumber) => (e) => {
    if (e.target.checked) {
      setSelectedRows((prevSelectedRows) => [
        ...prevSelectedRows,
        registrationNumber,
      ]);
    } else {
      setSelectedRows((prevSelectedRows) =>
        prevSelectedRows.filter((row) => row !== registrationNumber)
      );
    }
  };

  useEffect(() => {
    onRowSelect(selectedRows);
  }, [selectedRows]);

  return (
    <>
      <button onClick={handleSelectAll}>Выделить все</button>
      <table className="table table-striped table-hover">
        <thead>
          <tr>
            {isDelete && <th></th>}
            {headers.map((header) => (
              <th
                style={{ display: header.nonVisible ? "none" : "table-cell" }}
                key={header.name}
                onClick={
                  header.sortName ? handleColSortName(header.sortName) : null
                }
              >
                {header.title}
                {sortField === header.sortName
                  ? isAscending
                    ? " ▼"
                    : " ▲"
                  : null}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {data.map((row) => (
            <tr key={row[headers[0].name]}>
              {isDelete && (
                <td className="d-flex justify-content-center">
                  <input
                    style={{ height: "34px", width: "20px" }}
                    type="checkbox"
                    checked={selectedRows.includes(row[headers[0].name])}
                    onChange={handleCheckboxChange(row[headers[0].name])}
                  />
                </td>
              )}
              {headers.map((header, i) => (
                <td
                  key={i}
                  style={{ display: header.nonVisible ? "none" : "table-cell" }}
                >
                  {row[header.name]}
                </td>
              ))}
              {isEdit && (
                <td>
                  <button onClick={handleRowChange(row[headers[0].name])}>
                    <svg
                      width="24"
                      strokeWidth="1.5"
                      height="24"
                      viewBox="0 0 24 24"
                      fill="none"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      {" "}
                      <path
                        d="M20 12V5.74853C20 5.5894 19.9368 5.43679 19.8243 5.32426L16.6757 2.17574C16.5632 2.06321 16.4106 2 16.2515 2H4.6C4.26863 2 4 2.26863 4 2.6V21.4C4 21.7314 4.26863 22 4.6 22H11"
                        stroke="currentColor"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                      />{" "}
                      <path
                        d="M8 10H16M8 6H12M8 14H11"
                        stroke="currentColor"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                      />{" "}
                      <path
                        d="M16 5.4V2.35355C16 2.15829 16.1583 2 16.3536 2C16.4473 2 16.5372 2.03725 16.6036 2.10355L19.8964 5.39645C19.9628 5.46275 20 5.55268 20 5.64645C20 5.84171 19.8417 6 19.6464 6H16.6C16.2686 6 16 5.73137 16 5.4Z"
                        fill="currentColor"
                        stroke="currentColor"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                      />{" "}
                      <path
                        d="M17.9541 16.9394L18.9541 15.9394C19.392 15.5015 20.102 15.5015 20.5399 15.9394V15.9394C20.9778 16.3773 20.9778 17.0873 20.5399 17.5252L19.5399 18.5252M17.9541 16.9394L14.963 19.9305C14.8131 20.0804 14.7147 20.2741 14.6821 20.4835L14.4394 22.0399L15.9957 21.7973C16.2052 21.7646 16.3988 21.6662 16.5487 21.5163L19.5399 18.5252M17.9541 16.9394L19.5399 18.5252"
                        stroke="currentColor"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                      />{" "}
                    </svg>
                  </button>
                </td>
              )}
              {isEdit && (
                <td>
                  <button onClick={handleRowDelete(row[headers[0].name])}>
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
                </td>
              )}
              {!isEdit && !isDelete && (
                <td>
                  <button onClick={handleRowChange(row[headers[0].name])}>
                    <img
                      style={{ width: "30px", height: "32px" }}
                      src={lupa}
                    ></img>
                  </button>
                </td>
              )}
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
};

export default Table;
