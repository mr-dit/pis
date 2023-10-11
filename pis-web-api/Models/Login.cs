using System;
using pis_web_api.Services;
namespace pis_web_api.Models
{
	public class Login
	{
		public string Username { get; set; } = "";
		public string Password { get; set; } = "";

        public Login()
		{
		}
	}
}

