using System;
using System.Collections.Generic;
using System.Text;

namespace RES_QRCode.Models
{
    public class ResponseMessageModel
    {
        string responseMessage;

        bool responseStatus;
       
        public bool ResponseStatus { get => responseStatus; set => responseStatus = value; }

        public string ResponseMessage { get => responseMessage; set => responseMessage = value; }
    }
}
