using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.ServiceResponses
{
    public class LockAccountResponse
    {
        public string Username { get; set; }
        public bool AccountLocked { get; set; }
        public List<ServiceResponse> ServiceResponse;

        public LockAccountResponse(string username, bool isLocked, List<ServiceResponse> serviceResponse)
        {
            Username = username;
            this.AccountLocked = isLocked;
            ServiceResponse = serviceResponse;
        }
    }
}
