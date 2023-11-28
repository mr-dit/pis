using System.ComponentModel.DataAnnotations;

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
        public string TableName { get; set; }


        public Journal(int userID, int editID, string descriptionObject, string tableName)
        {
            UserID = userID;
            EditID = editID;
            DescriptionObject = descriptionObject;
            TableName = tableName;
        }
    }
}
