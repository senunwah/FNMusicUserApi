using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.ServiceResponses
{
    public class ConfirmEmailResponse
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public bool AccountConfirmed { get; set; }
        public List<ServiceResponse> ServiceResponse { get; set; }

        public ConfirmEmailResponse(string username, string emailAddress, bool accountConfirmed, List<ServiceResponse> serviceResponse)
        {
            Username = username;
            EmailAddress = emailAddress;
            AccountConfirmed = accountConfirmed;
            ServiceResponse = serviceResponse;
        }
    }
}
