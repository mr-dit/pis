using System;
using pis.Services;

namespace pis_web_api.Models.db
{
    public class Login
    {
        public User User { get; set; }

        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public Login()
        {
        }
    }
}

