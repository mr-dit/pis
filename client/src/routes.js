import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import AnimalPage from "./pages/Animal/AnimalPage";
import EditAnimal from "./pages/Animal/EditAnimal";

export const useRoutes = () => {
  return (
    <Routes>
      <Route path="/Animal" element={<AnimalPage></AnimalPage>}></Route>
      <Route
        path="/Animal/:id"
        element={<EditAnimal></EditAnimal>}
      ></Route>

      <Route path="*" element={<Navigate to="/Animal" replace />} />
    </Routes>
  );
};
