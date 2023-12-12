namespace pis_web_api.Models.db
{
    public interface IJurnable
    {
        public int Id { get; }
        virtual public static TableNames TableName { get; }
    }
}
