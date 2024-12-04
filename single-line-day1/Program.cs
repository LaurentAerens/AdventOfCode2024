namespace single_line_day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // the goal is to solve the challenge in a single line of code (excluding the using directives and the read, and like spliting in lists of the input)
            // read the input
            string[] lines = File.ReadAllLines("input.txt");
            // count the number of lines
            int lineCount = lines.Length;
            // create two arrays to store the left and right numbers
            int[] left = new int[lineCount];
            int[] right = new int[lineCount];
            for (int i = 0; i < lineCount; i++)
            {
                string[] parts = lines[i].Split(new[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    left[i] = int.Parse(parts[0]);
                    right[i] = int.Parse(parts[1]);
                }
                else
                {
                    Console.WriteLine($"Invalid line format: {lines[i]}");
                }
            }
            // challenge 1
            int sum = left.Zip(right, (l, r) => Math.Abs(l - r)).Sum();
            Console.WriteLine(sum);

        }
    }
}
