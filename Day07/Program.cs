namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            double sum = 0;
            double sum2 = 0;
            foreach (string line in input)
            {
                string[] parts = line.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                double answer = double.Parse(parts[0]);
                string[] numbersString = parts[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                double[] numbers = numbersString.Select(double.Parse).ToArray();

                bool result = CheckCombinations(numbers, answer, 1);
                if (result)
                {
                    sum += answer;
                }
                bool result2 = CheckCombinations(numbers, answer, 2);
                if (result2)
                {
                    sum2 += answer;
                }
            }
            Console.WriteLine(sum);
            Console.WriteLine(sum2);

        }

        static bool CheckCombinations(double[] numbers, double target, int part)
        {
            Stack<(double current, int index)> stack = new Stack<(double current, int index)>();
            stack.Push((numbers[0], 1));

            while (stack.Count > 0)
            {
                var (current, index) = stack.Pop();

                if (index == numbers.Length)
                {
                    if (current == target)
                    {
                        return true;
                    }
                }
                else
                {
                    stack.Push((current + numbers[index], index + 1));
                    stack.Push((current * numbers[index], index + 1));
                    if (part == 2)
                    {
                      int power = numbers[index].ToString().Length;
                        stack.Push(((current * Math.Pow(10,power )) + numbers[index], index + 1));

                    }
                }
            }

            return false;
        }
    }
}
