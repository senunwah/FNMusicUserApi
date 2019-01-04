using FNMusicUserApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.ServiceResponses
{
    public class ForgotPasswordResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool LinkGenerated { get; set; }
        public string ResetPasswordLink { get; set; }
        public List<ServiceResponse> ServiceResponse { get; set; }

        public ForgotPasswordResponse(string username, string email, string phoneNumber, bool linkGenerated, string resetPasswordLink, List<ServiceResponse> serviceResponse)
        {
            Username = username;
            Email = email;
            PhoneNumber = phoneNumber;
            LinkGenerated = linkGenerated;
            ResetPasswordLink = resetPasswordLink;
            ServiceResponse = serviceResponse;
        }
    }
}
