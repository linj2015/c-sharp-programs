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
        // POST: Sms
        [HttpPost]
        public TwiMLResult Index()
        {
            string requestBody = Request.Form["Body"];

            // using Dictionary to pass in session variables
            // because the get/set syntax is the same as HttpSessionStateBase
            Dictionary<string, Object> sessionVars = new Dictionary<string, Object>();
            sessionVars["jokeStatus"] = Session["jokeStatus"];
            sessionVars["jokeID"] = Session["jokeID"];

            KnockKnockJokes program = new KnockKnockJokes();
            string responseString = program.ProcessRequest(requestBody, ref sessionVars);

            Session["jokeStatus"] = sessionVars["jokeStatus"];
            Session["jokeID"] = sessionVars["jokeID"];

            var messagingResponse = new MessagingResponse();
            messagingResponse.Message(responseString);
            return TwiML(messagingResponse);
        }
    }
}

namespace KnockKnockJokes
{
    public struct Joke
    {
        public string Setup { get; }
        public string Punchline { get; } 

        public Joke(string setup, string punchline)
        {
            Setup = setup;
            Punchline = punchline;
        }
    }

    public class KnockKnockJokes
    {
        private Dictionary<int, Joke> jokes;

        /// <summary>
        /// enum to show conversation status.
        /// </summary>
        enum Status
        {
            /// <summary>
            /// User has not started the conversation.
            /// </summary>
            NONE,
            /// <summary>
            /// User has asked who's there.
            /// </summary>
            SETUP,
            /// <summary>
            /// User has asked {person} who.
            /// </summary>
            PUNCHLINE
        };

        public KnockKnockJokes()
        {
            // zipper-mouth face emoji
            string strZipperMouthFace = ""
            + (char)int.Parse("D83EDD10".Substring(0, 4), System.Globalization.NumberStyles.HexNumber)
            + (char)int.Parse("D83EDD10".Substring(4, 4), System.Globalization.NumberStyles.HexNumber);

            jokes = new Dictionary<int, Joke>()
            {
                {0, new Joke("Dozen", "Dozen anybody want to let me in?")},
                {1, new Joke("Robin", "Robin you—hand over the cash!")},
                {2, new Joke("Howl", "Howl you know if you don't open the door?")},
                {3, new Joke("Cash", "No thanks, I prefer peanuts.")},
                {4, new Joke("Art", "R2-D2, of course.")},
                {5, new Joke("Kanga", "Actually, it's kangaroo.")},
                {6, new Joke("Déja", "Knock! Knock!")},
                {7, new Joke("No one", strZipperMouthFace)},
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
        }

        public string ProcessRequest(string requestBody, ref Dictionary<string, Object> Session)
        {
            Random random = new Random();

            string responseString = "";
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

            // generate response
            if (requestBody.Contains("joke"))
            {
                // user can ask for a new joke at any point in the conversation
                responseString = "Knock knock";
                status = Status.SETUP;
                jokeID = random.Next() % 20;
            }
            else if (status == Status.SETUP)
            {
                if (requestBody.Contains("Who's there"))
                {
                    responseString = jokes[jokeID].Setup + ".";
                    status = Status.PUNCHLINE;
                }
                else
                {
                    // trying to help the user...
                    responseString = "Try asking me \"Who's there?\"";
                }
            }
            else if (status == Status.PUNCHLINE)
            {
                if (requestBody.Contains(jokes[jokeID].Setup + " who"))
                {
                    responseString = jokes[jokeID].Punchline;
                    Session["jokeStatus"] = null;
                    Session["jokeID"] = null;
                    return responseString;
                }
                else
                {
                    // trying to help the user...
                    responseString = $"Try asking me \"{jokes[jokeID].Setup} who?\"";
                }
            }
            else
            {
                responseString = "I don't understand what you said, but I know some great knock-knock jokes.";
                Session["jokeStatus"] = null;
                Session["jokeID"] = null;
                return responseString;
            }
            
            // save the session variables
            Session["jokeStatus"] = status;
            Session["jokeID"] = jokeID;

            return responseString;
        }
    }

    /// <summary>
    /// Class for a knock-knock joke conversation. (Hopefully) can be stored in a cookie.
    /// </summary>
    //public class Conversation
    //{
    //    The phone number that asked for the joke
    //    string From { get; set; }

    //    The time the conversation was last active + 4 hrs.
    //     (Twilio cookies expire after 4 hrs of inactivity)
    //    DateTime ExpireTime { get; set; }

    //    The line number that the joke is currently on
    //    int jokeStatus { get; set; }

    //    Which joke from the collection
    //    int JokeID { get; set; }
    //}
}