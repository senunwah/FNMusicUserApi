using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.Models
{
    public class LockAccountRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
