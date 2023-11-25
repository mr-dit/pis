export function saveToLocalStorage(data) {
  localStorage.setItem("userData", JSON.stringify(data));
}

export function clearLocalStorage() {
    localStorage.removeItem('userData');
  }

export function getDataForRequest() {
  const userData = JSON.parse(localStorage.getItem("userData"));
  const requestData = {
    surname: userData?.data.surname,
    firstName: userData?.data.firstName,
    lastName: userData?.data.lastName,
    organisationId: userData?.data.organisationId,
    login: userData?.data.login,
    password: userData?.data.password,
    roles: userData?.data.roles
  };
  return requestData;
}
