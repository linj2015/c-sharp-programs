// In Package Manager, run:
// Install-Package Twilio.AspNet.Mvc -DependencyVersion HighestMinor

using System;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace KnockKnockJokes.Controllers
{
    public class SmsController : TwilioController
    {
        // POST: Sms
        [HttpPost]
        public TwiMLResult Index()
        {

            //string from = Request.Form["From"];

            string requestBody = Request.Form["Body"];
            string responseString = "";
            var messagingResponse = new MessagingResponse();


            int? lineNumber = 0;

            // get the session varible if it exists
            if (Session["lineNumber"] != null)
            {
                lineNumber = (int)Session["lineNumber"];
            }
            //else // started a new conversation
            //{
            //    if (!requestBody.Contains("joke"))
            //    {
            //        responseString = "I don't understand what you said, but I know a great knock-knock joke.";
            //        messagingResponse.Message(responseString);
            //        return TwiML(messagingResponse);
            //    }
            //}

            Joke theOnlyJoke = new Joke("Dozen", "Dozen anybody want to let me in?");

            if (lineNumber == 0 && requestBody.Contains("joke"))
            {
                responseString = "Knock knock";
                lineNumber = 2;
            }
            else if (lineNumber == 2 && requestBody.Contains("Who's there"))
            {
                responseString = theOnlyJoke.Person;
                lineNumber = 4;
            }
            else if (lineNumber == 4 && requestBody.Contains(theOnlyJoke.Person + " who"))
            {
                responseString = theOnlyJoke.Answer;
                lineNumber = null;
            }
            else
            {
                responseString = "I don't understand what you said, but I know a great knock-knock joke.";
                Session["lineNumber"] = null;
                messagingResponse.Message("\n" + responseString);
                return TwiML(messagingResponse);
            }


            //// increment it
            //lineNumber++;

            // save it
            Session["lineNumber"] = lineNumber;

            //    // make an associative array of senders we know, indexed by phone number
            //    var people = new System.Collections.Generic.Dictionary<string, string>()
            //{
            //    {"+14158675308", "Rey"},
            //    {"+14158675310", "Finn"},
            //    {"+14158675311", "Chewy"}
            //};



            //// if the sender is known, then greet them by name
            //var name = "Friend";
            //var from = Request.Form["From"];
            //var to = Request.Form["To"];
            //if (people.ContainsKey(from))
            //{
            //    name = people[from];
            //}

            //var response = new MessagingResponse();
            //response.Message($"{name} has messaged {to} {lineNumber} times");

            messagingResponse.Message("\n" + responseString);
            return TwiML(messagingResponse);
        }
    }
}

namespace KnockKnockJokes
{
    public struct Joke
    {
        public string Person { get; }
        public string Answer { get; } 

        public Joke(string person, string answer)
        {
            Person = person;
            Answer = answer;
        }
    }

    /// <summary>
    /// Class for a knock-knock joke conversation. (Hopefully) can be stored in a cookie.
    /// </summary>
    public class Conversation
    {
        // The phone number that asked for the joke
        //string From { get; set; }

        // The time the conversation was last active + 4 hrs. 
        // (Twilio cookies expire after 4 hrs of inactivity)
        //DateTime ExpireTime { get; set; }

        // The line number that the joke is currently on
        //int LineNumber { get; set; }

        // Which joke from the collection
        //int JokeID { get; set; }
    }
}