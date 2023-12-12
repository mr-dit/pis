using Microsoft.EntityFrameworkCore;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;

namespace pis_web_api.Services
{
    public class JournalsService<T> where T : class, IJurnable
    {
        private Repository<Journal> _repository;

        private Dictionary<string, Func<Journal, string, bool>> filter = new Dictionary<string, Func<Journal, string, bool>>
            (StringComparer.InvariantCultureIgnoreCase)
        {
            [""] = (journal, filterValue) => true,
            ["fio"] = (journal, filterValue) => (journal.User.LastName + " " + journal.User.FirstName + " " + journal.User.Surname)
                                            .Contains(filterValue, StringComparison.InvariantCultureIgnoreCase),
            ["orgName"] = (journal, filterValue) => journal.User.Organisation.OrgName.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase),
            ["userLogin"] = (journal, filterValue) => journal.User.Login.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase),
            ["idObject"] = (journal, filterValue) => journal.EditID.ToString() == filterValue,
            ["descObject"] = (journal, filterValue) => journal.DescriptionObject.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase)
        };

        public JournalsService()
        {
            _repository = new Repository<Journal>();
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

        public void JournalCreate(int userId, T entity, JournalActionType type)
        {
            if (entity == null)
            {
                throw new Exception("Данного объекта не существует");
            }
            Journal journal = new Journal(userId, entity.Id, entity.ToString(), T.TableName, type);
            _repository.Add(journal);
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
