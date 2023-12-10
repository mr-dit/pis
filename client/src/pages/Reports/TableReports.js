import { useState, useEffect } from "react";
import lupa from "../../magnifying-glass-bold.svg";

const TableReports = ({
  data,
  headers,
  handleChange,
  handleDelete,
  handleSortName,
  sortField = [],
  isEdit = false,
}) => {
  const [isAscending, setIsAscending] = useState(false);

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
              {isEdit && (
                <td>
                  <button onClick={handleRowChange(row[headers[0].name])}>
                  ✓
                  </button>
                </td>
              )}
              {isEdit && (
                <td>
                  <button class="btn-close" onClick={handleRowDelete(row[headers[0].name])}>
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
