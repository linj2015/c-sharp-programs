// In Package Manager, run:
// Install-Package Twilio.AspNet.Mvc -DependencyVersion HighestMinor

// Jokes from Reader's Digest https://www.rd.com/jokes/knock-knock/ and the Internet

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace KnockKnockJokes.Controllers
{
    public class SmsController : TwilioController
    {
        // enum to show conversation status
        enum Status
        {
            NONE, // user has not started the conversation
            PERSON, // user asked who's there
            ANSWER // user asked {person} who
        };

        // POST: Sms
        [HttpPost]
        public TwiMLResult Index()
        {
            Random random = new Random();

            //string from = Request.Form["From"];

            string requestBody = Request.Form["Body"];
            string responseString = "";
            var messagingResponse = new MessagingResponse();


            Status status;
            int jokeID;

            // get the session varible if it exists
            if (Session["jokeStatus"] != null && Session["jokeID"] != null)
            {
                status = (Status)Session["jokeStatus"];
                jokeID = (int)Session["jokeID"];
            }
            else
            {
                status = Status.NONE;
                jokeID = random.Next() % 20;
            }

            // make a collection of jokes
            var jokes = new Dictionary<int, Joke>()
            {
                {0, new Joke("Dozen", "Dozen anybody want to let me in?")},
                {1, new Joke("Robin", "Robin you—hand over the cash!")},
                {2, new Joke("Howl", "Howl you know if you don't open the door?")},
                {3, new Joke("Cash", "No thanks, I prefer peanuts.")},
                {4, new Joke("Art", "R2-D2, of course.")},
                {5, new Joke("Kanga", "Actually, it's kangaroo.")},
                {6, new Joke("Déja", "Knock! Knock!")},
                {7, new Joke("No one", "🤐 \u0001F910")},
                {8, new Joke("An extraterrestrial", "Wait–how many extraterrestrials do you know?")},
                {9, new Joke("Spell", "W-H-O")},
                {10, new Joke("Two knee", "Tunee fish!")},
                {11, new Joke("Loaf", "I don't just like bread, I loaf it.")},
                {12, new Joke("Tank", "You’re welcome.")},
                {13, new Joke("Mustache", "I mustache you a question, but I’ll shave it for later.")},
                {14, new Joke("Doctor", "He's on television.")},
                {15, new Joke("Yah", "No, I prefer google.")},
                {16, new Joke("Wendy", "Wendy bell works again I won’t have to knock anymore.")},
                {17, new Joke("Luke", "Luke through the keyhole and you’ll see!")},
                {18, new Joke("Mary", "Mary Christmas!")},
                {19, new Joke("Wanda", "Wanda where I put my car keys.")},
            };

            // generate response
            if (requestBody.Contains("joke"))
            {
                responseString = "Knock knock";
                status = Status.PERSON;
                jokeID = random.Next() % 20;
            }
            else if (status == Status.PERSON && requestBody.Contains("Who's there"))
            {
                responseString = jokes[jokeID].Person + ".";
                status = Status.ANSWER;
            }
            else if (status == Status.ANSWER && requestBody.Contains(jokes[jokeID].Person + " who"))
            {
                responseString = jokes[jokeID].Answer;
                Session["jokeStatus"] = null;
                Session["jokeID"] = null;
            }
            else
            {
                responseString = "I don't understand what you said, but I know a great knock-knock joke.";
                Session["jokeStatus"] = null;
                Session["jokeID"] = null;
                messagingResponse.Message(responseString);
                return TwiML(messagingResponse);
            }

            // save the session variables
            Session["jokeStatus"] = status;
            Session["jokeID"] = jokeID;

            //// make an associative array of senders we know, indexed by phone number
            //var people = new Dictionary<string, string>()
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
            //response.Message($"{name} has messaged {to} {count} times");

            messagingResponse.Message(responseString);
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
    //public class Conversation
    //{
        // The phone number that asked for the joke
        //string From { get; set; }

        // The time the conversation was last active + 4 hrs. 
        // (Twilio cookies expire after 4 hrs of inactivity)
        //DateTime ExpireTime { get; set; }

        // The line number that the joke is currently on
        //int jokeStatus { get; set; }

        // Which joke from the collection
        //int JokeID { get; set; }
    //}
}