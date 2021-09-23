using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            string name1, name2, sentence;
            bool isName1Correct = true;
            bool isName2Correct = true;
            do
            {
                Console.WriteLine("Enter your first name");
                name1 = Console.ReadLine();

                isName1Correct = IsAllLetters(name1);

                Console.WriteLine("Enter your second name");

                name2 = Console.ReadLine();

                isName2Correct = IsAllLetters(name2);

                if (!isName1Correct || !isName2Correct)
                    Console.WriteLine("One of the entered values contains non-alphabetic characters");
            } while (!isName1Correct || !isName2Correct);

            sentence = $"{name1} matches {name2}";

            var matchCount = CalculateMatches(sentence);
            var percentCount = CalculatePercentage(matchCount);
            DeclareOutput(percentCount, name1, name2);
        }

        public static bool IsAllLetters(string sentence)
        {
            foreach (var c in sentence)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }

        public static string CalculateMatches(string sentence)
        {
            var newString = "";
            sentence = sentence.ToLower();
            sentence = Regex.Replace(sentence, @" ", "");
            foreach (var letter in sentence)
            {
                newString += sentence.Count(a => a == letter);
                sentence = sentence.Replace(letter.ToString(), "");
            }
            //remove the zeros from the string
            newString = Regex.Replace(newString, @"0", "");
            return newString;
        }

        public static string CalculatePercentage(string numbers)
        {
            var newString = "";
            //process the string until there's either 1 or 0 chars left. remember the length starts from 0 not 1
            while (numbers.Length > 1)
            {
                //add the left most and the right most chars to a new string
                newString += int.Parse(numbers[0].ToString()) + int.Parse(numbers[numbers.Length - 1].ToString());
                //remove the first and the last numbers from the original string of numbers
                numbers = numbers.Substring(1, numbers.Length - 2);
            }
            //add the remaining characters to a new string
            newString += numbers;

            //should the new string be more than 2 chars it will run the method again. see that we are using recursion here
            if (newString.Length > 2)
            {

                return CalculatePercentage(newString);
            }
            else
            {
                return newString;
            }
        }

        public static void DeclareOutput(string percentage, string name, string otherName)
        {
            var p = Convert.ToInt32(percentage);
            if (p >= 80)
                Console.WriteLine($"{name} matches {otherName} {p}%, good match");
            else Console.WriteLine($"{name} matches {otherName} {p}%");
        }
    }
}
