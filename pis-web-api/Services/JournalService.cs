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

        public (List<Journal>, int) GetJournals(string filterValue, string filterField, int pageNumber, int pageSize, TableNames tableName)
        {
            var filterRequest = filter[filterField];
            var journals = _repository.db.Journals
                                         .Include(x => x.User)
                                            .ThenInclude(x => x.Organisation)
                                         .Where(x => x.TableName == tableName)
                                         .OrderBy(x => x.DateTime)
                                         .ToList();
            journals = journals.Where(x => filterRequest.Invoke(x, filterValue)).ToList();
            var count = journals.Count();
            var pagJournals = journals.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (pagJournals, count);
        }



        // Animal Journal
        public void JournalAddAnimal(int userId, int animalId)
        {
            Animal animal = _animalService.GetEntry(animalId);
            if (animal == null)
            {
                throw new Exception("Данного животного не существует");
            }
            string description = CreateAnimalDescription(animal);
            Journal journal = new Journal(userId, animalId, description, TableNames.Животные, JournalActionType.Добавить);
            _repository.Add(journal);
        }

        public void JournalEditAnimal(int userId, int animalId)
        {
            Animal animal = _animalService.GetEntry(animalId);
            if (animal == null)
            {
                throw new Exception("Данного животного не существует");
            }
            string description = CreateAnimalDescription(animal);
            Journal journal = new Journal(userId, animalId, description, TableNames.Животные, JournalActionType.Изменить);
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
            Journal journal = new Journal(userId, animalId, description, TableNames.Животные, JournalActionType.Удалить);
            _repository.Add(journal);
        }
        private string CreateAnimalDescription(Animal animal)
        {
            string description = "";
            description += animal.ElectronicChipNumber + ";";
            description += animal.AnimalName + ";";
            description += animal.YearOfBirth + ";";
            description += animal.AnimalCategory.NameAnimalCategory + ";";
            description += animal.Gender.NameGender + ";";
            description += animal.SpecialSigns + ";";
            description += animal.Locality.NameLocality + ";";
            return description;
        }




        // Contract Journal
        public void JournalAddContract(int userId, int contractId)
        {
            Contract contract = _contractService.GetEntry(contractId);
            if (contract == null)
            {
                throw new Exception("Contract does not exist");
            }
            string description = CreateContractDescription(contract);
            Journal journal = new Journal(userId, contractId, description, TableNames.Контракты, JournalActionType.Добавить);
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
            Journal journal = new Journal(userId, contractId, description, TableNames.Контракты, JournalActionType.Изменить);
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
            Journal journal = new Journal(userId, contractId, description, TableNames.Контракты, JournalActionType.Удалить);
            _repository.Add(journal);
        }

        private string CreateContractDescription(Contract contract)
        {
            string localitiesDescription = contract.Localities != null
                ? string.Join(", ", contract.Localities.Select(l => $"{l.LocalityId}:{l.Price}"))
                : "No localities";
            string description = $"{contract.ConclusionDate}; " +
                                 $"{contract.ExpirationDate}; " +
                                 $"{contract.PerformerId}; " +
                                 $"{contract.CustomerId}; " +
                                 $"{localitiesDescription};";
            return description;
        }




        // Organisation Journal
        public void JournalAddOrganisation(int userId, int orgId)
        {
            var org = _repository.db.Organisations
                                        .Where(x => x.OrgId == orgId)
                                        .Include(x => x.OrgType)
                                        .Include(x => x.Locality)
                                        .Single();
            if (org == null)
            {
                throw new Exception("Организация не существует");
            }
            string description = CreateOrganisationDescription(org);
            Journal journal = new Journal(userId, orgId, description, TableNames.Организации, JournalActionType.Добавить);
            _repository.Add(journal);
        }

        public void JournalEditOrganisation(int userId, int orgId)
        {
            var org = _repository.db.Organisations
                                        .Where(x => x.OrgId == orgId)
                                        .Include(x => x.OrgType)
                                        .Include(x => x.Locality)
                                        .Single();
            if (org == null)
            {
                throw new Exception("Организация не существует");
            }
            string description = CreateOrganisationDescription(org);
            Journal journal = new Journal(userId, orgId, description, TableNames.Организации, JournalActionType.Изменить);
            _repository.Add(journal);
        }

        public void JournalDeleteOrganisation(int userId, int orgId)
        {
            var org = _repository.db.Organisations
                                        .Where(x => x.OrgId == orgId)
                                        .Include(x => x.OrgType)
                                        .Include(x => x.Locality)
                                        .Single();
            if (org == null)
            {
                throw new Exception("Организация не существует");
            }
            string description = CreateOrganisationDescription(org);
            Journal journal = new Journal(userId, orgId, description, TableNames.Организации, JournalActionType.Удалить);
            _repository.Add(journal);
        }

        private string CreateOrganisationDescription(Organisation org)
        {
            string description = $"{org.OrgName}; " +
                                 $"{org.INN}; " +
                                 $"{org.KPP}; " +
                                 $"{org.AdressReg}; " +
                                 $"{org.OrgType?.NameOrgType}; " +
                                 $"{org.Locality?.NameLocality};";
            return description;
        }

        public bool Delete(int[] ids)
        {
            foreach (var id in ids)
            {
                var journal = _repository.db.Journals.Where(x => x.JounalID == id).Single();
                _repository.Remove(journal);
            }
            return true;
        }
    }
}
