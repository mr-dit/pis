using System;
using pis.Repositorys;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
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

        public (List<Organisation>, int) GetOrganisationsByOrg(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize, UserPost user)
        {
            var filterFields = new Dictionary<string, Func<string, int, int, string, bool, UserPost, (List<Organisation>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [""] = _organisationRepository.GetOrganisationsByDefault,
                [nameof(Organisation.OrgName)] = _organisationRepository.GetOrganisationsByOrgName,
                [nameof(Organisation.INN)] = _organisationRepository.GetOrganisationsByINN,
                [nameof(Organisation.KPP)] = _organisationRepository.GetOrganisationsByKPP,
                [nameof(Organisation.Locality)] = _organisationRepository.GetOrganisationsByLocality,
                [nameof(Organisation.AdressReg)] = _organisationRepository.GetOrganisationsByAdressReg
            };
            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending, user);
        }

        internal (List<Organisation>, int) GetOrganisationsForOperatorVetService(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var filterFields = new Dictionary<string, Func<string, int, int, string, bool, (List<Organisation>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [""] = _organisationRepository.GetOrganisationsByDefaultForOperatorVetService,
                [nameof(Organisation.OrgName)] = _organisationRepository.GetOrganisationsByOrgNameForOperatorVetService,
                [nameof(Organisation.INN)] = _organisationRepository.GetOrganisationsByINNForOperatorVetService,
                [nameof(Organisation.KPP)] = _organisationRepository.GetOrganisationsByKPPForOperatorVetService,
                [nameof(Organisation.Locality)] = _organisationRepository.GetOrganisationsByLocalityForOperatorVetService,
                [nameof(Organisation.AdressReg)] = _organisationRepository.GetOrganisationsByAdressRegForOperatorVetService
            };
            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
        }

        internal (List<Organisation>, int) GetOrganisationsForOperatorOMSU(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var filterFields = new Dictionary<string, Func<string, int, int, string, bool, (List<Organisation>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [""] = _organisationRepository.GetOrganisationsByDefaultForOperatorOMSU,
                [nameof(Organisation.OrgName)] = _organisationRepository.GetOrganisationsByOrgNameForOperatorOMSU,
                [nameof(Organisation.INN)] = _organisationRepository.GetOrganisationsByINNForOperatorOMSU,
                [nameof(Organisation.KPP)] = _organisationRepository.GetOrganisationsByKPPForOperatorOMSU,
                [nameof(Organisation.Locality)] = _organisationRepository.GetOrganisationsByLocalityForOperatorOMSU,
                [nameof(Organisation.AdressReg)] = _organisationRepository.GetOrganisationsByAdressRegForOperatorOMSU
            };
            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
        }
    }
}

