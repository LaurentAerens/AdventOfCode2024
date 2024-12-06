using System.Linq;

namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// I manually split the input into two files
            //string[] rules = File.ReadAllLines("input.txt");
            //string[] instructions = File.ReadAllLines("input2.txt");
            //Dictionary<int, List<int>> Orderrules = new Dictionary<int, List<int>>();
            //foreach (string rule in rules)
            //{
            //    string[] parts = rule.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            //    // check if the key is already in the dictionary
            //    if (Orderrules.ContainsKey(int.Parse(parts[0])))
            //    {
            //        Orderrules[int.Parse(parts[0])].Add(int.Parse(parts[1]));
            //    }
            //    else
            //    {
            //        Orderrules.Add(int.Parse(parts[0]), new List<int>() { int.Parse(parts[1]) });
            //    }
            //}
            //// check if the rules are correct
            //int sum = 0;
            //foreach (string instructionline in instructions)
            //{
            //    int[] parts = instructionline.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            //    // convert parts to a stack with the first element on top
            //    List<int> partslist = parts.Reverse().ToList();
            //    bool valid = true;
            //    while (partslist.Count > 0)
            //    {
            //        // get the first element
            //        int instruction = partslist[0];
            //        // get the pages that can not be in the remainder of the instruction
            //        List<int> pages = Orderrules[instruction];
            //        // print all usefull information to the console for debugging the bug
            //        Console.WriteLine($"instruction: {instruction}");
            //        Console.Write("remaining numbers: ");
            //        foreach (int part in partslist)
            //        {
            //            Console.Write(part + " ");
            //        }
            //        Console.WriteLine();
            //        Console.Write("incorrect instructions: ");
            //        foreach (int page in pages)
            //        {
            //            Console.Write(page + " ");
            //        }
            //        Console.WriteLine();
            //        // check if any of the pages are in the remaining part of the instruction
            //        if (pages.Any(x => partslist.Contains(x)))
            //        {
            //            Console.WriteLine("Invalid instruction");
            //            Console.WriteLine($"because {instruction} is after {pages.First(x => partslist.Contains(x))}");
            //            valid = false;
            //            break;
            //        }
            //        // remove the first element
            //        partslist.RemoveAt(0);
            //        Console.WriteLine("---------------------------------------------------");
            //    }
            //    if (valid == true)
            //    {
            //        // print the valid lines to the console
            //        foreach (int part in parts)
            //        {
            //            Console.Write(part + " ");
            //        }
            //        Console.WriteLine();
            //        // determine middle page of parts, // it will be uneven
            //        int middle = parts.Length / 2;
            //        Console.WriteLine(parts[middle]);
            //        sum += parts[middle];

            //    }
            //}
            //Console.WriteLine(sum);

            //let's try this again
            string[] lines = File.ReadAllLines("inputPart2.txt");

            HashSet<(string, string)> rules = new HashSet<(string, string)>();
            int i = 0;
            for (; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }

                string[] parts = line.Split('|');
                rules.Add((parts[0], parts[1]));
            }

            int total = 0;
            int totalFixed = 0;

            i++;
            for (; i < lines.Length; i++)
            {
                string[] pages = lines[i].Split(',');

                if (CanUpdate(rules, pages))
                {
                    total += int.Parse(pages[pages.Length / 2]);
                    continue;
                }

                Array.Sort(pages, (p1, p2) => rules.Contains((p1, p2))
                     ? -1
                     : rules.Contains((p2, p1)) ? 1 : 0
                );

                totalFixed += int.Parse(pages[pages.Length / 2]);
            }

            Console.WriteLine(total);
            Console.WriteLine(totalFixed);
        }

        private static bool CanUpdate(HashSet<(string, string)> rules, string[] pages)
        {
            for (int j = pages.Length - 1; j > 0; j--)
            {
                for (int k = j - 1; k >= 0; k--)
                {
                    if (rules.Contains((pages[j], pages[k])))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
    
}
