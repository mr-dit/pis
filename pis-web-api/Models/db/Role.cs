using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pis_web_api.Models.db
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string NameRole { get; set; }

        public List<UserRole>? UsersRole { get; set; }

        public Role(string nameRole)
        {
            NameRole = nameRole;
        }

        public Role() { }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Role)) return false;
            var compRole = (Role)obj;
            return IdRole == compRole.IdRole;
        }
    }
}
