// Week 02
// Code sample for ASP.NET MVC on .NET Framework 4.6.1+
// Custom Responses to Incoming SMS Messages

using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using ProjectBirthday; // for birthday methods

namespace SimpleSMSWebapp.Controllers
{
    public class SmsController : TwilioController
    {
        // POST: SMS
        [HttpPost]
        public TwiMLResult Index()
        {
            string requestBody = Request.Form["Body"];
            string responseString = Birthday.ProcessRequest(requestBody);

            var messagingResponse = new MessagingResponse();
            messagingResponse.Message(responseString);
            return TwiML(messagingResponse);
        }
    }
}

