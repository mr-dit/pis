import React, { useState, useEffect } from "react";
import axios from "axios";
import TableReports from "./TableReports";
import Menu from "../../components/Menu/Menu";
import Select from "react-select";
import { getDataForRequest, isRoleEdit } from "../../helpers";
import { Modal, DatePicker } from "antd";
import { OutTable, ExcelRenderer } from "react-excel-renderer";
import dayjs from "dayjs";

const { REACT_APP_API_URL } = process.env;
const dateFormat = "MM-DD-YYYY";

const cols = [
  {
    name: "id",
    title: "id",
    // nonVisible: true,
  },
  { name: "dateStart", title: "Дата начала", sortName: "dateStart" },
  {
    name: "dateEnd",
    title: "Дата окончания",
    sortName: "dateEnd",
  },
  { name: "performer", title: "Исполнитель", sortName: "performer" },
  { name: "status", title: "Статус", sortName: "status" },
  {
    name: "statusUpdate",
    title: "Дата изменения статуса",
    sortName: "statusUpdate",
  },
  { name: "dateCreate", title: "Дата создания отчета", sortName: "dateCreate" },
];
const filterOptions = [
  { label: "Исполнитель", value: "Performer" },
  //   { label: "ИНН", value: "Inn" },
  //   { label: "КПП", value: "Kpp" },
  //   { label: "Адрес регистрации", value: "AdressReg" },
  //   { label: "Город", value: "Locality" },
];

