using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.Utils
{
    public class ServiceResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<Error> ResponseErrors { get; set; }

        public ServiceResponse(string responseCode, string responseMessage, List<Error> responseErrors)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            ResponseErrors = responseErrors;
        }
    }
}
