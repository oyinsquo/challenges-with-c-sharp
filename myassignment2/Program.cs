// See https://aka.ms/new-console-template for more information

using System;
using System.Text.RegularExpressions;

namespace ReverseDateFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            // User input loop
            while (true)
            {
                Console.Write("Enter a date in the format mm/dd/yyyy: ");
                string input = Console.ReadLine();

                // Validate input
                if (!IsValidDate(input))
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    continue;
                }

                // Call the ReverseDateFormat method
                string reversedDate = ReverseDateFormat(input);

                Console.WriteLine($"Reversed date: {reversedDate}");
            }
        }

        static bool IsValidDate(string input)
        {
            // Use DateTime.TryParse to check if the input is a valid date
            DateTime date;
            return DateTime.TryParseExact(input, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out date);
        }

        static string ReverseDateFormat(string input)
        {
            // Regex pattern with named capturing groups
            string pattern = @"^ (?<mon>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})$";

            // Set a timeout for the regex operation
            TimeSpan timeout = TimeSpan.FromSeconds(1);

            try
            {
                // Use the regex pattern to match and replace the date format
                Match match = Regex.Match(input, pattern, timeout);
                if (match.Success)
                {
                    // Use the captured groups to format the date as yyyy-mm-dd
                    string year = match.Groups["year"].Value;
                    string mon = match.Groups["mon"].Value;
                    string day = match.Groups["day"].Value;

                    return $"{year}-{mon}-{day}";
                }
                else
                {
                    // Return the original input date if the regex operation times out
                    return input;
                }
            }
            catch (RegexMatchTimeoutException ex)
            {
                // Handle the RegexMatchTimeoutException
                Console.WriteLine("Regex operation timed out. Returning original input date.");
                return input;
            }
        }
    }
}    

 // The program uses a while loop to continuously prompt the user for input and call the ReverseDateFormat method.
 //The IsValidDate method uses DateTime.TryParseExact to check if the input is a valid date in the format mm / dd/vyyy.
//If the input is not a valid date, the program will prompt the user to try again.
//The ReverseDateFormat method uses a regular expression pattern with named capturing groups to match and replace the date format. The method also sets a timeout for the regex operation using a TimeSpan object.
//If the regex operation times out, the method catches the RegexMatchTimeoutException and returns the original input date.
//If the regex operation is successful, the method uses the captured groups to format the date as yyyy-mm-dd.


