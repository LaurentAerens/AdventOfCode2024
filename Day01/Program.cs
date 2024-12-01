namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Times are added after the score was submitted
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // chalenge 1
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            string[] lines = File.ReadAllLines("input.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    left.Add(int.Parse(parts[0].Trim()));
                    right.Add(int.Parse(parts[1].Trim()));
                }
                else
                {
                    Console.WriteLine($"Invalid line format: {line}");
                }
            }
            // now sort them both from smallest to largest
            left.Sort();
            right.Sort();

            List<int> difference = new List<int>();
            for (int i = 0; i < left.Count; i++)
            {
                difference.Add(Math.Abs(right[i] - left[i]));
            }
            // count up all numbers in the difference list
            int sum = difference.Sum();
            Console.WriteLine(sum);
            
            watch.Stop();
            double elapsedns1 = watch.Elapsed.TotalNanoseconds;
            Console.WriteLine($"Execution Time: {elapsedns1} ns");

            watch.Restart();

            // challenge 2
            List<int> multlipication = new List<int>();
            foreach (int number in left)
            {
                int count = right.Count(x => x == number);
                multlipication.Add(number * count);
            }
            int sum2 = multlipication.Sum();
            Console.WriteLine(sum2);
            watch.Stop();
            double elapsedns2 = watch.Elapsed.TotalNanoseconds;
            Console.WriteLine($"Execution Time: {elapsedns2} ns");
            double total = elapsedns1 + elapsedns2;
            Console.WriteLine($"Total Execution Time: {total} ns");
        }
    }
}
