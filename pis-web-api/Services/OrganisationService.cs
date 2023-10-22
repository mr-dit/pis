using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
	public class OrganisationService
	{
        private OrganisationsRepository _organisationRepository;

        public OrganisationService()
        {
            _organisationRepository = new OrganisationsRepository();
        }

        public bool FillData(Organisation org)
        {
            bool status = _organisationRepository.Add(org);
            return status;
        }

        public bool DeleteEntry(int id)
        {
            bool status = _organisationRepository.Remove(_organisationRepository.GetById(id));
            return status;
        }

        public Organisation GetEntry(int id)
        {
            var entry = _organisationRepository.GetById(id);
            return entry;
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


        public bool ChangeEntry(Organisation org)
        {
            bool status = _organisationRepository.Update(org);
            return status;
        }
    }
}

