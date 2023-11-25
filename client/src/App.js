import logo from "./logo.svg";
import "./App.css";
import { useState } from "react";
import { BrowserRouter as Router } from "react-router-dom";
import { useRoutes } from "./routes";
import locale from 'antd/locale/ru_RU.js';
import { ConfigProvider } from "antd";


function App() {
  const routes = useRoutes();

  return (
    <Router>
      <ConfigProvider locale={locale}>
      <div className="container">{routes}</div>
      </ConfigProvider>
    </Router>
  );
}

export default App;
