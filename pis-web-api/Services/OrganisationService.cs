using System;
using pis.Models;
using pis.Repositorys;
using pis_web_api.Services;

namespace pis.Services
{
	public class OrganisationService : Service<Organisation>
	{
        private OrganisationsRepository _organisationRepository;

        public OrganisationService()
        {
            _organisationRepository = new OrganisationsRepository();
            _repository = _organisationRepository;
        }

        public (List<Organisation>, int) GetOrganisations(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var filterFields = new Dictionary<string, Func<string, int, int, string, bool, (List<Organisation>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [""] = _organisationRepository.GetOrganisationsByDefault,
                [nameof(Organisation.OrgName)] = _organisationRepository.GetOrganisationsByOrgName,
                [nameof(Organisation.INN)] = _organisationRepository.GetOrganisationsByINN,
                [nameof(Organisation.KPP)] = _organisationRepository.GetOrganisationsByKPP,
                [nameof(Organisation.Locality)] = _organisationRepository.GetOrganisationsByLocality,
                [nameof(Organisation.AdressReg)] = _organisationRepository.GetOrganisationsByAdressReg
            };
            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
        }
    }
}

