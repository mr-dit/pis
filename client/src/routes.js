import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import AnimalPage from "./pages/Animal/AnimalPage";
import EditAnimal from "./pages/Animal/EditAnimal";
import EditOrganisation from './pages/Organisation/EditOrganisation'
import OrganisationPage from "./pages/Organisation/OrganisationPage";
import ContractPage from "./pages/Contract/ContractPage";

export const useRoutes = () => {
  return (
    <Routes>
      <Route path="/Animal" element={<AnimalPage></AnimalPage>}></Route>
      <Route
        path="/Animal/update/:id"
        element={<EditAnimal></EditAnimal>}
      ></Route>
      <Route path="/Animal/update" element={<EditAnimal></EditAnimal>}></Route>

      <Route
        path="/Organisation"
        element={<OrganisationPage></OrganisationPage>}
      ></Route>
      <Route
        path="/Organisation/update/:id"
        element={<EditOrganisation></EditOrganisation>}
      ></Route>
      <Route
        path="/Organisation/update"
        element={<EditOrganisation></EditOrganisation>}
      ></Route>

      <Route
        path="/Contract"
        element={<ContractPage></ContractPage>}
      ></Route>
      <Route
        path="/Contract/update/:id"
        element={<EditOrganisation></EditOrganisation>}
      ></Route>
      <Route
        path="/Contract/update"
        element={<EditOrganisation></EditOrganisation>}
      ></Route>

      <Route path="*" element={<Navigate to="/Animal" replace />} />
    </Routes>
  );
};
