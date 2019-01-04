using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.Models
{
    public class SignupRequest : LoginRequest
    {
        public string UserId = UserIdGenerator.RandomGen(60);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public UserGender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Location { get; set; }
        public long PhoneNumber { get; set; }
        public string PrimaryGenre { get; set; }
        public string Biography { get; set; }
        public string Website { get; set; }
        public string ProfileImagePath { get; set; }
        public string CoverImagePath { get; set; }
        public long Following { get; set; }
        public long Followers { get; set; }
        public AccountVerificationStatus VerifiedAccount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