const ReportsPage = () => {
  const [reports, setReports] = useState([]);
  const [filterValue, setFilterValue] = useState("");
  const [sortBy, setSortBy] = useState("");
  const [isAscending, setIsAscending] = useState(true);
  const [filterField, setFilterField] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalItems, setTotalItems] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    fetchData();
  }, [sortBy, isAscending, filterField, pageNumber, pageSize]);

  const fetchData = async () => {
    try {
      const resReporys = await axios.post(
        `${REACT_APP_API_URL}/Statistica/openRegister`,
        getDataForRequest(),
        {
          params: {
            filterValue,
            sortBy,
            isAscending,
            filterField,
            pageNumber,
            pageSize,
          },
        }
      );

      const { reports, totalItems, totalPages } = resReporys.data;

      setReports(reports);
      setTotalItems(totalItems);
      setTotalPages(totalPages);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchOrg();
  }, []);

  const [organisationOptions, setOrganisationOptions] = useState([]);
  const fetchOrg = async () => {
    try {
      const org = await axios.post(
        `${REACT_APP_API_URL}/Organisation/OpensRegister`,
        getDataForRequest(),
        {
          params: {
            filterValue,
            sortBy,
            isAscending,
            filterField,
            pageNumber,
            pageSize,
          },
        }
      );

      const { organisations } = org.data;
      const orgOptions = organisations.map((i) => ({
        label: i.orgName,
        value: `${i.orgId}`,
      }));

      setOrganisationOptions(orgOptions);
    } catch (error) {
      console.error(error);
    }
  };

  const [org, setOrg] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const handleCreate = async () => {
    try {
      await axios.post(
        `${REACT_APP_API_URL}/Statistica/createReport/${startDate}/${endDate}/${org}`,
        {
          ...getDataForRequest(),
        }
      );
      await fetchData();
    } catch (error) {
      alert(error);
    }
  };

  const [isModalOpen, setIsModalOpen] = useState(false);
  const showModal = () => {
    setIsModalOpen(true);
  };
  const handleOk = async () => {
    handleCreate();
    handleCancel();
  };
  const handleCancel = () => {
    setIsTableOpen(false);
    setTable({ rows: [], cols: [] });
    setEndDate();
    setStartDate();
    setOrg();
    setIsModalOpen(false);
  };

  const handleDelete = async (id) => {
    try {
      await axios.post(`${REACT_APP_API_URL}/Statistica/cancel/${id}`, {
        ...getDataForRequest(),
      });
      await fetchData();
    } catch (e) {
      alert(e);
    }
  };
  const handleChange = async (id) => {
    try {
      await axios.post(`${REACT_APP_API_URL}/Statistica/confirm/${id}`, {
        ...getDataForRequest(),
      });
      await fetchData();
    } catch (e) {
      alert(e);
    }
  };

  const handleRecalc = async (id) => {
    try {
      await axios.post(
        `${REACT_APP_API_URL}/Statistica/recalculateReport/${id}`,
        {
          ...getDataForRequest(),
        }
      );
      await fetchData();
    } catch (e) {
        console.error(e);
    }
  };

  const [idReport, setIdReport] = useState();
  const [isTableOpen, setIsTableOpen] = useState(false);
  const [table, setTable] = useState({ rows: [], cols: [] });
  const handleCheck = async (id) => {
    try {
      const response = await axios.post(
        `${REACT_APP_API_URL}/Statistica/getReportAsFile/${id}`,
        getDataForRequest(),
        { responseType: "arraybuffer" }
      );
      setIdReport(id);
      const blob = new Blob([response.data], {
        type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      });

      ExcelRenderer(blob, (err, resp) => {
        if (err) {
          console.log(err);
        } else {
          console.log({ cols: resp.cols, rows: resp.rows });
          setTable({
            cols: resp.cols,
            rows: resp.rows,
          });
        }
      });
      setIsTableOpen(true);
    } catch (e) {
      console.error(e);
    }
  };

  const saveFile = async () => {
    try {
      const response = await axios.post(
        `${REACT_APP_API_URL}/Statistica/getReportAsFile/${idReport}`,
        getDataForRequest(),
        { responseType: "blob" }
      );

      const url = window.URL.createObjectURL(new Blob([response.data]));
      const link = document.createElement("a");
      console.log(url);
      link.href = url;
      link.setAttribute("download", "имя_файла.xlsx");
      document.body.appendChild(link);
      link.click();
      handleCancel();
    } catch (e) {
      alert(e);
    }
  };

  const isOmsu = isRoleEdit([9, 10, 11, 15]);

  return (
    <div>
      <Menu />
      <div className="filter d-flex justify-content-between mb-3 mt-4">
        <div className="d-flex align-items-center">
          <Select
            isClearable
            isSearchable
            placeholder="Поле фильтра..."
            options={filterOptions}
            onChange={(val) => setFilterField(val?.value)}
          />
          <div className="input-group ms-2">
            <input
              type="text"
              className="form-control"
              placeholder="Значение фильтра..."
              value={filterValue}
              onChange={(e) => setFilterValue(e.target.value)}
              aria-label="Recipient's username"
              aria-describedby="button-addon2"
            />
            <button
              className="btn btn-outline-secondary"
              type="button"
              id="button-addon2"
              onClick={fetchData}
            >
              Поиск
            </button>
          </div>
        </div>
        {isOmsu && (
          <button className="btn btn-success mt-2" onClick={showModal}>
            Создать отчет
          </button>
        )}
      </div>
      <TableReports
        data={reports}
        headers={cols}
        handleChange={handleChange}
        handleDelete={handleDelete}
        handleCheck={handleCheck}
        handleRecalculate={handleRecalc}
        handleSortName={(value) => {
          setSortBy(value);
          setIsAscending((prev) => !prev);
        }}
        sortField={sortBy}
      />
      {/* Pagination */}
      {totalPages > 1 && (
        <div>
          <button
            disabled={pageNumber === 1}
            onClick={() => setPageNumber(pageNumber - 1)}
          >
            ←
          </button>
          <span>
            Страница {pageNumber} из {totalPages}
          </span>
          <button
            disabled={pageNumber === totalPages}
            onClick={() => setPageNumber(pageNumber + 1)}
          >
            →
          </button>
        </div>
      )}
      <Modal
        title="Статистика за промежуток"
        open={isModalOpen}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <form>
          <label id="my-label" className="mb-3">
            Дата начала
            <DatePicker
              size="large"
              aria-required
              value={startDate ? dayjs(startDate, dateFormat) : ""}
              format={dateFormat}
              onChange={(dayjs, string) => setStartDate(string)}
            />
          </label>
          <label id="my-label" className="mb-3">
            Дата конца
            <DatePicker
              size="large"
              aria-required
              value={endDate ? dayjs(endDate, dateFormat) : ""}
              format={dateFormat}
              onChange={(dayjs, string) => setEndDate(string)}
            />
          </label>
          <label id="my-label" className="mb-3">
            Организация
            <Select
              isClearable
              isSearchable
              required
              placeholder="По организации..."
              options={organisationOptions}
              onChange={(val) => setOrg(val?.value)}
            />
          </label>
        </form>
      </Modal>
      <Modal
        open={isTableOpen}
        onOk={saveFile}
        okText="Скачать Excel"
        onCancel={handleCancel}
      >
        <OutTable
          data={table.rows}
          columns={table.cols}
          tableClassName="ExcelTable2007"
          tableHeaderRowClass="heading"
        />
      </Modal>
    </div>
  );
};

export default ReportsPage;
