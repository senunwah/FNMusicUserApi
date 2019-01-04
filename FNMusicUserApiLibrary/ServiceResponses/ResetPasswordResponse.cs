using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.ServiceResponses
{
    public class ResetPasswordResponse
    {
        public string Username { get; set; }
        public bool Reset { get; set; }
        public List<ServiceResponse> ServiceResponse { get; set; }

        public ResetPasswordResponse(bool reset, List<ServiceResponse> serviceResponse)
        {
            Reset = reset;
            ServiceResponse = serviceResponse;
        }
    }
}
