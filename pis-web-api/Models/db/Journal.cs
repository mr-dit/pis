using System.ComponentModel.DataAnnotations;
using pis_web_api.References;

namespace pis_web_api.Models.db
{
    public class Journal
    {
        [Key]
        public int JounalID { get; set; } 
        public int UserID { get; set; }
        public User User { get; set; }
        public int EditID { get; set; }
        public string DescriptionObject { get; set; }
        public DateTime DateTime { get; set; }

        [Range(1, 3)]
        public TableNames TableName { get; set; }

        [Range(1, 3)]
        public JournalActionType ActionType { get; set; }


        public Journal(int userID, int editID, string descriptionObject, TableNames tableName, JournalActionType actionType)
        {
            UserID = userID;
            EditID = editID;
            DescriptionObject = descriptionObject;
            TableName = tableName;
            ActionType = actionType;
            DateTime = DateTime.Now;
        }
    }
}
