using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.ServiceResponses
{
    public class EditPhoneNoResponse 
    {
        public string PhoneNumber { get; set; }
        public bool PhoneNumberUpdated { get; set; }
        public List<ServiceResponse> ServiceResponse { get; set; }

        public EditPhoneNoResponse(string phoneNumber, bool phoneNumberUpdated, List<ServiceResponse> serviceResponse)
        {
            PhoneNumber = phoneNumber;
            PhoneNumberUpdated = phoneNumberUpdated;
            ServiceResponse = serviceResponse;
        }
    }
}
