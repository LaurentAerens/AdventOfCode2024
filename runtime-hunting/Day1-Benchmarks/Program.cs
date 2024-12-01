using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Threading.Tasks;

namespace Day1_Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // also add a fancy exporter with graphs and stuff
            var config = DefaultConfig.Instance
                .AddExporter(MarkdownExporter.GitHub)
                .AddExporter(HtmlExporter.Default)
                .AddExporter(CsvExporter.Default)
                .AddExporter(JsonExporter.BriefCompressed)
                .AddExporter(RPlotExporter.Default);
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
    [SimpleJob(RuntimeMoniker.Net90)]
    // html, markdown, json and csv are the default exporters
    [HtmlExporter]
    [MarkdownExporterAttribute.GitHub]
    [CsvExporter]
    [JsonExporterAttribute.BriefCompressed]
    [RPlotExporter]
    public class Day1Benchmarks
    {
        [Benchmark]
        public void InitalSolution()
        {
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
            // now sort them both from smallest to largest do both in parallel
            Parallel.Invoke(
                () => left.Sort(),
                () => right.Sort()
            );

            List<int> difference = new List<int>();
            for (int i = 0; i < left.Count; i++)
            {
                difference.Add(Math.Abs(right[i] - left[i]));
            }
            // count up all numbers in the difference list
            int sum = difference.Sum();
            if (sum != 1879048) throw new Exception("Wrong answer");

            // challenge 2
            List<int> multlipication = new List<int>();
            foreach (int number in left)
            {
                int count = right.Count(x => x == number);
                multlipication.Add(number * count);
            }
            int sum2 = multlipication.Sum();
            if (sum2 != 21024792) throw new Exception("Wrong answer");
        }
        [Benchmark]
        /// For this improvement I focused on the second challenge, which is to multiply the number of times a number appears in the right list by the number itself. i'm using a stack to halve my runtime.
        public void AddedStackSolution()
        {
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
            if (sum != 1879048) throw new Exception("Wrong answer");
            // challenge 2
            // convert right to a stack with the first element on top
            Stack<int> rightStack = new Stack<int>(right.Reverse<int>());
            List<int> multlipication = new List<int>();
            for (int i = 0; i < left.Count; i++)
            {
                int leftnumber = left[i];
                int count = 0;
                while (rightStack.Count > 0)
                {
                    int rightnumber = rightStack.Pop();
                    if (rightnumber == leftnumber)
                    {
                        count++;
                    }
                    else if (rightnumber < leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                    }
                    else if (rightnumber > leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                        rightStack.Push(rightnumber);
                        break;
                    }
                }
                multlipication.Add(leftnumber * count);
            }
            int sum2 = multlipication.Sum();
            if (sum2 != 21024792) throw new Exception("Wrong answer");
        }
        [Benchmark]
        /// For this improvement I focussed doing the sorts in parallel. this ends up being a slight bit slower than the previous solution.
        public void ParallelSortSolution()
        {
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
            if (sum != 1879048) throw new Exception("Wrong answer");
            // challenge 2
            // convert right to a stack with the first element on top
            Stack<int> rightStack = new Stack<int>(right.Reverse<int>());
            List<int> multlipication = new List<int>();
            for (int i = 0; i < left.Count; i++)
            {
                int leftnumber = left[i];
                int count = 0;
                while (rightStack.Count > 0)
                {
                    int rightnumber = rightStack.Pop();
                    if (rightnumber == leftnumber)
                    {
                        count++;
                    }
                    else if (rightnumber < leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                    }
                    else if (rightnumber > leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                        rightStack.Push(rightnumber);
                        break;
                    }
                }
                multlipication.Add(leftnumber * count);
            }
            int sum2 = multlipication.Sum();
            if (sum2 != 21024792) throw new Exception("Wrong answer");
        }
        [Benchmark]
        /// go to arrays instead of lists
        public void ArraySolution()
        {
            // chalenge 1
            string[] lines = File.ReadAllLines("input.txt");
            // count the number of lines
            int lineCount = lines.Length;
            // create two arrays to store the left and right numbers
            int[] left = new int[lineCount];
            int[] right = new int[lineCount];
            for (int i = 0; i < lineCount; i++)
            {
                string[] parts = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    left[i] = int.Parse(parts[0].Trim());
                    right[i] = int.Parse(parts[1].Trim());
                }
                else
                {
                    Console.WriteLine($"Invalid line format: {lines[i]}");
                }
            }
            // now sort them both from smallest to largest
            Array.Sort(left);
            Array.Sort(right);
            List<int> difference = new List<int>();
            for (int i = 0; i < lineCount; i++)
            {
                difference.Add(Math.Abs(right[i] - left[i]));
            }
            // count up all numbers in the difference list
            int sum = difference.Sum();
            if (sum != 1879048) throw new Exception("Wrong answer");
            // challenge 2
            // convert right to a stack with the first element on top
            Stack<int> rightStack = new Stack<int>(right.Reverse<int>());
            List<int> multlipication = new List<int>();
            for (int i = 0; i < lineCount; i++)
            {
                int leftnumber = left[i];
                int count = 0;
                while (rightStack.Count > 0)
                {
                    int rightnumber = rightStack.Pop();
                    if (rightnumber == leftnumber)
                    {
                        count++;
                    }
                    else if (rightnumber < leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                    }
                    else if (rightnumber > leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                        rightStack.Push(rightnumber);
                        break;
                    }
                }
                multlipication.Add(leftnumber * count);
            }
            int sum2 = multlipication.Sum();
            if (sum2 != 21024792) throw new Exception("Wrong answer");
        }
        [Benchmark]
        public void BetterSplitInputSolution()
        {
            // chalenge 1
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
            // now sort them both from smallest to largest
            Array.Sort(left);
            Array.Sort(right);
            List<int> difference = new List<int>();
            for (int i = 0; i < lineCount; i++)
            {
                difference.Add(Math.Abs(right[i] - left[i]));
            }
            // count up all numbers in the difference list
            int sum = difference.Sum();
            if (sum != 1879048) throw new Exception("Wrong answer");
            // challenge 2
            // convert right to a stack with the first element on top
            Stack<int> rightStack = new Stack<int>(right.Reverse<int>());
            List<int> multlipication = new List<int>();
            for (int i = 0; i < lineCount; i++)
            {
                int leftnumber = left[i];
                int count = 0;
                while (rightStack.Count > 0)
                {
                    int rightnumber = rightStack.Pop();
                    if (rightnumber == leftnumber)
                    {
                        count++;
                    }
                    else if (rightnumber < leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                    }
                    else if (rightnumber > leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                        rightStack.Push(rightnumber);
                        break;
                    }
                }
                multlipication.Add(leftnumber * count);
            }
            int sum2 = multlipication.Sum();
            if (sum2 != 21024792) throw new Exception("Wrong answer");
        }
        [Benchmark]
        public void FirstChallegeAddupDirectlySolution()
        {
            // chalenge 1
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
            // now sort them both from smallest to largest
            Array.Sort(left);
            Array.Sort(right);
            int difference = 0;
            for (int i = 0; i < lineCount; i++)
            {
                difference += (Math.Abs(right[i] - left[i]));
            }
            // count up all numbers in the difference list
            if (difference != 1879048) throw new Exception("Wrong answer");
            // challenge 2
            // convert right to a stack with the first element on top
            Stack<int> rightStack = new Stack<int>(right.Reverse<int>());
            List<int> multlipication = new List<int>();
            for (int i = 0; i < lineCount; i++)
            {
                int leftnumber = left[i];
                int count = 0;
                while (rightStack.Count > 0)
                {
                    int rightnumber = rightStack.Pop();
                    if (rightnumber == leftnumber)
                    {
                        count++;
                    }
                    else if (rightnumber < leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                    }
                    else if (rightnumber > leftnumber)
                    {
                        multlipication.Add(leftnumber * count);
                        count = 0;
                        rightStack.Push(rightnumber);
                        break;
                    }
                }
                multlipication.Add(leftnumber * count);
            }
            int sum2 = multlipication.Sum();
            if (sum2 != 21024792) throw new Exception("Wrong answer");
        }
        [Benchmark]
        public void IfEqualsAssLastSolution()
        {
            // chalenge 1
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
            // now sort them both from smallest to largest
            Array.Sort(left);
            Array.Sort(right);
            int difference = 0;
            for (int i = 0; i < lineCount; i++)
            {
                difference += (Math.Abs(right[i] - left[i]));
            }
            // count up all numbers in the difference list
            if (difference != 1879048) throw new Exception("Wrong answer");
            // challenge 2
            // convert right to a stack with the first element on top
            Stack<int> rightStack = new Stack<int>(right.Reverse<int>());
            int multlipication = 0;
            for (int i = 0; i < lineCount; i++)
            {
                int leftnumber = left[i];
                int count = 0;
                while (rightStack.Count > 0)
                {
                    int rightnumber = rightStack.Pop();
                    if (rightnumber < leftnumber)
                    {
                        multlipication += (leftnumber * count);
                        count = 0;
                    }
                    else if (rightnumber > leftnumber)
                    {
                        multlipication += (leftnumber * count);
                        count = 0;
                        rightStack.Push(rightnumber);
                        break;
                    }
                    else if (rightnumber == leftnumber)
                    {
                        count++;
                    }
                }
                multlipication += (leftnumber * count);
            }
            if (multlipication != 21024792) throw new Exception("Wrong answer");
        }
        [Benchmark]
        public void ReducesStackOperationsSolution()
        {
            // chalenge 1
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
            // now sort them both from smallest to largest
            Array.Sort(left);
            Array.Sort(right);
            int difference = 0;
            for (int i = 0; i < lineCount; i++)
            {
                difference += (Math.Abs(right[i] - left[i]));
            }
            // count up all numbers in the difference list
            if (difference != 1879048) throw new Exception("Wrong answer");
            // challenge 2
            // convert right to a stack with the first element on top
            Stack<int> rightStack = new Stack<int>(right.Reverse<int>());
            int multlipication = 0;
            int rightnumber = rightStack.Pop();
            for (int i = 0; i < lineCount; i++)
            {
                int leftnumber = left[i];
                int count = 0;
                while (rightStack.Count > 0)
                {
                    if (rightnumber < leftnumber)
                    {
                        multlipication += (leftnumber * count);
                        count = 0;
                        rightnumber = rightStack.Pop();
                    }
                    else if (rightnumber > leftnumber)
                    {
                        multlipication += (leftnumber * count);
                        count = 0;
                        break;
                    }
                    else if (rightnumber == leftnumber)
                    {
                        count++;
                        rightnumber = rightStack.Pop();
                    }
                }
                multlipication += (leftnumber * count);
            }
            if (multlipication != 21024792) throw new Exception("Wrong answer");
        }
        [Benchmark]
        public void StreamingInputSolution()
        {
            // chalenge 1
            // I know there are 1000 lines in the input file
            int[] left = new int[1000];
            int[] right = new int[1000];
            int lineCount = 0;
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(new[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
                    left[lineCount] = int.Parse(parts[0]);
                    right[lineCount] = int.Parse(parts[1]);
                    lineCount++;
                }
            }
            // now sort them both from smallest to largest
            Array.Sort(left);
            Array.Sort(right);
            int difference = 0;
            for (int i = 0; i < lineCount; i++)
            {
                difference += (Math.Abs(right[i] - left[i]));
            }
            // count up all numbers in the difference list
            if (difference != 1879048) throw new Exception("Wrong answer");
            // challenge 2
            // convert right to a stack with the first element on top
            Stack<int> rightStack = new Stack<int>(right.Reverse<int>());
            int multlipication = 0;
            int rightnumber = rightStack.Pop();
            for (int i = 0; i < lineCount; i++)
            {
                int leftnumber = left[i];
                int count = 0;
                while (rightStack.Count > 0)
                {
                    if (rightnumber < leftnumber)
                    {
                        multlipication += (leftnumber * count);
                        count = 0;
                        rightnumber = rightStack.Pop();
                    }
                    else if (rightnumber > leftnumber)
                    {
                        multlipication += (leftnumber * count);
                        count = 0;
                        break;
                    }
                    else if (rightnumber == leftnumber)
                    {
                        count++;
                        rightnumber = rightStack.Pop();
                    }
                }
                multlipication += (leftnumber * count);
            }
            if (multlipication != 21024792) throw new Exception("Wrong answer");
        }
    }
}
