using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnockKnockJokes.Tests
{
    [TestClass]
    public class KnockKnockJokesTests
    {
        [TestMethod]
        public void Normal_Joke_State_0_Test()
        {
            // Arrange
            string input = "Tell me a knock-knock joke.";
            string expected = "Knock knock";
            var session = new Dictionary<string, Object>();
            session["jokeID"] = 0;
            session["jokeStatus"] = 0;
            // Act
            KnockKnockJokes program = new KnockKnockJokes();
            string actual = program.ProcessRequest(input, ref session);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Normal_Joke_State_1_Test()
        {
            // Arrange
            string input = "Who's there?";
            string expected = "Dozen.";
            var session = new Dictionary<string, Object>();
            session["jokeID"] = 0;
            session["jokeStatus"] = 1;
            // Act
            KnockKnockJokes program = new KnockKnockJokes();
            string actual = program.ProcessRequest(input, ref session);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Normal_Joke_State_2_Test()
        {
            // Arrange
            string input = "Dozen who?";
            string expected = "Dozen anybody want to let me in?";
            var session = new Dictionary<string, Object>();
            session["jokeID"] = 0;
            session["jokeStatus"] = 2;
            // Act
            KnockKnockJokes program = new KnockKnockJokes();
            string actual = program.ProcessRequest(input, ref session);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Normal_Joke_All_States_Test()
        {
            // Arrange
            var session = new Dictionary<string, Object>();
            session["jokeID"] = 9;
            session["jokeStatus"] = 0;
            // Act
            KnockKnockJokes program = new KnockKnockJokes();
            string actual = program.ProcessRequest("Tell me a knock-knock joke.", ref session);
            session["jokeID"] = 9;
            string actual1 = program.ProcessRequest("Who's there?", ref session);
            string actual2 = program.ProcessRequest("Spell who?", ref session);
            // Assert
            Assert.AreEqual("Knock knock", actual);
            Assert.AreEqual("Spell.", actual1);
            Assert.AreEqual("W-H-O", actual2);
        }

        // User should be able to ask for a new joke at any point in the conversation
        // Test may fail if Random happens to generate the same random number twice in a row
        [TestMethod]
        public void Ask_For_New_Joke_Test()
        {
            // Arrange
            var session = new Dictionary<string, Object>();
            session["jokeID"] = 9;
            session["jokeStatus"] = 0;
            // Act
            KnockKnockJokes program = new KnockKnockJokes();
            string actual = program.ProcessRequest("Tell me a knock-knock joke.", ref session);
            session["jokeID"] = 9;
            string actual1 = program.ProcessRequest("Who's there?", ref session);
            string actual2 = program.ProcessRequest("Tell me a knock-knock joke.", ref session);
            string actual3 = program.ProcessRequest("Who's there?", ref session);
            // Assert
            Assert.AreEqual("Knock knock", actual);
            Assert.AreEqual("Spell.", actual1);
            Assert.AreEqual("Knock knock", actual2);
            Assert.AreNotEqual("Spell.", actual3);
        }

        [TestMethod]
        public void Give_Hint_Test()
        {
            // Arrange
            var session = new Dictionary<string, Object>();
            session["jokeID"] = 9;
            session["jokeStatus"] = 0;
            // Act
            KnockKnockJokes program = new KnockKnockJokes();
            string actual = program.ProcessRequest("Tell me a knock-knock joke.", ref session);
            session["jokeID"] = 9;
            string actual1 = program.ProcessRequest("What do I do now?", ref session);
            string actual2 = program.ProcessRequest("Who's there?", ref session);
            // Assert
            Assert.AreEqual("Knock knock", actual);
            Assert.AreEqual("Try asking me \"Who's there?\"", actual1);
            Assert.AreEqual("Spell.", actual2);
        }
    }
}
