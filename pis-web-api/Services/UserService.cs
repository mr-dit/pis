﻿using pis.Repositorys;
using pis_web_api.Models.db;
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

                [nameof(User.Organisation)] = _userRepository.GetUsersByOrganisationName,

                [""] = _userRepository.GetUsersByDefault
            };
            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
        }

        public List<User> GetUsersByOrganisation(int orgId) =>
            _userRepository.GetUsersByOrganisation(orgId);

        public User LoginUser(string login, string password)
        {
            return _userRepository.GetUserByLogin(login, password);
        }
    }
}
