using pis_web_api.Models.db;
using pis_web_api.Models.get;

namespace pis_web_api.Services
{
    public class JournalConverter
    {
        public JournalConverter() { }

        public List<JournalGet> ToGet(IEnumerable<Journal> journals)
        {
            var result = new List<JournalGet>();
            foreach (var journal in journals)
            {
                var journalGet = new JournalGet(journal);
                result.Add(journalGet);
            }
            return result;
        }
    }
}
