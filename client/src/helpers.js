export function saveToLocalStorage(data) {
  localStorage.setItem("userData", JSON.stringify(data));
  window.location.reload();
}

export function clearLocalStorage() {
  localStorage.removeItem("userData");
  window.location.reload();
}

export function getDataForRequest() {
  const userData = JSON.parse(localStorage.getItem("userData"));
  const requestData = {
    idUser: userData?.data.idUser,
    surname: userData?.data.surname,
    firstName: userData?.data.firstName,
    lastName: userData?.data.lastName,
    organisationId: userData?.data.organisationId,
    login: userData?.data.login,
    password: userData?.data.password,
    roles: userData?.data.roles,
  };
  return requestData;
}
export function getUserId() {
  const userData = JSON.parse(localStorage.getItem("userData"));
  const requestData = userData?.data.idUser;
  return requestData;
}

export function isRoleEdit(roles) {
  const userData = JSON.parse(localStorage.getItem("userData"));

  const userRoles = userData?.data.roles;
  const containsCommonValue = roles.some((value) => userRoles.includes(value));

  return containsCommonValue;
}
