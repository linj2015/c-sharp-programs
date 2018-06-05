using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace ProjectBirthday.Tests
{
    [TestClass]
    public class BirthdayTests
    {
        const string expectedNormal = @"Your Age: \d+.\d+ years old.\nThere are \d+ days until your next birthday! ";

        [TestMethod]
        public void Normal_Birthday_Later_In_The_Year_Test()
        {
            // Arrange
            string input = "12/31/1996";
            string expected = expectedNormal;
            // Act
            string actual = Birthday.ProcessRequest(input);
            // Assert
            Assert.IsTrue(Regex.Match(actual, expected).Success);
        }

        [TestMethod]
        public void Normal_Birthday_Earlier_In_The_Year_Test()
        {
            // Arrange
            string input = "01/01/1996";
            string expected = expectedNormal;
            // Act
            string actual = Birthday.ProcessRequest(input);
            // Assert
            Assert.IsTrue(Regex.Match(actual, expected).Success);
        }

        [TestMethod]
        public void Birthday_Is_Today_Test()
        {
            // Arrange
            var date = System.DateTime.Today;
            date = date.AddYears(-22);
            string input = date.ToString("MM/dd/yyyy");
            string expected = expectedNormal + "Happy birthday!";
            // Act
            string actual = Birthday.ProcessRequest(input);
            // Assert
            Assert.IsTrue(Regex.Match(actual, expected).Success);
        }

        [TestMethod]
        public void Too_Old_Birthday_Test()
        {
            // Arrange
            string input = "01/01/1850";
            string expected = "You shouldn't be older than the oldest person ever lived. Please enter a valid birthday.";
            // Act
            string actual = Birthday.ProcessRequest(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Birthday_In_The_Future_Test()
        {
            // Arrange
            string input = "01/01/2028";
            string expected = "The birthday you've entered is in the future! Please enter a valid birthday.";
            // Act
            string actual = Birthday.ProcessRequest(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Incorrect_Date_Format_Test()
        {
            // Arrange
            string input = "436841";
            string expected = "You've entered an incorrect date format. Please enter the date in mm/dd/yyyy.";
            // Act
            string actual = Birthday.ProcessRequest(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Incorrect_Date_Format_Test_1()
        {
            // Arrange
            string input = "asdf";
            string expected = "You've entered an incorrect date format. Please enter the date in mm/dd/yyyy.";
            // Act
            string actual = Birthday.ProcessRequest(input);
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
