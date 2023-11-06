import logo from "./logo.svg";
import "./App.css";
import { useState } from "react";
import { BrowserRouter as Router } from "react-router-dom";
import { useRoutes } from "./routes";

function App() {
  const routes = useRoutes();

  return (
    <Router>
      <div className="container">{routes}</div>
    </Router>
  );
}

export default App;
