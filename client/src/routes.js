import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import AnimalPage from "./pages/Animal/AnimalPage";
import EditAnimal from "./pages/Animal/EditAnimal";
import EditOrganisation from "./pages/Organisation/EditOrganisation";
import OrganisationPage from "./pages/Organisation/OrganisationPage";
import ContractPage from "./pages/Contract/ContractPage";
import EditContractsForm from "./pages/Contract/EditContract";
import AuthPage from "./pages/AuthPage/AuthPage";
import LoggingOrganisation from "./pages/Organisation/LoggingOrganisation";

export const useRoutes = () => {
  return (
    <Routes>
      <Route path="/Animal" element={<AnimalPage />}></Route>
      <Route path="/Animal/update/:id" element={<EditAnimal />}></Route>
      <Route path="/Animal/update" element={<EditAnimal />}></Route>

      <Route path="/Organisation" element={<OrganisationPage />}></Route>
      <Route
        path="/Organisation/update/:id"
        element={<EditOrganisation />}
      ></Route>
      <Route path="/Organisation/update" element={<EditOrganisation />}></Route>
      <Route
        path="/Organisation/logging"
        element={<LoggingOrganisation />}
      ></Route>

      <Route path="/Contract" element={<ContractPage />}></Route>
      <Route
        path="/Contract/update/:id"
        element={<EditContractsForm />}
      ></Route>
      <Route path="/Contract/update" element={<EditContractsForm />}></Route>
      <Route path="/Auth" element={<AuthPage />}></Route>

      <Route path="*" element={<Navigate to="/Animal" replace />} />
    </Routes>
  );
};
