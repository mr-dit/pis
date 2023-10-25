using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pis.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string NameRole { get; set; }

        public List<UserRole>? UsersRole { get; set; }

        public Role (string nameRole)
        {
            NameRole = nameRole;
        }

        public Role() { }
    }
}
