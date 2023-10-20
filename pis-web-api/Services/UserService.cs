using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class UserService
    {
        public static bool FillData(User user)
        {
            bool status = UserRepository.AddUser(user);
            return status;
        }

        public static bool DeleteEntry(int id)
        {
            bool status = UserRepository.DeleteUser(UserRepository.GetUserById(id));
            return status;
        }

        public static User? GetEntry(int id)
        {
            var entry = UserRepository.GetUserById(id);
            return entry;
        }

        public static (List<User>, int) GetUsers(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            List<User> vaccines;
            int count;
            switch (filterField)
            {
                case nameof(User.Surname):
                    (vaccines, count) = UserRepository.GetUsersBySurname(filterValue, pageNumber, pageSize, sortBy, isAscending);
                    break;
                case nameof(User.FirstName):
                    (vaccines, count) = UserRepository.GetUsersByFirstName(filterValue, pageNumber, pageSize, sortBy, isAscending);
                    break;
                case nameof(User.LastName):
                    (vaccines, count) = UserRepository.GetUsersByLastName(filterValue, pageNumber, pageSize, sortBy, isAscending);
                    break;
                //case nameof(User.Roles):
                //    (vaccines, count) = UserRepository.GetUsersByRole(filterValue, pageNumber, pageSize, sortBy, isAscending);
                //    break;
                case nameof(User.Organisation):
                    (vaccines, count) = UserRepository.GetUsersByOrganisation(filterValue, pageNumber, pageSize, sortBy, isAscending);
                    break;
                case "":
                    (vaccines, count) = UserRepository.GetUsersBySurname(filterValue, pageNumber, pageSize, sortBy, isAscending);
                    break;

                default:
                    throw new ArgumentException("Нет такого поля для фильтрации");
            }

            return (vaccines, count);
        }


        public static bool ChangeEntry(User user)
        {
            bool status = UserRepository.UpdateUser(user);
            return status;
        }
    }
}
