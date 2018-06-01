// Namespace for birthday calculation methods

using System;

namespace ProjectBirthday
{
    public class Birthday
    {
        public static string ProcessRequest(string birthdayString)
        {
            const float longestLivedAge = 122F + 164 / 365F; // 122 yrs 164 days

            DateTime birthday = new DateTime();
            string messageString = "";
            float age;
            int daysUntilNextBirthday;

            // Check input date format
            if (!DateTime.TryParse(birthdayString, out birthday))
            {
                return "You've entered an incorrect date format. Please enter the date in mm/dd/yyyy.";
            }

            // Check date range
            if (birthday > DateTime.Today)
            {
                return "The birthday you've entered is in the future! Please enter a valid birthday.";
            }

            // Calculate
            BirthdayCalculations(birthday, out age, out daysUntilNextBirthday);

            // Check age range
            if (age > longestLivedAge)
            {
                return "You shouldn't be older than the oldest person ever lived. Please enter a valid birthday.";
            }

            // Output
            messageString += String.Format("Your Age: {0:F3} years old.\n", age);
            messageString += String.Format("There are {0} days until your next birthday! ", daysUntilNextBirthday);
            if (daysUntilNextBirthday == 0)
            {
                messageString += "Happy birthday!";
            }

            return messageString;
        }

        static void BirthdayCalculations(DateTime birthday, out float age, out int daysUntilNextBirthday)
        {
            // Calculate age
            age = (DateTime.Today - birthday).Days / 365F;

            // Calculate days until your next birthday
            DateTime nextBirthday = new DateTime(DateTime.Today.Year, birthday.Month, birthday.Day);
            if (nextBirthday < DateTime.Today) // Check if your birthday this year has passed already
            {
                nextBirthday.AddYears(1);
            }
            TimeSpan timeSpan = nextBirthday - DateTime.Today;
            daysUntilNextBirthday = timeSpan.Days;
        }
    }
}
