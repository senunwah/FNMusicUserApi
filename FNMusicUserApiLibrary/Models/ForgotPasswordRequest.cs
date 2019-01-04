using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.Models
{
    public class ForgotPasswordRequest
    {
        public string Username { get; set; }
        public VerificationMedia Media {get;set; }
    }
}
