using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.Models
{
    public class SigninResponse
    {
        public bool isLoggedIn = false;
        public List<ServiceResponse> ServiceResponse { get; set; }

        public SigninResponse(bool isLoggedIn, List<ServiceResponse> serviceResponse)
        {
            this.isLoggedIn = isLoggedIn;
            ServiceResponse = serviceResponse;
        }
    }
}
