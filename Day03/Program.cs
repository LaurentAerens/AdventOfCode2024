using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Challenge 1
            string[] lines = File.ReadAllLines("input.txt");
            int sum = 0;
            foreach (string line in lines)
            {
                // There is a bunch of garbage in the string, only keep anything that is mul(number1, number2)
                string pattern = @"mul\((\d+),\s*(\d+)\)";
                Regex regex = new Regex(pattern);
                MatchCollection matches = regex.Matches(line);

                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Groups[1].Value);
                    int number1 = int.Parse(match.Groups[1].Value);
                    int number2 = int.Parse(match.Groups[2].Value);
                    sum += number1 * number2;
                }
            }
            Console.WriteLine(sum);

            Console.WriteLine("Challenge 2");

            // Challenge 2
            string input = File.ReadAllText("input.txt");
            sum = 0;

            // Split the input based on "do()" and "don't()"
            string[] parts = Regex.Split(input, @"(?=\bdo\(\)|\bdon't\(\))");
            bool first = true;
            foreach (string part in parts){
                // if part starts with "do" then we add the numbers
                Console.WriteLine(part);
                if (part.Contains("do()") || first)
                {
                    string pattern = @"mul\((\d+),\s*(\d+)\)";
                    Regex regex = new Regex(pattern);
                    MatchCollection matches = regex.Matches(part);

                    foreach (Match match in matches)
                    {
                        Console.WriteLine(match.Groups[1].Value);
                        int number1 = int.Parse(match.Groups[1].Value);
                        int number2 = int.Parse(match.Groups[2].Value);
                        sum += number1 * number2;
                    }
                }
                first = false;
                Console.WriteLine(sum);
            }
            Console.WriteLine(sum);
        }
    }
}