using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.ServiceResponses
{
    public class SignupResponse
    {
        public bool SignedUp = false;
        public List<ServiceResponse> ServiceResponse { get; set; }

        public SignupResponse(bool signedUp, List<ServiceResponse> serviceResponse)
        {
            SignedUp = signedUp;
            ServiceResponse = serviceResponse;
        }
    }
}
