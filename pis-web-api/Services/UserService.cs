using pis.Models;
using pis.Repositorys;
using System.Runtime.InteropServices;

namespace pis_web_api.Services
{
    public class UserService : Service<User>
    {
        private UserRepository _userRepository;

        public UserService() 
        {
            _userRepository = new UserRepository();
            _repository = _userRepository;
        }

        public (List<User>, int) GetUsers(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var filterFields = new Dictionary<string, Func<string, int, int, string, bool, (List<User>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(User.Surname)] = _userRepository.GetUsersBySurname,

                [nameof(User.FirstName)] = _userRepository.GetUsersByFirstName,

                [nameof(User.LastName)] = _userRepository.GetUsersByLastName,

                [nameof(User.Organisation)] = _userRepository.GetUsersByOrganisation,

                [""] = _userRepository.GetUsersByDefault
            };
            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
        }
    }
}
