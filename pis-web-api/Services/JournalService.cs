using Microsoft.EntityFrameworkCore;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;
using System.Reflection.Metadata.Ecma335;

namespace pis_web_api.Services
{
    public class JournalService
    {
        private Repository<Journal> _repository;
        private AnimalService _animalService;
        private ContractService _contractService;
        private OrganisationService _organisationService;

        private Dictionary<string, Func<Journal, string, bool>> filter = new Dictionary<string, Func<Journal, string, bool>>
            (StringComparer.InvariantCultureIgnoreCase)
        {
            [""] = (journal, filterValue) => (true),
            ["fio"] = (journal, filterValue) => ((journal.User.LastName + " " + journal.User.FirstName + " " + journal.User.Surname)
                                            .Contains(filterValue, StringComparison.InvariantCultureIgnoreCase)),
            ["orgName"] = (journal, filterValue) => (journal.User.Organisation.OrgName.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase)),
            ["userLogin"] = (journal, filterValue) => (journal.User.Login.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase)),
            ["idObject"] = (journal, filterValue) => (journal.EditID.ToString() == filterValue),
            ["descObject"] = (journal, filterValue) => (journal.DescriptionObject.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase))
        };
        public JournalService() 
        {
            _repository = new Repository<Journal>();
            _animalService = new AnimalService();
            _contractService = new ContractService();
            _organisationService = new OrganisationService();
        }

        public void JournalAddAnimal(int userId, int animalId)
        {
            Animal animal = _animalService.GetEntry(animalId);
            if (animal == null)
            {
                throw new Exception("Данного животного не существует");
            }
            string description = CreateAnimalDescription(animal);
            Journal journal = new Journal(userId, animalId, description, TableNames.Животные, JournalActionType.Add);
            _repository.Add(journal);
        }

        public (List<Journal>, int) GetJournals(string filterValue, string filterField, int pageNumber, int pageSize, TableNames tableName)
        {
            var filterRequest = filter[filterField];
            var journals = _repository.db.Journals
                                         .Include(x => x.User)
                                            .ThenInclude(x => x.Organisation)
                                         .Where(x => x.TableName == tableName)
                                         .ToList();
            journals = journals.Where(x => filterRequest.Invoke(x, filterValue)).ToList();
            var count = journals.Count();
            var pagJournals = journals.Skip((pageNumber-1) * pageSize).Take(pageSize).ToList();
            return (pagJournals, count);
        }
        public void JournalEditAnimal(int userId, int animalId)
        {
            Animal animal = _animalService.GetEntry(animalId);
            if (animal == null)
            {
                throw new Exception("Данного животного не существует");
            }
            string description = CreateAnimalDescription(animal);
            Journal journal = new Journal(userId, animalId, description, TableNames.Животные, JournalActionType.Edit);
            _repository.Add(journal);
        }

        public void JournalDeleteAnimal(int userId, int animalId)
        {
            Animal animal = _animalService.GetEntry(animalId);
            if (animal == null)
            {
                throw new Exception("Данного животного не существует");
            }
            string description = CreateAnimalDescription(animal);
            Journal journal = new Journal(userId, animalId, description, TableNames.Животные, JournalActionType.Delete);
            _repository.Add(journal);
        }
        private string CreateAnimalDescription(Animal animal)
        {
            string description = "";
            description += animal.RegistrationNumber + ";";
            description += animal.AnimalName + ";";
            description += animal.YearOfBirth + ";";
            description += animal.AnimalCategory.NameAnimalCategory + ";";
            description += animal.Gender.NameGender + ";";
            return description;
        }

        public void JournalAddContract(int userId, int contractId)
        {
            Contract contract = _contractService.GetEntry(contractId);
            if (contract == null)
            {
                throw new Exception("Contract does not exist");
            }
            string description = CreateContractDescription(contract);
            Journal journal = new Journal(userId, contractId, description, TableNames.Контракты, JournalActionType.Add);
            _repository.Add(journal);
        }

        public void JournalEditContract(int userId, int contractId)
        {
            Contract contract = _contractService.GetEntry(contractId);
            if (contract == null)
            {
                throw new Exception("Contract does not exist");
            }
            string description = CreateContractDescription(contract);
            Journal journal = new Journal(userId, contractId, description, TableNames.Контракты, JournalActionType.Edit);
            _repository.Add(journal);
        }

        public void JournalDeleteContract(int userId, int contractId)
        {
            Contract contract = _contractService.GetEntry(contractId);
            if (contract == null)
            {
                throw new Exception("Contract does not exist");
            }
            string description = CreateContractDescription(contract);
            Journal journal = new Journal(userId, contractId, description, TableNames.Контракты, JournalActionType.Delete);
            _repository.Add(journal);
        }

        private string CreateContractDescription(Contract contract)
        {
            string localitiesDescription = contract.Localities != null
                ? string.Join(", ", contract.Localities.Select(l => $"Locality ID: {l.LocalityId}"))
                : "No localities";
            string description = $"Contract ID: {contract.IdContract}; " +
                                 $"Conclusion Date: {contract.ConclusionDate}; " +
                                 $"Expiration Date: {contract.ExpirationDate}; " +
                                 $"Performer ID: {contract.PerformerId}; " +
                                 $"Customer ID: {contract.CustomerId}; " +
                                 $"{localitiesDescription};";
            return description;
        }

        public void JournalAddOrganisation(int userId, int orgId)
        {
            Organisation org = _organisationService.GetEntry(orgId);
            if (org == null)
            {
                throw new Exception("Организация не существует");
            }
            string description = CreateOrganisationDescription(org);
            Journal journal = new Journal(userId, orgId, description, TableNames.Организации, JournalActionType.Add);
            _repository.Add(journal);
        }
        public void JournalEditOrganisation(int userId, int orgId)
        {
            Organisation org = _organisationService.GetEntry(orgId);
            if (org == null)
            {
                throw new Exception("Организация не существует");
            }
            string description = CreateOrganisationDescription(org);
            Journal journal = new Journal(userId, orgId, description, TableNames.Организации, JournalActionType.Edit);
            _repository.Add(journal);
        }

        public void JournalDeleteOrganisation(int userId, int orgId)
        {
            Organisation org = _organisationService.GetEntry(orgId);
            if (org == null)
            {
                throw new Exception("Организация не существует");
            }
            string description = CreateOrganisationDescription(org);
            Journal journal = new Journal(userId, orgId, description, TableNames.Организации, JournalActionType.Delete);
            _repository.Add(journal);
        }
        private string CreateOrganisationDescription(Organisation org)
        {
            string description = $"Org ID: {org.OrgId}; " +
                                 $"Org Name: {org.OrgName}; " +
                                 $"INN: {org.INN}; " +
                                 $"KPP: {org.KPP}; " +
                                 $"Address: {org.AdressReg}; " +
                                 $"Org Type: {org.OrgType?.NameOrgType}; " +
                                 $"Locality: {org.Locality?.NameLocality};";
            return description;
        }

    }
}
