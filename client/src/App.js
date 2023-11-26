import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter as Router } from "react-router-dom";
import { useRoutes } from "./routes";
import locale from "antd/locale/ru_RU.js";
import { ConfigProvider } from "antd";
import AuthPage from "./pages/AuthPage/AuthPage";
import { getDataForRequest } from "./helpers";

function App() {
  const routes = useRoutes();

  if (!getDataForRequest().roles) {
    return (
      <div className="container">
        <Router>
          <AuthPage />
        </Router>
      </div>
    );
  }

  return (
    <Router>
      <ConfigProvider locale={locale}>
        <div className="container">{routes}</div>
      </ConfigProvider>
    </Router>
  );
}

export default App;
