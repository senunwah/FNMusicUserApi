using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginRequest()
        {
        }

        public LoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
