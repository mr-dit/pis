using System.ComponentModel.DataAnnotations;

namespace pis.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }

        public int PostId { get; set; }

        public Post? Post { get; set; }

        public int OrganisationId { get; set; }

        public Organisation? Organisation { get; set; }

        public User() { }

        public User(string surName, string firstName, string lastName, int postId, int organizationId)
        {
            Surname = surName;
            FirstName = firstName;
            LastName = lastName;
            PostId = postId;
            OrganisationId = organizationId;
        }
        //public User(int idUser, string firstName, string lastName, string surname, Post post, Organisation organisation, List<Vaccination> vaccinations)
        //{
        //    IdUser = idUser;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Surname = surname;
        //    Post = post;
        //    Organisation = organisation;
        //    Vaccinations = vaccinations;
        //}
    }
}
