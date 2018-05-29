// Week 01

using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello!");

        float longestLivedAge = 122F + 164 / 365F; // 122 yrs 164 days
        float age;
        int daysUntilNextBirthday;
        DateTime birthday = new DateTime();

        while (true)
        {
            // Ask for birthday
            Console.Write("Enter your birthday (mm/dd/yyyy): ");
            string birthdayString = Console.ReadLine();

            // Check input date format
            if (!DateTime.TryParse(birthdayString, out birthday))
            {
                Console.WriteLine("You've entered an incorrect date format. Please enter the date in mm/dd/yyyy format.");
                continue;
            }

            // Calculate
            BirthdayCalculations(birthday, out age, out daysUntilNextBirthday);

            // Check age range
            if (birthday > DateTime.Today)
            {
                Console.WriteLine("The birthday you've entered is in the future! Please enter a valid birthday.");
                continue;
            }
            if (age > longestLivedAge)
            {
                Console.WriteLine("You shouldn't be older than the oldest person ever lived. Please enter a valid birthday.");
                continue;
            }
            break;
        }

        // Output
        Console.WriteLine("Your Age: {0:F3} years old", age);
        Console.WriteLine("There are {0} days until your next birthday!", daysUntilNextBirthday);
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

//public class Calculate
//{
//    public static int YourAge(DateTime birthday)
//    {
//        int age = DateTime.Today.Year - birthday.Year;

//        // Check if your birthday this year has passed already
//        DateTime birthdayThisYear = new DateTime(DateTime.Today.Year, birthday.Month, birthday.Day);
//        if (birthdayThisYear > DateTime.Today)
//        {
//            age--;
//        }

//        return age;
//    }

//    public static int DaysUntilBirthday(DateTime birthday)
//    {
//        // Check if your birthday this year has passed already
//        DateTime nextBirthday = new DateTime(DateTime.Today.Year, birthday.Month, birthday.Day);
//        if (nextBirthday < DateTime.Today)
//        {
//            nextBirthday.AddYears(1);
//        }

//        TimeSpan timeSpan = nextBirthday - DateTime.Today;
//        return timeSpan.Days;
//    }
//}
