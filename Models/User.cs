namespace pis.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public Post Post { get; set; }
        public Organisation Organisation { get; set; }

        public List<Vaccination> Vaccinations { get; set; }

        public User(int idUser, string firstName, string lastName, string surname, Post post, Organisation organisation, List<Vaccination> vaccinations)
        {
            IdUser = idUser;
            FirstName = firstName;
            LastName = lastName;
            Surname = surname;
            Post = post;
            Organisation = organisation;
            Vaccinations = vaccinations;
        }

    }
}
