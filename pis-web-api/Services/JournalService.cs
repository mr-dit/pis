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
        }

        public void JournalAddAnimal(int userId, int animalId)
        {
            Animal animal = _animalService.GetEntry(animalId);
            if (animal == null)
            {
                throw new Exception("Данного животного не существует");
            }
            string desctription = "";
            desctription += animal.RegistrationNumber + ";";
            desctription += animal.AnimalName + ";";
            desctription += animal.YearOfBirth + ";";
            desctription += animal.AnimalCategory.NameAnimalCategory + ";";
            desctription += animal.Gender.NameGender + ";";
            Journal journal = new Journal(userId, animalId, desctription, TableNames.Животные, JournalActionType.Add);
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
    }
}
