using pis_web_api.Models.db;

namespace pis_web_api.Models.get
{
    public class JournalGet
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public string OrgName { get; set; }
        public string UserLogin { get; set; }
        public string Date { get; set; }
        public int IdObject { get; set; }
        public string DescObject { get; set; }
        public string ActionType { get; set; }

        public JournalGet(Journal journal) 
        {
            ID = journal.JounalID;
            FIO = journal.User.Surname + " " + journal.User.FirstName + " " + journal.User.LastName;
            OrgName = journal.User.Organisation.OrgName;
            UserLogin = journal.User.Login;
            Date = journal.DateTime.ToString();
            IdObject = journal.EditID;
            DescObject = journal.DescriptionObject;
            ActionType = journal.ActionType.ToString();
        }
    }
}
