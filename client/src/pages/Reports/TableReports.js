import { useState } from "react";
import { isRoleEdit } from "../../helpers";

const TableReports = ({
  data,
  headers,
  handleChange,
  handleDelete,
  handleCheck,
  handleSortName,
  sortField = [],
}) => {
  const [isAscending, setIsAscending] = useState(false);

  const handleRowChange = (registrationNumber) => () => {
    handleChange(registrationNumber);
  };

  const handleRowDelete = (registrationNumber) => () => {
    handleDelete(registrationNumber);
  };

  const handleRowCheck = (registrationNumber) => () => {
    handleCheck(registrationNumber);
  };

  const handleColSortName = (value) => () => {
    handleSortName(value);
    setIsAscending((prev) => !prev);
  };

  return (
    <>
      <table className="table table-striped table-hover">
        <thead>
          <tr>
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
              {headers.map((header, i) => (
                <td
                  key={i}
                  style={{ display: header.nonVisible ? "none" : "table-cell" }}
                >
                  {row[header.name]}
                </td>
              ))}
              <td>
                <button
                  className="mt-2"
                  onClick={handleRowCheck(row[headers[0].name])}
                >
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
              {row.status.includes("Дораб") ? (
                <td>
                  <button
                    className="btn btn-light mt-2"
                    onClick={handleRowChange(row[headers[0].name])}
                  >
                    Пересчитать
                  </button>
                </td>
              ) : (
                <td></td>
              )}
              {isRoleEdit(row.rolesAccess) && (
                <td>
                  <button
                    className="fs-2"
                    onClick={handleRowChange(row[headers[0].name])}
                  >
                    √
                  </button>
                </td>
              )}
              {isRoleEdit(row.rolesAccess) && (
                <td>
                  <button
                    className="fs-2"
                    onClick={handleRowDelete(row[headers[0].name])}
                  >
                    ×
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

export default TableReports;
